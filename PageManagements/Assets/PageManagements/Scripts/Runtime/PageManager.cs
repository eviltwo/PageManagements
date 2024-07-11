using System;
using System.Collections.Generic;
using System.Linq;

namespace PageManagements
{
    public class PageManager
    {
        private List<PageBase> _pages = new List<PageBase>();

        public event Action PageChanged;

        public void Push(PageBase page)
        {
            var oldPage = _pages.LastOrDefault();

            // TODO: Add transition
            if (oldPage == null)
            {
                page.Show();
            }
            else
            {
                oldPage.Hide();
                page.Show();
            }

            _pages.Add(page);
            PageChanged?.Invoke();
        }

        public void Pop()
        {
            if (_pages.Count == 0)
            {
                return;
            }

            var lastPage = _pages.Last();
            var prevPage = _pages.Count > 1 ? _pages[_pages.Count - 2] : null;

            // TODO: Add transition
            if (prevPage == null)
            {
                lastPage.Hide();
            }
            else
            {
                lastPage.Hide();
                prevPage.Show();
            }

            _pages.Remove(lastPage);
            lastPage.Dispose();
            PageChanged?.Invoke();
        }

        public int GetPageCount()
        {
            return _pages.Count;
        }

        public bool HasPage<T>() where T : PageBase
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
