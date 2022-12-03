using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;

public class LosePanel : MonoBehaviour, IPanelUI
{
    [Title("OBJECT UI", bold: true, horizontalLine: true), Space(2)]
    public Button resetButton;


    //------------------------------------Unity Functions----------------------------------
    private void Start()
    {

    }

    //------------------------------------Event of Panel----------------------------------

    public void AddEventPanel()
    {

    }

    public void AddButtonEventPanel()
    {
        resetButton.onClick.AddListener( () => {
            EventManager.Instance.ResetGameEvent();
        });
    }

    //------------------------------------Functions----------------------------------
}
