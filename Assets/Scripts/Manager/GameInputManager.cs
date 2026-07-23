using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class GameInputManager
{
    public GameInputStateData State => inputState;
    private GameInputStateData inputState;

    public PlayerInputControl playerInputControl;
    
    public GameInputManager()
    {
        inputState = new GameInputStateData();
        playerInputControl = new PlayerInputControl();
        
        playerInputControl.RegisterActionCallBack(
            playerInputControl.GamePlay.Pause, ActionType.Started, (cb) => { 
                inputState.SwitchPause = true;
            });
        
        playerInputControl.RegisterActionCallBack(
            playerInputControl.GamePlay.LeftClick, ActionType.Started, (cb) =>
            {
                inputState.LeftMouseClick = true;
            });
        
        playerInputControl.RegisterActionCallBack(
            playerInputControl.GamePlay.RightClick, ActionType.Started, (cb) =>
            {
                inputState.RightMouseClick = true;
            });
    }

    public void ResetAllButtonValueOnLateUpdate()
    {
        State.SwitchPause = false;
        State.Cancel = false;
        State.LeftMouseClick = false;
        State.RightMouseClick = false;
    }
}