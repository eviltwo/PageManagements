using System.Threading;
using Cysharp.Threading.Tasks;
using PageManagements;
using UnityEngine;

public class Sample : MonoBehaviour
{
    [SerializeField]
    private Transform _pageParent = null;

    [SerializeField]
    private PagePrefabReferences _pagePrefabReferences = null;

    [SerializeField]
    private GameObject _cover = null;

    private PageManager _manager;

    private void Start()
    {
        var builder = new PageBuilder(_pageParent, _pagePrefabReferences);
        _manager = new PageManager(builder);
        _manager.PageChanged += OnPageChanged;
    }

    private void OnDestroy()
    {
        _manager.Dispose();
    }

    private void Update()
    {
        _manager.Update();
        if (Input.GetKeyDown(KeyCode.Space) && !_manager.HasPage<FirstPage>())
        {
            OpenPageAsync(destroyCancellationToken).Forget();
        }
    }

    private async UniTask OpenPageAsync(CancellationToken cancellationToken)
    {
        var pageHandle = await _manager.Create<FirstPage>(cancellationToken);
        pageHandle.Page.Setup(_manager);
    }

    private void OnPageChanged()
    {
        _cover.SetActive(_manager.GetPageCount() > 0);
    }
}
