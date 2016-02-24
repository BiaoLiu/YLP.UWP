using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace YLP.UWP.Core.Common
{
    public class IncrementalLoading<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        private readonly Func<int, int, Task<OperationResult<List<T>>>>[] _dataFetchDelegate;

        private bool _busy;
        private bool _hasMoreItems;
        //当前页码
        private int _pageIndex = 1;
        //页容量
        private int _pageSize = 12;

        private int[] _pageSizes;

        public event DataLoadingEventHandler DataLoading;
        public event DataLoadedEventHandler DataLoaded;
        public ResultProcessDelegate<T> ResultProcess; 

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 是否还有更多条目
        /// </summary>
        public bool HasMoreItems
        {
            get
            {
                if (_busy)
                {
                    return false;
                }

                return _hasMoreItems;
            }
            private set
            {
                _hasMoreItems = value;
            }
        }
        public IncrementalLoading(params Func<int, int, Task<OperationResult<List<T>>>>[] dataFetchDelegate)
        {
            _dataFetchDelegate = dataFetchDelegate;

            HasMoreItems = true;
        }

        public IncrementalLoading(int[] pageSize, params Func<int, int, Task<OperationResult<List<T>>>>[] dataFetchDelegate)
            : this(dataFetchDelegate)
        {
            _pageSizes = pageSize;
        }

        /// <summary>
        /// 刷新操作
        /// </summary>
        public void DoRefresh()
        {
            _pageIndex = 1;
            TotalCount = 0;
            Clear();
            HasMoreItems = true;
        }
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return InnerLoadMoreItemsAsync(count).AsAsyncOperation();
        }
        private async Task<LoadMoreItemsResult> InnerLoadMoreItemsAsync(uint expectedCount)
        {
            _busy = true;
            //实际条数
            var actualCount = 0;

            List<T> list = new List<T>();
            try
            {
                //开始加载
                DataLoading?.Invoke();
                //请求服务器获取数据
                for (var index = 0; index < _dataFetchDelegate.Count(); index++)
                {
                    var dataFetchDelegate = _dataFetchDelegate[index];

                    int pageSize = _pageSize;
                    if (_pageSizes != null && _pageSizes.Count() > index)
                    {
                        pageSize = _pageSizes[index];
                    }

                    var result = await dataFetchDelegate(_pageIndex, pageSize);
                    list.AddRange(result.Data);
                }

                ResultProcess?.Invoke(list);
            }
            catch (Exception ex)
            {
                HasMoreItems = false;
            }

            if (list != null && list.Any())
            {
                actualCount = list.Count;
                TotalCount += actualCount;
                _pageIndex++;

                HasMoreItems = true;
                list.ForEach(this.Add);
            }
            else
            {
                HasMoreItems = false;
            }

            //加载结束
            DataLoaded?.Invoke();
            _busy = false;

            return new LoadMoreItemsResult { Count = (uint)actualCount };
        }
    }

    public delegate void DataLoadingEventHandler();

    public delegate void DataLoadedEventHandler();

    public delegate void ResultProcessDelegate<T>(List<T> result);
}
