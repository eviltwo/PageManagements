using PageManagements;

public class SecondPage : PageBase
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

    public void OnClose()
    {
        PageArg.PageManager.Pop();
    }
}
