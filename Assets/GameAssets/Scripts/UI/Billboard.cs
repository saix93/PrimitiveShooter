using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    /* Variables */
    // Cámara
    private Camera mainCamera;


    /* Métodos */

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        this.transform.rotation = mainCamera.transform.rotation;
    }
}
