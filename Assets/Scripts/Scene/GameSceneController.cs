using UnityEngine;

[SceneController(SceneName = "GameScene", isGameScene = true)]
public class GameSceneController : SceneControllerBase
{
    public async override void OnSceneEnter()
    {
        await GameManager.UI.ShowPanel<GameStartPanel>();
        GameManager.Audio.SceneEffectAudioSource = GetComponent<AudioSource>();
        GameManager.Audio.PlayBGM("GameStart");
    }
}