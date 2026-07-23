using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager
{
    private Dictionary<string, SceneControllerBase> sceneEventCallbacks = new Dictionary<string, SceneControllerBase>();
    public SceneControllerBase currentScene;
    public void OnSceneEnter(string sceneName) 
    {
        //Debug.Log("Enter Scene:" + sceneName);
        if (sceneEventCallbacks.ContainsKey(sceneName))
        {
            sceneEventCallbacks[sceneName]?.OnSceneEnter();
        }
    }
    public void OnSceneExit(string sceneName)
    {
        //Debug.Log("Exit Scene:" + sceneName);
        if (sceneEventCallbacks.ContainsKey(sceneName))
        {
            sceneEventCallbacks[sceneName]?.OnSceneExit();
        }
    }

    public void Register(string sceneName,SceneControllerBase sceneController)
    {
        sceneEventCallbacks[sceneName] = sceneController;
        GameManager.Instance.sceneControllerInitialFinish = true;
        currentScene = sceneController;
    }

    public void Unregister(string sceneName) 
    {
        if (sceneEventCallbacks.ContainsKey(sceneName))
        {
            sceneEventCallbacks[sceneName] = null;
        }
    }
}
