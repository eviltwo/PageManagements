using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PageManagements
{
    public class PageManager : IDisposable
    {
        private readonly PageBuilder _pageBuilder;
        private readonly List<IPage> _pages = new List<IPage>();
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public event Action PageChanged;

        public PageManager(PageBuilder pageBuilder)
        {
            _pageBuilder = pageBuilder;
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        public void Update()
        {
            for (var i = _pages.Count - 1; i >= 0; i--)
            {
                if (_pages[i].ShouldClose())
                {
                    Remove(i, _cancellationTokenSource.Token).Forget();
                }
            }
        }

        public async UniTask<PageHandle<T>> Create<T>(CancellationToken cancellationToken)
            where T : IPage
        {
            var page = _pageBuilder.Build<T>();
            await Push(page, cancellationToken);
            return new PageHandle<T>(page, this);
        }

        public async UniTask<PageHandle<T>> Create<T>(string pageName, CancellationToken cancellationToken)
            where T : IPage
        {
            var page = _pageBuilder.Build<T>(pageName);
            await Push(page, cancellationToken);
            return new PageHandle<T>(page, this);
        }

        private async UniTask Push(IPage page, CancellationToken cancellationToken)
        {
            var oldPage = _pages.LastOrDefault();
            _pages.Add(page);
            PageChanged?.Invoke();
            // Switch page animation
            if (oldPage != null)
            {
                await oldPage.Hide(cancellationToken);
            }
            await page.Show(cancellationToken);
        }

        public UniTask Remove(IPage page, CancellationToken cancellationToken)
        {
            var index = _pages.IndexOf(page);
            if (index == -1)
            {
                Debug.LogError($"Page not found. {page}");
                return UniTask.CompletedTask;
            }
            return Remove(index, cancellationToken);
        }

        public async UniTask Remove(int index, CancellationToken cancellationToken)
        {
            if (index < 0 || index >= _pages.Count)
            {
                Debug.LogError($"Invalid index. pages:{_pages.Count}, index:{index}");
                return;
            }

            var isLastPage = index == _pages.Count - 1;
            var page = _pages[index];
            _pages.Remove(page);
            PageChanged?.Invoke();

            // Switch page animation
            await page.Hide(cancellationToken);
            page.Dispose();
            if (isLastPage && _pages.Count > 0)
            {
                var prevPage = _pages.Last();
                await prevPage.Show(cancellationToken);
            }
        }

        public UniTask Pop(CancellationToken cancellationToken)
        {
            return Remove(_pages.Count - 1, cancellationToken);
        }

        public int GetPageCount()
        {
            return _pages.Count;
        }

        public bool HasPage<T>() where T : IPage
        {
            var pageCount = _pages.Count;
            for (var i = 0; i < pageCount; i++)
            {
                if (_pages[i] is T)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
