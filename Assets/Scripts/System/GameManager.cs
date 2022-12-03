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

    // ---------------------------------Game Mode------------------------------
    public enum GameMode {
        Float_The_Boat,
        Hit_Obs
    }

    // ---------------------------------Variable of GameManager------------------------------
    [Title("SYSTEM CHECKING PROPERTY", bold: true, horizontalLine: true), Space(2)]
    public bool showFPS;
    public bool isConnectInternet;
    // public bool showAds;

    private RuntimePlatform _platformDevice;
    private float _frequency = 1.0f;
    private string _fps;

    [Title("MANAGER REFERENCE PROPERTY", bold: true, horizontalLine: true), Space(2)]
    public InputManager input;
    public SpawnObstacle spawn;
    public GameMode gameMode;

    [Title("OBJECT SCENE PROPERTY", bold: true, horizontalLine: true), Space(2)]
    public Boat boat;

    [Title("GAME PROPERTY", bold: true, horizontalLine: true), Space(2)]
    public float timePlayMax = 30f;

    // Get Set
    public float TimePlay {get; set;}
    public int TimeMutipler {get; set;}

    private Coroutine _startGameCoroutine;

    // ------------------------------------Unity Function------------------------------
    private IEnumerator Start()
    {
        StartCoroutine(FPS());
        yield return new WaitForEndOfFrame();

        AddEvent();

        yield return new WaitForEndOfFrame();
    }


    // ------------------------------------Add Action Event------------------------------
    private void AddEvent() {
        EventManager.Instance.onSharkBiteEvent += () => {
            gameMode = GameMode.Hit_Obs;

            TimeMutipler = 0;
        };

        EventManager.Instance.onDestroyObsEvent += (id) => {
            gameMode = GameMode.Float_The_Boat;

            TimeMutipler = 1;
        };

        EventManager.Instance.onWinEvent += () => {
            input.firstTouchAction = null;

            StopCoroutine(_startGameCoroutine);
        };

        EventManager.Instance.onLoseEvent += () => {
            input.firstTouchAction = null;
        };

        EventManager.Instance.onStartGameEvent += () => {
            _startGameCoroutine = StartCoroutine(GamePlayTiming());

            AddInputEvent();
        };
    }

    private void AddInputEvent() {
        input.firstTouchAction = FirstTouchGameMode;
        input.holdTouchAction = null;
        input.endTouchAction = null;
    }

    private void FirstTouchGameMode() {
        if(gameMode == GameMode.Float_The_Boat) {
            ScoopingWater();
        }
        else {
            HitObstacle();
        }
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
    public void ScoopingWater() {
        boat.Floating();
    }

    public void HitObstacle() {
        print("Hit Obs");
    }

    public IEnumerator GamePlayTiming() {
        TimePlay = 0f;

        TimeMutipler = 1;

        while(TimePlay < timePlayMax) {
            TimePlay += Time.deltaTime * TimeMutipler;

            yield return null;
        }

        // Trigger win event
        EventManager.Instance.WinEvent();
    }

    public float GameTimeConvert() {
        return Extensions.ScaleValue(TimePlay, 0f, timePlayMax, 0f, 1f);
    }

    public void SlowTime(float amount = 0.2f) {
        Time.timeScale = amount;
    }

    public void ResortTime() {
        Time.timeScale = 1f;
    }
    
}
