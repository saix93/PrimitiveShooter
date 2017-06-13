using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAudio : MonoBehaviour {

    /* Variables */
    [SerializeField]
    private float timeToDestroy = 3;

    /* Métodos */
    private void Start()
    {
        Destroy(this.gameObject, timeToDestroy);
    }
}
