using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    private Action _onInputAction;

    private bool _isOnMobile;

    private int _inputMouse = 0;

    // Action input
    [Header("User Input Action")]
    public Action firstTouchAction;
    public Action holdTouchAction;
    public Action endTouchAction;

    private void Start() {
        InitInputSetting();

        AddEvent();
    }

    private void Update() {
        _onInputAction();
    }

    //--------------------------------Initialize------------------------------
    private void InitInputSetting() {
        _isOnMobile = GameManager.Instance.IsMobileDevice();

        // Check input setting
        if(_isOnMobile) {
            _onInputAction = VirtualInput;
        }
        else
        {
            _onInputAction = PhysicInput;
        }
    }

    private void AddEvent() {
        // // Add to enter level event
        // EventManager.Instance.onEnterLevelEvent += () => {
        //     this.enabled = true;
        // };

        // // Add to win event
        // EventManager.Instance.onWinEvent[EventManager.WinAction.Pass_Finish_Line] += () => {
        //     this.enabled = false;
        // };

        // // Add to lose event
        // EventManager.Instance.onLoseEvent[EventManager.LoseAction.Hit_Obs] += () => {
        //     this.enabled = false;
        // };

        // // Add to continue level event
        // EventManager.Instance.onContinueLevelEvent += () => {
        //     this.enabled = true;
        // };
    }

    //--------------------------------Physic Input------------------------------
    private void PhysicInput() {
        if(Input.GetMouseButtonDown(_inputMouse)) {
            firstTouchAction?.Invoke();
        }
        else if(Input.GetMouseButton(_inputMouse)) {
            holdTouchAction();
        }
        else if(Input.GetMouseButtonUp(_inputMouse)) {
            endTouchAction?.Invoke();
        }
    }

    //--------------------------------Virtual Input------------------------------
    private void VirtualInput() {
        if(Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            switch(touch.phase) {
                case TouchPhase.Began:
                    firstTouchAction();
                    break;

                case TouchPhase.Stationary:
                    holdTouchAction();
                    break;

                case TouchPhase.Moved:
                    holdTouchAction();
                    break;
                
                case TouchPhase.Ended:
                    endTouchAction();
                    break;
            }
        }
    }
}
