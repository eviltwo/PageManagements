using UnityEngine;

namespace PageManagements
{
    [CreateAssetMenu(fileName = "PagePrefabReferences", menuName = "PageManagements/PagePrefabReferences")]
    public class PagePrefabReferences : ScriptableObject
    {
        [SerializeField]
        private PageBase[] _pagePrefabs = null;

        public T GetPagePrefab<T>() where T : PageBase
        {
            foreach (var page in _pagePrefabs)
            {
                if (page is T tPage)
                {
                    return tPage;
                }
            }

            return null;
        }
    }
}
