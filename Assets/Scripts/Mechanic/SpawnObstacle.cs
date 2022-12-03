using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SpawnObstacle : MonoBehaviour
{
    [Title("LIST SPAWN PROPERTY", bold: true, horizontalLine: true), Space(2)]
    public List<Transform> spawnPosList = new List<Transform>();

    [Title("SPAWN PROPERTY", bold: true, horizontalLine: true), Space(2)]
    [SerializeField] private float timeSpawnObs = 2f;
    private float _timeBtwSpawn = 0f;

    private ObjectPoolItems _objectPoolItem;
    private Vector3 _posSpawn;

    private bool _isShark = true;
    private GameObject _currentObs;
    private int _idObs = 1;

    private void Start() {
        AddEvent();

        this.enabled = false;
    }

    void Update()
    {   
        if(_currentObs != null) {
            return;
        }

        // Spawn obs
        if(_timeBtwSpawn >= timeSpawnObs) {
            Spawning();
            _timeBtwSpawn = 0;
        }
        else {
            _timeBtwSpawn += Time.deltaTime;
        }
    }

    private void AddEvent() {
        EventManager.Instance.onDestroyObsEvent += (id) => {
            ResetCurrentObs();
        };

        EventManager.Instance.onWinEvent += () => {
            DisableAllObs();

            this.enabled = false;
        };

        EventManager.Instance.onLoseEvent += () => {
            DisableAllObs();

            this.enabled = false;
        };

        EventManager.Instance.onStartGameEvent += () => {
            ResetCurrentObs();

            this.enabled = true;
        };
    }

    public void Spawning() {
        _objectPoolItem = _isShark ? ObjectPoolItems.Shark : ObjectPoolItems.Rock;
        _posSpawn = spawnPosList[Random.Range(0, spawnPosList.Count)].position;
        _currentObs = ObjectPooler.Instance.GetPooledObject(_objectPoolItem, _posSpawn, true);

        if(_isShark) {
            _currentObs.GetComponent<Shark>().idObs = _idObs;
        }
        else {
            _currentObs.GetComponent<Obstacle>().idObs = _idObs;
        }

        _isShark = !_isShark;

        _idObs++;
    }

    public void ResetCurrentObs() {
        _currentObs = null;
    }

    public void DisableAllObs() {
        ObjectPooler.Instance.disableAllPooled();
    }

}
