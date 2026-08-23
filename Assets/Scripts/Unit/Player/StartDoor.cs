using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDoor : PlayerUnit
{
    protected override void OnClicked()
    {
        base.OnClicked();
        LoadGameDifficultSelectionPanel();
    }
    
    async void LoadGameDifficultSelectionPanel()
    {
        await GameManager.UI.ShowPanel<GameDifficultySelection>();
    }
}
