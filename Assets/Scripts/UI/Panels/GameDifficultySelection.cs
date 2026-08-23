using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameDifficultySelection : BasePanel
{
    private int currentDifficulty;

    [SerializeField]
    private GameObject PlayerViewport;

    [SerializeField]
    private GameObject EnemyViewport;
    
    [SerializeField]
    private TextMeshProUGUI DiffcultyText;
    
    [SerializeField]
    private Button AddButton;
    
    [SerializeField]
    private Button SubstractButton;
    
    [SerializeField]
    private Button StartButton;

    [SerializeField]
    private GameSelectionItem GameSelectionItem;
    
    [SerializeField] 
    private GameObject PlayerInstanceParent;
    
    [SerializeField] 
    private GameObject EnemyInstanceParent;

    public override void Init()
    {
        currentDifficulty = 0;
        AddButton.onClick.AddListener((() =>
        {
            AddDifficulty();
            GameManager.Audio.PlayUIEffect("UI_ClickNormal");
        }));
        SubstractButton.onClick.AddListener((() =>
        {
            SubstractDifficulty();
            GameManager.Audio.PlayUIEffect("UI_ClickNormal");
        }));
        StartButton.onClick.AddListener((() =>
        {
            LoadGameScene();
        }));
        foreach (Transform child in PlayerInstanceParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    async void LoadGameScene()
    {
        SceneLoadPanel panel = await GameManager.UI.ShowPanel<SceneLoadPanel>();
        GameManager.UI.mainCanvas.PrepareFade();
        await GameManager.AssetLoader.LoadScene("GameScene", p =>
        {
            panel.SetPercentage((int)p*100);
        }, scene =>
        {
            panel.SetPercentage(100);
            GameManager.UI.HidePanel<GameEndPanel>();
        });
    }

    void AddDifficulty()
    {
        if(currentDifficulty >= 10) return;
        currentDifficulty++;
        ShowDifficulty();
        AddDifficultyItem(PlayerInstanceParent);
        AddDifficultyItem(EnemyInstanceParent);
    }

    void SubstractDifficulty()
    {
        if(currentDifficulty <= 0) return;
        currentDifficulty--;
        ShowDifficulty();
        RemoveDifficultyItem(PlayerInstanceParent);
        RemoveDifficultyItem(EnemyInstanceParent);
    }

    void AddDifficultyItem(GameObject parent)
    {
        Instantiate(GameSelectionItem, parent.transform);
    }

    void RemoveDifficultyItem(GameObject parent)
    {
        if (parent == null || parent.transform.childCount == 0) return;
    
        Transform lastChild = parent.transform.GetChild(parent.transform.childCount - 1);
        Destroy(lastChild.gameObject);
    }

    void ShowDifficulty()
    {
        DiffcultyText.text = "N " + currentDifficulty.ToString();
    }
}
