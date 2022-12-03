using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HitBox : MonoBehaviour
{
    public int idHitBox;

    public Transform circle;

    private bool _canHit;

    private void OnEnable()
    {
        ResetHitBox();

        WaitTimeToHit();
    }

    private void OnDisable()
    {
        circle.DOKill();
    }

    public void PressHitBox()
    {
        if (_canHit)
        {
            EventManager.Instance.DestroyObstacleEvent(idHitBox);

            Destroy(this.gameObject);
        }
        else
        {
            print("hit Fail");

            FailHitBox();
        }
    }

    public void ResetHitBox()
    {
        _canHit = false;

        circle.localScale = Vector3.one;
    }

    public void WaitTimeToHit()
    {
        circle.DOScale(Vector3.one * 0.2f, 2.5f)
            .SetUpdate(true)
            .OnUpdate(() =>
            {
                if (circle.localScale.x < 0.5f)
                {
                    _canHit = true;
                }
                else
                {
                    _canHit = false;
                }
            })
            .OnComplete(() =>
            {
                FailHitBox();
            });
    }

    public void FailHitBox()
    {
        if (GameManager.Instance.spawn.IsShark())
        {
            EventManager.Instance.DestroyObstacleEvent(idHitBox);

            GameManager.Instance.ResortTime();

            Destroy(this.gameObject);
        }
        else
        {
            EventManager.Instance.DestroyObstacleEvent(idHitBox);

            GameManager.Instance.ResortTime();

            Destroy(this.gameObject);
        }
    }

    
}
