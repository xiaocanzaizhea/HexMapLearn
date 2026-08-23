using UnityEngine;

[SceneController(SceneName = "VillageScene", isGameScene = false)]
public class VillageSceneController : SceneControllerBase
{
    public override async void OnSceneEnter()
    {
        // await GameManager.UI.ShowPanel<GameStartPanel>();
        GameManager.Audio.PlayBGM("Village");
    }
}