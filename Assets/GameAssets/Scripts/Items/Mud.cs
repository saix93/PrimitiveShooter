using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud : MonoBehaviour {

    /* Variables */
    // Modificador de velocidad que se aplica al entrar en el barro
    [SerializeField]
    private float speedModifier = 2;

    /* Métodos */

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Character>() != null)
        {
            other.gameObject.GetComponent<Character>().MoveSlower(speedModifier);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Character>() != null)
        {
            other.gameObject.GetComponent<Character>().MoveFaster(speedModifier);
        }
    }

}
