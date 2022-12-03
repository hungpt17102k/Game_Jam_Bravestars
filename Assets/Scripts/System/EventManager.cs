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
    public event Action<int, int> onRopeCuttedEvent;

    public void RopeCuttedEvent(int ropeId, int linkId) {
        onRopeCuttedEvent?.Invoke(ropeId, linkId);
    }
}
