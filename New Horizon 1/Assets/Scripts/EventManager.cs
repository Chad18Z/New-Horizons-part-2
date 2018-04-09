using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventParam
{
    public float float1;
    public float float2;
    public EventParam(float float1 = 0f, float float2 = 0f)
    {
        this.float1 = float1;
        this.float2 = float2;
    }
}

public class EventManager : SingletonBehavior<EventManager>
{
    private Dictionary<string, Action<EventParam>> eventDictionary;
    protected override void Init()
    {
        base.Init();
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, Action<EventParam>>();
        }
    }

    public void StartListening(string eventName, Action<EventParam> listener)
    {
        Action<EventParam> thisEvent;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            instance.eventDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public void StopListening(string eventName, Action<EventParam> listener)
    {
        Action<EventParam> thisEvent;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            instance.eventDictionary[eventName] = thisEvent;
        }
    }

    public void TriggerEvent(string eventName, EventParam eventParam)
    {
        Action<EventParam> thisEvent;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(eventParam);
        }
    }
}