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

    private void Start() {
        AddEvent();
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
    }

    public void Spawning() {
        _objectPoolItem = _isShark ? ObjectPoolItems.Shark : ObjectPoolItems.Rock;

        _isShark = !_isShark;

        _posSpawn = spawnPosList[Random.Range(0, spawnPosList.Count)].position;

        _currentObs = ObjectPooler.Instance.GetPooledObject(_objectPoolItem, _posSpawn, true);
    }

    public void ResetCurrentObs() {
        _currentObs = null;
    }

}
