using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraControll : MonoBehaviour
{
    public Transform defaultTrans;
    public Transform zoomOutTrans;

    //------------------------------------Unity Functions----------------------------------
    private void Start()
    {
        AddEvent();
    }

    //------------------------------------Event of Camera----------------------------------

    public void AddEvent()
    {
        EventManager.Instance.onWinEvent += () => {
            ZoomOutCamera();
        };

        EventManager.Instance.onResetGameEvent += () => {
            ResetCamera();
        };
    }

    //------------------------------------Functions----------------------------------
    public void ZoomOutCamera() {
        transform.DOMove(zoomOutTrans.position, 5f).SetEase(Ease.Linear);
    }

    public void ResetCamera() {
        transform.position = defaultTrans.position;
    }
}
