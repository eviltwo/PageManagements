using Cysharp.Threading.Tasks;
using PageManagements;

public class FirstPage : PageBase
{
    public override void Dispose()
    {
        Destroy(gameObject);
    }

    public override void Show()
    {
        gameObject.SetActive(true);
    }

    public override void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnOpenSecondPage()
    {
        OpenSecondPageAsync().Forget();
    }

    private async UniTask OpenSecondPageAsync()
    {
        var page = await PageArg.PageBuilder.BuildAsync<SecondPage>(PageArg, destroyCancellationToken);
        PageArg.PageManager.Push(page);
    }

    public void OnClose()
    {
        PageArg.PageManager.Pop();
    }
}
