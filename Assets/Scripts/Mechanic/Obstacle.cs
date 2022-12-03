using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IObjectPool
{
    public int idObs;

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

    private void Start() {
        _boat = GameManager.Instance.boat;
        _boatPos = new Vector3(_boat.transform.position.x, 0, _boat.transform.position.z);

        AddEvent();
    }

    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, _boatPos, _moveSpeed * Time.deltaTime);
    }

    private void AddEvent() {
        EventManager.Instance.onDestroyObsEvent += (id) => {
            if(id == this.idObs) {
                OnDestroyObject();
            }
        };
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.layer == 6) {
            OnDestroyObject();

            EventManager.Instance.LoseEvent();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == 8) {
            EventManager.Instance.ShowHitBoxEvent(this.transform, idObs);
        }
    }
}
