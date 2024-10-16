using System.Threading;
using Cysharp.Threading.Tasks;
using PageManagements;
using PageManagements.Pages;
using TMPro;
using UnityEngine;

public class FirstPage : MonoBehaviour, IPage
{
    [SerializeField]
    private TMP_Text _foodText = null;

    private PageManager _pageManager;
    private bool _shouldClose;

    public void Setup(PageManager pageManager)
    {
        _pageManager = pageManager;
    }

    public void Dispose()
    {
        Debug.Log("FirstPage.Dispose");
        Destroy(gameObject);
    }

    public UniTask Show(CancellationToken cancellationToken)
    {
        Debug.Log("FirstPage.Show");
        gameObject.SetActive(true);
        return UniTask.CompletedTask;
    }

    public bool ShouldClose() => _shouldClose;

    public UniTask Hide(CancellationToken cancellationToken)
    {
        Debug.Log("FirstPage.Hide");
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

    public void OnOpenFoodSelectionPage()
    {
        OpenFoodSelectionPageAsync(destroyCancellationToken).Forget();
    }

    private async UniTask OpenFoodSelectionPageAsync(CancellationToken cancellationToken)
    {
        var pageHandle = await _pageManager.Create<MultipleButtonsPage>("FoodSelectionPage", cancellationToken);
        pageHandle.Page.OnClickButton += key =>
        {
            switch (key)
            {
                case "Pizza":
                    _foodText.text = "Pizza! Loaded with cheese.";
                    break;
                case "Hamburger":
                    _foodText.text = "Hamburger! With potatoes.";
                    break;
                case "Sushi":
                    _foodText.text = "Sushi! Without wasabi.";
                    break;
            }
            pageHandle.Remove(destroyCancellationToken).Forget();
        };
    }

    public void OnClose()
    {
        _shouldClose = true;
    }
}
