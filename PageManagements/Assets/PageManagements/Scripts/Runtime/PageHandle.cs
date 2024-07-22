using System.Threading;
using Cysharp.Threading.Tasks;

namespace PageManagements
{
    public class PageHandle<T> where T : IPage
    {
        public readonly T Page;
        private readonly PageManager _pageManager;

        public PageHandle(T page, PageManager pageManager)
        {
            Page = page;
            _pageManager = pageManager;
        }

        public UniTask Remove(CancellationToken cancellationToken)
        {
            return _pageManager.Remove(Page, cancellationToken);
        }
    }
}
