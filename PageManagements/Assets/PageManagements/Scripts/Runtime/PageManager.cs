using System.Collections.Generic;
using System.Linq;

namespace PageManagements
{
    public class PageManager
    {
        private List<PageBase> _pages = new List<PageBase>();

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
        }
    }
}
