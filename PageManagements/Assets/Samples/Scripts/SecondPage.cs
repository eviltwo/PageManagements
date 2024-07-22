using System.Threading;
using Cysharp.Threading.Tasks;
using PageManagements;
using UnityEngine;

public class SecondPage : MonoBehaviour, IPage
{
    private bool _shouldClose;

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

    public void OnClose()
    {
        _shouldClose = true;
    }
}
