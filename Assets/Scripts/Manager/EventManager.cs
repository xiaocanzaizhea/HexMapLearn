using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    // ===== 游戏事件 =====
    private Action onGameStart;
    private Action<bool> onGameEnd;
    private Action onRoundStart;
    private Action onRoundEnd;
    
    // ===== 单位事件 =====
    private Action<HexUnit> onUnitDeath;
    private Action<HexUnit> onUnitSpawn;
    private Action<HexUnit, HexUnit, float> onUnitDamaged;
    private Action<HexUnit, HexCell, HexCell> onUnitMoved;
    private Action<HexUnit> onUnitBuild;
    private Action<HexUnit, bool> onUnitSelection;
    private Action onUnitUnSelection;
    private Action<HexUnit> onUnitLevelUp;
    
    // ===== 资源 =====
    private Action<int> onResourceGet;
    
    // ===== 天气事件 =====
    private Action<WeatherType> onWeatherChanged;
    
    // 游戏
    public void AddListener_GameStart(Action listener) => onGameStart += listener;
    public void RemoveListener_GameStart(Action listener) => onGameStart -= listener;
    public void AddListener_GameEnd(Action<bool> listener) => onGameEnd += listener;
    public void RemoveListener_GameEnd(Action<bool> listener) => onGameEnd -= listener;
    public void AddListener_RoundStart(Action listener) => onRoundStart += listener;
    public void RemoveListener_RoundStart(Action listener) => onRoundStart -= listener;
    public void AddListener_RoundEnd(Action listener) => onRoundEnd += listener;
    public void RemoveListener_RoundEnd(Action listener) => onRoundEnd -= listener;
    
    // 单位
    public void AddListener_UnitDeath(Action<HexUnit> listener) => onUnitDeath += listener;
    public void RemoveListener_UnitDeath(Action<HexUnit> listener) => onUnitDeath -= listener;
    
    public void AddListener_UnitSpawn(Action<HexUnit> listener) => onUnitSpawn += listener;
    public void RemoveListener_UnitSpawn(Action<HexUnit> listener) => onUnitSpawn -= listener;
    
    public void AddListener_UnitDamaged(Action<HexUnit, HexUnit, float> listener) 
        => onUnitDamaged += listener;
    public void RemoveListener_UnitDamaged(Action<HexUnit, HexUnit, float> listener) 
        => onUnitDamaged -= listener;
    
    public void AddListener_UnitMoved(Action<HexUnit, HexCell, HexCell> listener) => onUnitMoved += listener;
    public void RemoveListener_UnitMoved(Action<HexUnit, HexCell, HexCell> listener) => onUnitMoved -= listener;
    
    public void AddListener_UnitBuild(Action<HexUnit> listener) => onUnitBuild += listener;
    public void RemoveListener_UnitBuild(Action<HexUnit> listener) => onUnitBuild -= listener;
    
    public void AddListener_UnitSelection(Action<HexUnit, bool> listener) => onUnitSelection += listener;
    public void RemoveListener_UnitSelection(Action<HexUnit, bool> listener) => onUnitSelection -= listener;
    
    public void AddListener_UnitUnSelection(Action listener) => onUnitUnSelection += listener;
    public void RemoveListener_UnitUnSelection(Action listener) => onUnitUnSelection -= listener;
    
    public void AddListener_UnitLevelUp(Action<HexUnit> listener) => onUnitLevelUp += listener;
    public void RemoveListener_UnitLevelUp(Action<HexUnit> listener) => onUnitLevelUp -= listener;
    
    // 资源
    public void AddListener_ResourceGet(Action<int> listener) => onResourceGet += listener;
    public void RemoveListener_ResourceGet(Action<int> listener) => onResourceGet -= listener;
    
    // 天气
    public void AddListener_WeatherChanged(Action<WeatherType> listener) => onWeatherChanged += listener;
    public void RemoveListener_WeatherChanged(Action<WeatherType> listener) => onWeatherChanged -= listener;
    
    // ===== 触发事件 =====
    
    // 游戏
    public void TriggerGameStart() => onGameStart?.Invoke();
    public void TriggerGameEnd(bool isVictory) => onGameEnd?.Invoke(isVictory);
    public void TriggerRoundStart() => onRoundStart?.Invoke();
    public void TriggerRoundEnd() => onRoundEnd?.Invoke();
    
    // 单位
    public void TriggerUnitDeath(HexUnit unit) => onUnitDeath?.Invoke(unit);
    public void TriggerUnitSpawn(HexUnit unit) => onUnitSpawn?.Invoke(unit);
    public void TriggerUnitDamaged(HexUnit source, HexUnit target, float damage) 
        => onUnitDamaged?.Invoke(source, target, damage);
    public void TriggerUnitMoved(HexUnit unit, HexCell cell1, HexCell cell2) 
        => onUnitMoved?.Invoke(unit, cell1, cell2);
    
    public void TriggerUnitBuild(HexUnit unit) => onUnitBuild?.Invoke(unit);
    
    public void TriggerUnitSelection(HexUnit unit, bool b) => onUnitSelection?.Invoke(unit, b);
    public void TriggerUnitUnSelection() => onUnitUnSelection?.Invoke();
    
    public void TriggerUnitLevelUp(HexUnit unit) => onUnitLevelUp?.Invoke(unit);
    
    // 资源
    public void TriggerResourceGet(int value) => onResourceGet?.Invoke(value);
    
    // 天气
    public void TriggerWeatherChanged(WeatherType weather) => onWeatherChanged?.Invoke(weather);
}
