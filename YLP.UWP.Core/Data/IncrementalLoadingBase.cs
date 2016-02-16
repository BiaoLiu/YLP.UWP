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
using YLP.UWP.Core;
using YLP.UWP.Core.Data;
using YLP.UWP.Core.Models;
using YLP.UWP.Core.Services;

namespace YLP.UWP.Core
{
    public class IncrementalLoadingBase<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        protected readonly Func<int, Task<OperationResult<List<T>>>> _dataFetchDelegate;

        private bool _busy;
        private bool _hasMoreItems;
        private int _currentPage = 1;
        private int _pageSize = 12;

        public event DataLoadingEventHandler DataLoading;
        public event DataLoadedEventHandler DataLoaded;

        public int TotalCount
        {
            get; set;
        }
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
        public IncrementalLoadingBase(Func<int, Task<OperationResult<List<T>>>> dataFetchDelegate)
        {
            _dataFetchDelegate = dataFetchDelegate;

            HasMoreItems = true;
        }
        public void DoRefresh()
        {
            _currentPage = 1;
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
            var actualCount = 0;
            List<T> list = null;

            try
            {
                DataLoading?.Invoke();

                var result = await _dataFetchDelegate(_currentPage);
                list = result.Data;
            }
            catch (Exception)
            {
                HasMoreItems = false;
            }

            if (list != null && list.Any())
            {
                actualCount = list.Count;
                TotalCount += actualCount;
                _currentPage++;

                HasMoreItems = true;
                list.ForEach(c => this.Add(c));
            }
            else
            {
                HasMoreItems = false;
            }

            DataLoaded?.Invoke();

            _busy = false;

            return new LoadMoreItemsResult { Count = (uint)actualCount };
        }
    }
}
