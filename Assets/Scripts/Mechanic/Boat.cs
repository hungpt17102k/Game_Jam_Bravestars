using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Boat : MonoBehaviour
{
    [Title("BOAT PROPERTY", bold: true, horizontalLine: true), Space(2)]
    [SerializeField] private float floatAmount = 0.05f;
    [SerializeField] private float sinkSpeed = 0.1f;

    [SerializeField] private GameObject winVFX;

    [Title("CHARACTER PROPERTY", bold: true, horizontalLine: true), Space(2)]
    [SerializeField] private Animator characterAni;

    // Get Set Variable
    public bool IsSinking {get; set;}
    private bool _boatSinked = false;
    private float _defaultSinkSpeed;

    private void Start() {
        _defaultSinkSpeed = sinkSpeed;
        IsSinking = false;
        characterAni.CrossFade("ScoopingWater", 0, 0);

        AddEvent();
    }

    private void Update() {
        if(IsSinking) {
            Sinking();
        }
    }

    private void AddEvent() {
        EventManager.Instance.onSharkBiteEvent += () => {
            sinkSpeed *= 7;
        };

        EventManager.Instance.onDestroyObsEvent += (id) => {
            sinkSpeed = _defaultSinkSpeed;
        };

        EventManager.Instance.onStartGameEvent += () => {
            IsSinking = true;

            SoundManager.Instance.playSound(AudioClips.WaterBucket);
        };

        EventManager.Instance.onWinEvent += () => {
            IsSinking = false;

            MoveToIsLand();
        };

        EventManager.Instance.onLoseEvent += () => {
            IsSinking = false;

            SinkDownToOcean();
        };

        EventManager.Instance.onResetGameEvent += () => {
            ResetBeginPosition();
        };
    }

    public void Floating() {
        float yPos = transform.position.y;
        float floatTo = yPos + floatAmount;
        
        // Max hight of boat
        floatTo = Mathf.Clamp(floatTo, -2f, 0f);

        transform.DOKill();
        transform.DOMoveY(floatTo, 0.5f).SetEase(Ease.Flash);
    }

    private void Sinking() {
        transform.position += Vector3.down * sinkSpeed * Time.deltaTime;

        if(transform.position.y <= -0.8f && !_boatSinked) {
            EventManager.Instance.LoseEvent();

            _boatSinked = true;
            IsSinking = false;
        }
    }

    public float WaterHeight() {
        return Extensions.ScaleValue(transform.position.y, -0.8f, 0f, 0f, 1f);
    }

    private void MoveToIsLand() {
        GameObject island = GameObject.FindGameObjectWithTag("Island");

        transform.DOMove(island.transform.GetChild(0).position, 6f)
            .OnComplete( () => {
                Vector3 pos = new Vector3(transform.position.x, 5f, transform.position.z);

                GameObject effect = Instantiate(winVFX, pos, Quaternion.identity);

                UIManager.Instance.ShowWinPanel();

                Destroy(effect, 3f);
            });
    }

    private void SinkDownToOcean() {

    }

    private void ResetBeginPosition() {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

}
