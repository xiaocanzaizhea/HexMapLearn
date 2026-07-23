using UnityEngine;
using UnityEngine.UI;

public class GameStartPanel : BasePanel
{
    public Button newGameButton;
    public Button continueButton;
    public Button loadGameButton;
    public Button optionButton;
    public Button exitButton;
    
    private bool alreadyClick=false;
    
    public override void Init()
    {
        GameManager.UI.RegisterPanel(this);
    }

    public override void Show()
    {
        base.Show();
        continueButton.gameObject.SetActive(!GameManager.Instance.firstTimeEnterGame);
        loadGameButton.gameObject.SetActive(!GameManager.Instance.firstTimeEnterGame);
    }

    protected override void Awake()
    {
        base.Awake();
        newGameButton.onClick.AddListener(OnContinueButtonClick);
        continueButton.onClick.AddListener(OnContinueButtonClick);
        loadGameButton.onClick.AddListener(OnLoadGameButtonClick);
        optionButton.onClick.AddListener(OnOptionButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private async void OnContinueButtonClick()
    {
        if (alreadyClick) return;
        alreadyClick = true;
        SceneLoadPanel panel = await GameManager.UI.ShowPanel<SceneLoadPanel>();
        GameManager.UI.mainCanvas.PrepareFade();
        GameManager.UI.HidePanel<GameStartPanel>();
        // string sceneName = GameManager.Files
    }
    
    private async void OnNewGameButtonClick() 
    {
        
    }

    private void OnLoadGameButtonClick()
    {
        
    }
    
    private void OnExitButtonClick() 
    {
        Application.Quit();
    }

    private void OnOptionButtonClick()
    {
        
    }
}