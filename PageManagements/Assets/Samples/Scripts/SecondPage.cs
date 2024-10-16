using System.Threading;
using Cysharp.Threading.Tasks;
using PageManagements;
using UnityEngine;

public class SecondPage : MonoBehaviour, IPage
{
    private bool _shouldClose;

    public void Dispose()
    {
        Debug.Log("SecondPage.Dispose");
    }

    public UniTask Show(CancellationToken cancellationToken)
    {
        Debug.Log("SecondPage.Show");
        gameObject.SetActive(true);
        return UniTask.CompletedTask;
    }

    public bool ShouldClose() => _shouldClose;

    public UniTask Hide(CancellationToken cancellationToken)
    {
        Debug.Log("SecondPage.Hide");
        gameObject.SetActive(false);
        return UniTask.CompletedTask;
    }

    public void OnClose()
    {
        _shouldClose = true;
    }
}
