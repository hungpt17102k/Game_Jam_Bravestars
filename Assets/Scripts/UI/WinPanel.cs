using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;

public class WinPanel : MonoBehaviour
{
    [Title("OBJECT UI", bold: true, horizontalLine: true), Space(2)]
    public Button playButton;


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
        playButton.onClick.AddListener( () => {

        });
    }

    //------------------------------------Functions----------------------------------
}
