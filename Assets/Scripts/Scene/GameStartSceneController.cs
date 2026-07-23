[SceneController(SceneName = "GameStart", isGameScene = false)]
public class GameStartSceneController : SceneControllerBase
{
    public async override void OnSceneEnter()
    {
        await GameManager.UI.ShowPanel<GameStartPanel>();
        GameManager.Audio.PlayBGM("GameStart");
    }

    public override void OnSceneExit()
    {
        base.OnSceneExit();
    }
}