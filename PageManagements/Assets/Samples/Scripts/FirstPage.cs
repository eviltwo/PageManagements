using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PageManagements;
using UnityEngine;

public class FirstPage : MonoBehaviour, IPage
{
    private PageManager _pageManager;

    public event Action OnCloseSelected;

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

    public UniTask Hide(CancellationToken cancellationToken)
    {
        gameObject.SetActive(false);
        return UniTask.CompletedTask;
    }

    public void OnOpenSecondPage()
    {
        OpenSecondPageAsync(destroyCancellationToken).Forget();
    }

    private async UniTask OpenSecondPageAsync(CancellationToken cancellationToken)
    {
        var pageHandle = await _pageManager.Create<SecondPage>(cancellationToken);
        pageHandle.Page.OnCloseSelected += () =>
        {
            pageHandle.Remove(destroyCancellationToken).Forget();
        };
    }

    public void OnClose()
    {
        OnCloseSelected?.Invoke();
    }
}
