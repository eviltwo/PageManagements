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

        internal T Build<T>() where T : IPage
        {
            var pagePrefab = _pagePrefabReferences.GetPagePrefab<T>();
            if (pagePrefab == null)
            {
                Debug.LogError($"Page prefab of type {typeof(T)} not found.");
                return default;
            }

            var go = Object.Instantiate(pagePrefab, _pageParent);
            return go.GetComponent<T>();
        }
    }
}
