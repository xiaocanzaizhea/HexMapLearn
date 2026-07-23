using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 游戏开始
public interface IGameStartHandler
{
    void OnGameStart();
}

// 游戏结束
public interface IGameEndHandler
{
    void OnGameEnd(bool isVictory);
}

// 回合开始
public interface IRoundStartHandler
{
    void OnRoundStart();
}

// 回合结束
public interface IRoundEndHandler
{
    void OnRoundEnd();
}
