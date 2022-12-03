using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    
    //----------------------------------------------------------------------------
    public event Action onStartGameEvent;
    
    public void StartGameEvent() {
        onStartGameEvent?.Invoke();
    }

    //----------------------------------------------------------------------------
    public event Action onWinEvent;

    public void WinEvent() {
        onWinEvent?.Invoke();
    }

    //----------------------------------------------------------------------------
    public event Action onLoseEvent;

    public void LoseEvent() {
        onLoseEvent?.Invoke();
    }
}
