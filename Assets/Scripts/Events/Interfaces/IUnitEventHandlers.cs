using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 单位死亡
public interface IUnitDeathHandler
{
    void OnUnitDeath(HexUnit unit);
}

// 单位生成
public interface IUnitSpawnHandler
{
    void OnUnitSpawn(HexUnit unit);
}

// 单位受伤
public interface IUnitDamageHandler
{
    void OnUnitDamaged(HexUnit unit, float damage);
}

// 单位移动
public interface IUnitMoveHandler
{
    void OnUnitMoved(HexUnit unit, HexCell from, HexCell to);
}

// 单位建造成功
public interface IUnitBuildHandler
{
    void OnUnitBuild(HexUnit unit);
}

// 单位被选择, bool表示是模版还是实际单位
public interface IUnitSelectionHandler
{
    void OnUnitSelected(HexUnit unit, bool isInMap = true);
}

// 单位取消选择
public interface IUnitUnSelectionHandler
{
    void OnUnitUnSelected();
}

public interface IUnitExpLevelUpHandler
{
    void OnUnitExpLevelUp(HexUnit unit);
}