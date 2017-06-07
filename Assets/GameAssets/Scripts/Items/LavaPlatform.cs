﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPlatform : MonoBehaviour {

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

        if (player != null)
        {
            MovePlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();

        if (character != null)
        {
            player = character;

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
        player.transform.position = player.transform.position + (player.transform.position - this.transform.position);
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
