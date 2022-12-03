using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public sealed class GameManager : MonoBehaviour
{
    // ---------------------------------------Singleton---------------------------
    public static GameManager Instance {get; private set;}

    private void Awake() {
        Application.targetFrameRate = 60;

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // ---------------------------------Variable of GameManager------------------------------
    [Title("SYSTEM CHECKING PROPERTY", bold: true, horizontalLine: true), Space(2)]
    public bool showFPS;
    public bool isConnectInternet;
    // public bool showAds;

    private RuntimePlatform _platformDevice;
    private float _frequency = 1.0f;
    private string _fps;


    // ------------------------------------Unity Function------------------------------
    private IEnumerator Start()
    {
        StartCoroutine(FPS());

        yield return new WaitForEndOfFrame();
    }

    // ------------------------------------System Checking Function------------------------------
    public bool IsMobileDevice()
    {
        _platformDevice = Application.platform;

        if (_platformDevice == RuntimePlatform.Android || _platformDevice == RuntimePlatform.IPhonePlayer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CheckInternetConnection() {
        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
            isConnectInternet = false;
        }
        else {
            isConnectInternet = true;
        }
    }

    private IEnumerator FPS() {
        for(;;){
            // Capture frame-per-second
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(_frequency);
            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;
           
            // Display it
            _fps = string.Format("FPS: {0}" , Mathf.RoundToInt(frameCount / timeSpan));
        }
    }

    private void OnGUI()
    {
        if(!showFPS) {
            return;
        }

        GUIStyle style = new GUIStyle();
        style.fontSize = 50;
        //style.fontStyle = FontStyle.Bold;

        GUI.Label(new Rect(Screen.width - 250,10,150,50), _fps, style);
    }

    private void OnApplicationFocus(bool focusStatus) {
        if(focusStatus) {
            CheckInternetConnection();
        }
    }

    // ------------------------------------Save Load System------------------------------


    // ------------------------------------Game Function------------------------------
}
