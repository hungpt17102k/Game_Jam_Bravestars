using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;
using TMPro;

public class GamePanel : MonoBehaviour, IPanelUI
{
    [Title("OBJECT UI", bold: true, horizontalLine: true), Space(2)]
    public RectTransform hitBoxTrans;
    public Image waterProcessImg;
    public Image gameProcessImg;
    public RectTransform boatIcon;
    public Image finishIcon;
    public TextMeshProUGUI scoreTxt;


    //------------------------------------Unity Functions----------------------------------
    private void Start()
    {

    }

    private void Update() {
        WaterProcess();

        GameProcess();
    }

    //------------------------------------Event of Panel----------------------------------

    public void AddEventPanel()
    {
        EventManager.Instance.onShowHitBoxEvent += ApearHitBox;

        EventManager.Instance.onStartGameEvent += () => {
            finishIcon.gameObject.SetActive(true);
        };

        EventManager.Instance.onWinEvent += () => {
            finishIcon.gameObject.SetActive(false);
        };

        EventManager.Instance.onScorePointEvent += UpdateScore;
    }

    public void AddButtonEventPanel()
    {

    }

    //------------------------------------Functions----------------------------------
    public void ApearHitBox(Transform obs, int idObs)
    {
        Vector2 hitboxUIPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, obs.position);

        RectTransform hitBox = Instantiate(hitBoxTrans) as RectTransform;
        hitBox.SetParent(this.transform);
        hitBox.SetAsFirstSibling();
        hitBox.gameObject.SetActive(true);

        // print(hitboxUIPosition);

        hitBox.transform.localScale = Vector3.one;
        hitBox.transform.localRotation = Quaternion.identity;
        hitBox.transform.localPosition = new Vector3(hitboxUIPosition.x - Screen.width * 0.5f, hitboxUIPosition.y - Screen.height * 0.5f, 0f);
        hitBox.GetComponent<HitBox>().idHitBox = idObs;
    }

    public void WaterProcess() {
        waterProcessImg.fillAmount = 1f - GameManager.Instance.boat.WaterHeight();
    }

    public void GameProcess() {
        gameProcessImg.fillAmount = GameManager.Instance.GameTimeConvert();

        boatIcon.localPosition = Vector3.right * Extensions.ScaleValue(gameProcessImg.fillAmount * 100, 0f, 100f, -335f, 325f);
    }

    public void UpdateScore(int amount) {
        GameManager.Instance.score += amount;
        scoreTxt.text = GameManager.Instance.score.ToString();
    }


}
