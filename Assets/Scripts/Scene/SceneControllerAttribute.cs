using System;

[AttributeUsage(AttributeTargets.Class)]
public class SceneControllerAttribute : Attribute
{
    public string SceneName;
    public bool isGameScene;
}

[AttributeUsage(AttributeTargets.Method)]
public class OnSceneEnterCallbackAttribute : Attribute 
{

}
[AttributeUsage(AttributeTargets.Method)]
public class OnSceneExitCallbackAttribute : Attribute
{

}