using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private readonly Func<int, int, Task<OperationResult<List<T>>>> _dataFetchDelegate;

        private bool _busy;
        private bool _hasMoreItems;
        //当前页码
        private int _pageIndex = 1;
        //页容量
        private int _pageSize = 12;

        public event DataLoadingEventHandler DataLoading;
        public event DataLoadedEventHandler DataLoaded;

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
        public IncrementalLoading(Func<int, int, Task<OperationResult<List<T>>>> dataFetchDelegate)
        {
            _dataFetchDelegate = dataFetchDelegate;

            HasMoreItems = true;
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

            List<T> list = null;
            try
            {
                //开始加载
                DataLoading?.Invoke();
                //请求服务器获取数据
                var result = await _dataFetchDelegate(_pageIndex, _pageSize);
                list = result.Data;
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
}
