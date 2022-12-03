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

    // Get Set Variable
    public bool IsSinking {get; set;}
    private bool _boatSinked = false;

    private void Start() {
        IsSinking = true;
    }

    private void Update() {
        if(IsSinking) {
            Sinking();
        }
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

        if(transform.position.y <= 2f && !_boatSinked) {
            EventManager.Instance.LoseEvent();
            _boatSinked = true;
        }
    }

}
