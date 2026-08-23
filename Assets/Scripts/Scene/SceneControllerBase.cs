using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

public class SceneControllerBase : MonoBehaviour
{
    public string sceneName;              
    private bool isGameScene;             
    private PlayerMainInfoPanel mainInfoPanel;  
    private bool loadToAnotherScene;
    private HexGrid Grid => GameManager.RunTimeData.grid;

    private UnitsBuildableItem BarCurrentUnit => GameManager.RunTimeData.CurrentSelectedUnitInUI;
    
    private HexCell currentCell;
    private HexUnit selectedUnit;
    
    protected virtual async void Awake()
    {
        HexGrid hexGrid = FindObjectOfType<HexGrid>(); 
        GameManager.RunTimeData.grid = hexGrid;
        sceneName = GetType().GetCustomAttribute<SceneControllerAttribute>().SceneName;
        // 注册场景
        GameManager.Scene.Register(sceneName,this);
        // 注册事件
        isGameScene = GetType().GetCustomAttribute<SceneControllerAttribute>().isGameScene;
        GameManager.Input.playerInputControl.GamePlay.Enable();
        if (isGameScene)
        {
            mainInfoPanel = await GameManager.UI.ShowPanel<PlayerMainInfoPanel>();
            GameManager.Camera.UpdateCameras();
        }
    }

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        GameManager.Event.Register(HexEvents.GameStart.ToString(), new GameEvent(OnGameStart));
        GameManager.Event.Register(HexEvents.GameOver.ToString(), new GameEvent(OnGameOver));
    }

    private void OnDisable()
    {
        GameManager.Event.Unregister(HexEvents.GameStart.ToString(), new GameEvent(OnGameStart));
        GameManager.Event.Unregister(HexEvents.GameOver.ToString(), new GameEvent(OnGameOver));
    }

    void OnGameStart()
    {
        if(!isGameScene) return;
        // 增加玩家资源数
        GameManager.Event.Broadcast(HexEvents.ResourceChange.ToString(),
            new GameEventParameter<int>(GameManager.RunTimeData.startResourceCount));
    }

    async void OnGameOver()
    {
        if(!isGameScene) return;
        Debug.Log("Game Over");
        await GameManager.UI.ShowPanel<GameEndPanel>();
    }

    protected virtual void Update()
    {
        if(loadToAnotherScene) return;
        if (isGameScene)
        {
            if (GameManager.Input.State.SwitchPause)
            {
                if (GameManager.UI.IsShow<PauseMenuPanel>())
                {
                    GameManager.UI.HidePanel<PauseMenuPanel>();
                    GameManager.TimeScale.ResetTime();
                    // GameManager.Camera.EnableCamera("MainCMCamera", true);
                }
                else
                {
                    GameManager.UI.ShowPanel<PauseMenuPanel>();
                    GameManager.TimeScale.ScaleTime(0, -1f);
                    // GameManager.Camera.DisableCamera("MainCMCamera", true);
                }
            }
        }
    }

    public virtual void OnSceneEnter() 
    {
        
    }
    
    public virtual void OnSceneExit() 
    {
        
    }
    
    private void OnDestroy()
    {
        GameManager.Scene.Unregister(sceneName);
    }
}