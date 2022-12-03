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

        print("win");
    }

    //----------------------------------------------------------------------------
    public event Action onLoseEvent;

    public void LoseEvent() {
        onLoseEvent?.Invoke();

        print("lose");
    }

    //----------------------------------------------------------------------------
    public event Action onSharkBiteEvent;

    public void SharkBiteEvent() {
        onSharkBiteEvent?.Invoke();
    }

    //----------------------------------------------------------------------------
    public event Action<int> onDestroyObsEvent;

    public void DestroyObstacleEvent(int idObs) {
        onDestroyObsEvent?.Invoke(idObs);
    }

    //----------------------------------------------------------------------------
    public event Action<Transform, int> onShowHitBoxEvent;

    public void ShowHitBoxEvent(Transform obsTrans, int idObs) {
        onShowHitBoxEvent?.Invoke(obsTrans, idObs);
    }

}
