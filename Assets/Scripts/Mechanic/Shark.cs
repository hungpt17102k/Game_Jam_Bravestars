using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour, IObjectPool
{
    public int idShark;

    private Boat _boat;
    private Vector3 _boatPos;

    private float _moveSpeed = 2f;

    private bool _isHit;

    public void OnDestroyObject()
    {
        gameObject.SetActive(false);
    }

    public void OnObjectReuse()
    {

    }

    // private void OnMouseDown() {
    //     EventManager.Instance.DestroyObstacleEvent(idShark);

    //     OnDestroyObject();
    // }

    private void Start() {
        _boat = GameManager.Instance.boat;
        _boatPos = _boat.transform.position;
    }

    private void Update() {
        if(!_isHit) {
            transform.position = Vector3.MoveTowards(transform.position, _boatPos, _moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.layer == 6 && !_isHit) {
            _isHit = true;

            print("hit boat");

            EventManager.Instance.SharkBiteEvent();

            EventManager.Instance.ShowHitBoxEvent(this.transform, idShark);
        }
    }
}
