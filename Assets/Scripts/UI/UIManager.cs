using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // -------------------------------------------Singleton---------------------
    public static UIManager Instance { get; private set; }

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

    [Header("All Panel Reference")]
    public GameObject menuPanel;

    [Space(10)]
    public GameObject gamePanel;

    [Space(10)]
    public GameObject winPanel;

    [Space(10)]
    public GameObject losePanel;


    //------------------------------------Unity Functions------------------------------------
    private void Start()
    {
        CloseGamePanel();
        CloseWinPanel();
        CloseLosePanel();

        ShowMenuPanel();

        // Add event to all panel
        AddEventAllPanel();

        AddEvent();
    }

    //------------------------------------Event of all Panel------------------------------------
    public void AddEventAllPanel()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            IPanelUI ipanel;

            if (transform.GetChild(i).TryGetComponent<IPanelUI>(out ipanel))
            {
                ipanel.AddEventPanel();
                ipanel.AddButtonEventPanel();
            }
        }
    }

    public void AddEvent() {
        EventManager.Instance.onStartGameEvent += () => {
            CloseMenuPanel();

            ShowGamePanel();
        };

        EventManager.Instance.onLoseEvent += () => {
            CloseGamePanel();

            ShowLosePanel();
        };

        EventManager.Instance.onResetGameEvent += () => {
            CloseWinPanel();
            CloseLosePanel();

            ShowMenuPanel();
        };
    }

    //------------------------------------UI Show/Close Panel Functions------------------------------------
    public void ShowMenuPanel()
    {
        menuPanel.SetActive(true);
    }

    public void CloseMenuPanel()
    {
        menuPanel.SetActive(false);
    }

    public void ShowGamePanel()
    {
        gamePanel.SetActive(true);
    }

    public void CloseGamePanel()
    {
        gamePanel.SetActive(false);
    }

    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
    }

    public void CloseWinPanel()
    {
        winPanel.SetActive(false);
    }

    public void ShowLosePanel()
    {
        losePanel.SetActive(true);
    }

    public void CloseLosePanel()
    {
        losePanel.SetActive(false);
    }

}
