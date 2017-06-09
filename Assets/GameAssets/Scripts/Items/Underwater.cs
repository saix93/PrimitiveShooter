using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underwater : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Name: " + other.name);
    }
}
