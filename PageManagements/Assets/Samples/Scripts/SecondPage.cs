using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PageManagements;
using UnityEngine;

public class SecondPage : MonoBehaviour, IPage
{
    public event Action OnCloseSelected;

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

    public void OnClose()
    {
        OnCloseSelected?.Invoke();
    }
}
