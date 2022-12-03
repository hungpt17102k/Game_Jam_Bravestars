using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IObjectPool
{
    private Boat _boat;
    private Vector3 _boatPos;

    private float _moveSpeed = 2f;

    public void OnDestroyObject()
    {
        gameObject.SetActive(false);
    }

    public void OnObjectReuse()
    {

    }

    // private void OnMouseDown() {
    //     // EventManager.Instance.DestroyObstacleEvent();

    //     OnDestroyObject();
    // }

    private void Start() {
        _boat = GameManager.Instance.boat;
        _boatPos = _boat.transform.position;
    }

    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, _boatPos, _moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.layer == 6) {
            OnDestroyObject();

            EventManager.Instance.LoseEvent();
        }
    }
}
