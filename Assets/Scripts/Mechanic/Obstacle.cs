using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IObstacle
{
    public void OnDestroyObject()
    {
        gameObject.SetActive(false);
    }

    public void OnObjectReuse()
    {

    }

    private void OnMouseDown() {
        print("hit");

        OnDestroyObject();
    }
}
