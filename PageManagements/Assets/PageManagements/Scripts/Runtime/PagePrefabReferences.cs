using UnityEngine;

namespace PageManagements
{
    [CreateAssetMenu(fileName = "PagePrefabReferences", menuName = "PageManagements/PagePrefabReferences")]
    public class PagePrefabReferences : ScriptableObject
    {
        [SerializeField]
        private GameObject[] _pagePrefabs = null;

        public GameObject GetPagePrefab<T>() where T : IPage
        {
            var count = _pagePrefabs.Length;
            for (var i = 0; i < count; i++)
            {
                var prefab = _pagePrefabs[i];
                if (prefab.TryGetComponent<T>(out _))
                {
                    return prefab;
                }
            }

            return null;
        }

        public GameObject GetPagePrefab<T>(string name) where T : IPage
        {
            var count = _pagePrefabs.Length;
            for (var i = 0; i < count; i++)
            {
                var prefab = _pagePrefabs[i];
                if (prefab.name == name && prefab.TryGetComponent<T>(out _))
                {
                    return prefab;
                }
            }

            return null;
        }
    }
}
