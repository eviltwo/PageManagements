using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PageManagements
{
    public class PageBuilder
    {
        private readonly Transform _pageParent = null;
        private readonly PagePrefabReferences _pagePrefabReferences = null;

        public PageBuilder(Transform pageParent, PagePrefabReferences pagePrefabReferences)
        {
            _pageParent = pageParent;
            _pagePrefabReferences = pagePrefabReferences;
        }

        public async UniTask<T> BuildAsync<T>(PageArgument arg, CancellationToken cancellationToken) where T : PageBase
        {
            var pagePrefab = _pagePrefabReferences.GetPagePrefab<T>();
            if (pagePrefab == null)
            {
                Debug.LogError($"Page prefab of type {typeof(T)} not found.");
                return null;
            }

            var page = Object.Instantiate(pagePrefab, _pageParent);
            await page.Initialize(arg, cancellationToken);
            return page;
        }
    }
}
