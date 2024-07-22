using System.Threading;
using Cysharp.Threading.Tasks;
using PageManagements;
using UnityEngine;

public class FirstPage : MonoBehaviour, IPage
{
    private PageManager _pageManager;
    private bool _shouldClose;

    public void Setup(PageManager pageManager)
    {
        _pageManager = pageManager;
    }

    public void Dispose()
    {
        Destroy(gameObject);
    }

    public UniTask Show(CancellationToken cancellationToken)
    {
        gameObject.SetActive(true);
        return UniTask.CompletedTask;
    }

    public bool ShouldClose() => _shouldClose;

    public UniTask Hide(CancellationToken cancellationToken)
    {
        gameObject.SetActive(false);
        return UniTask.CompletedTask;
    }

    public void OnOpenSecondPage()
    {
        OpenSecondPageAsync(destroyCancellationToken).Forget();
    }

    private UniTask OpenSecondPageAsync(CancellationToken cancellationToken)
    {
        return _pageManager.Create<SecondPage>(cancellationToken);
    }

    public void OnClose()
    {
        _shouldClose = true;
    }
}
