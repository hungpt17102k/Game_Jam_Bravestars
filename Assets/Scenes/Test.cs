using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        print("Hit");
    }
}
