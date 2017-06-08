using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPlatform : MonoBehaviour {

    /* Variables */

    // Punto donde mira para girar
    [SerializeField]
    private Transform rotationPoint;

    // Velocidad angular del giro en grados
    [SerializeField]
    private float angularSpeed = 10;

    // Radio de la circunferencia
    [SerializeField]
    private float radius = 15;

    // Angulo actual
    [SerializeField]
    private float actualAngle;

    private int movementDirection = 1; // 0 -> Quieto. 1 -> Derecha. 2 -> Izquierda

    private Character player;
    private bool playerIsOn;

    private Vector3 previousPlatformPosition;

    /* Métodos */

    private void Update()
    {

        if (player != null)
        {
            // Se modifica la posición de la plataforma para que rote alrededor de un punto concreto (rotationPoint)
            this.transform.position = rotationPoint.position + new Vector3(Mathf.Cos(actualAngle), 0, Mathf.Sin(actualAngle)) * radius;

            // Se modifica el angulo actual
            actualAngle += angularSpeed * Mathf.Deg2Rad * Time.deltaTime;

            MovePlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();

        if (character != null)
        {
            player = character;

            previousPlatformPosition = this.transform.position;

            Debug.Log("Player position: " + player.transform.position);
            Debug.Log("Platform position: " + this.transform.position);
            Debug.Log("Player - platform position: " + (player.transform.position - this.transform.position));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Character character = other.GetComponent<Character>();

        if (character != null)
        {
            player = null;
        }
    }

    private void MovePlayer()
    {
        Vector3 diff = this.transform.position - previousPlatformPosition;
        previousPlatformPosition = this.transform.position;

        player.transform.position += diff;
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
        this.transform.position += this.transform.right / 4 * Time.deltaTime;
    }

    /// <summary>
    /// Mueve a la izquierda
    /// </summary>
    private void MoveLeft()
    {
        this.transform.position += -this.transform.right / 4 * Time.deltaTime;
    }

}
