using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager
{
    public Dictionary<string, List<IGameEvent>> events = new Dictionary<string, List<IGameEvent>>();

    public void Register(string name, IGameEvent gameEvent) 
    {
        if (string.IsNullOrEmpty(name) || gameEvent == null) return;

        if (!events.ContainsKey(name))
        {
            events[name] = new List<IGameEvent>();
        }

        if (!events[name].Contains(gameEvent))
        {
            events[name].Add(gameEvent);
        }
    }
    public void Unregister(string name, IGameEvent gameEvent) 
    {
        if (string.IsNullOrEmpty(name) || gameEvent == null) return;
        if (!events.ContainsKey(name)) return;

        events[name].Remove(gameEvent);
        
        // 如果列表为空，删除该键
        if (events[name].Count == 0)
        {
            events.Remove(name);
        }
    }

    public void UnregisterAll(string name)
    {
        if (string.IsNullOrEmpty(name)) return;
        events.Remove(name);
    }
    
    public void Broadcast(string name, IGameEventParameter parameters)
    {
        if (string.IsNullOrEmpty(name)) return;
        if (!events.ContainsKey(name)) return;

        // 遍历所有注册的事件
        foreach (var gameEvent in events[name])
        {
            gameEvent.Invoke(parameters);
        }
    }
}
