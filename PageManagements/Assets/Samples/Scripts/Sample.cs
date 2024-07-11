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

    private PageBuilder _builder;
    private PageManager _manager;

    private void Start()
    {
        _builder = new PageBuilder(_pageParent, _pagePrefabReferences);
        _manager = new PageManager();
        _manager.PageChanged += OnPageChanged;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_manager.HasPage<FirstPage>())
        {
            OpenPageAsync(destroyCancellationToken).Forget();
        }
    }

    private async UniTask OpenPageAsync(CancellationToken cancellationToken)
    {
        var arg = new PageArgument
        {
            PageBuilder = _builder,
            PageManager = _manager,
        };
        var page = await _builder.BuildAsync<FirstPage>(arg, cancellationToken);
        _manager.Push(page);
    }

    private void OnPageChanged()
    {
        _cover.SetActive(_manager.GetPageCount() > 0);
    }
}
