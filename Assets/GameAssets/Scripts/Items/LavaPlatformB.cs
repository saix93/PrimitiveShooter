using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPlatformB : MonoBehaviour
{

    /* Variables */

    // Punto donde mira para girar
    [SerializeField]
    private Transform whereToLook;

    private int movementDirection = 1; // 0 -> Quieto. 1 -> Derecha. 2 -> Izquierda

    private Character player;
    private bool playerIsOn;

    /* Métodos */

    private void Update()
    {
        this.transform.LookAt(whereToLook);


        MovePlatform(movementDirection);
    }

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();

        if (character != null)
        {
            Vector3 globalRotation = character.transform.eulerAngles;
            Vector3 platfomRelativeRotation = this.transform.InverseTransformDirection(globalRotation);

            Debug.Log("Global: " + globalRotation);
            Debug.LogError("Local: " + platfomRelativeRotation);

            character.transform.SetParent(this.transform, true);
            character.transform.localEulerAngles = platfomRelativeRotation;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Character character = other.GetComponent<Character>();

        if (character != null)
        {
            Quaternion rotation = character.transform.rotation;
            character.transform.SetParent(null, true);
            character.transform.rotation = rotation;
        }
    }

    /// <summary>
    /// Mueve la plataforma
    /// </summary>
    /// <param name="direction"></param>
    private void MovePlatform(int direction)
    {
        switch (direction)
        {
            case 1: MoveRight(); break;
            case 2: MoveLeft(); break;
        }
    }

    /// <summary>
    /// Mueve a la derecha
    /// </summary>
    private void MoveRight()
    {
        this.transform.position += this.transform.right / 4;
    }

    /// <summary>
    /// Mueve a la izquierda
    /// </summary>
    private void MoveLeft()
    {
        this.transform.position += -this.transform.right / 4;
    }

}
