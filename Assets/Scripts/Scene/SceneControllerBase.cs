using System;
using System.Reflection;
using UnityEngine;

public class SceneControllerBase : MonoBehaviour
{
    public string sceneName;              
    private bool isGameScene;             
    private PlayerMainInfoPanel mainInfoPanel;  
    private bool loadToAnotherScene;

    protected virtual async void Awake()
    {
        sceneName = this.GetType().GetCustomAttribute<SceneControllerAttribute>().SceneName;
        // 注册场景
        GameManager.Scene.Register(sceneName,this);
        isGameScene = this.GetType().GetCustomAttribute<SceneControllerAttribute>().isGameScene;
        GameManager.Input.playerInputControl.GamePlay.Enable();
        if (isGameScene)
        {
            mainInfoPanel = await GameManager.UI.ShowPanel<PlayerMainInfoPanel>();
            GameManager.Camera.UpdateCameras();
        }
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
                    GameManager.Camera.EnableCamera("MainCMCamera", true);
                }
                else
                {
                    GameManager.UI.ShowPanel<PauseMenuPanel>();
                    GameManager.TimeScale.ScaleTime(0, -1f);
                    GameManager.Camera.DisableCamera("MainCMCamera", true);
                }
            }

            if (GameManager.Input.State.LeftMouseClick)
            {
                if (SightOnNPC())
                {
                    
                }
            }
        }
    }

    public bool SightOnNPC()
    {
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //
        // if (Physics.Raycast(ray, out RaycastHit hit))
        // {
        //     if (hit.transform.gameObject.layer == LayerMask.NameToLayer("NPC"))
        //     {
        //         return true;
        //     }
        // }
        return true;
    }

    public bool SightOnCell()
    {
        return true;
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