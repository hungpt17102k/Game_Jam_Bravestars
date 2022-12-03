using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public int idHitBox;

    public void PressHitBox() {
        EventManager.Instance.DestroyObstacleEvent(idHitBox);
        
        Destroy(this.gameObject);
    }
}
