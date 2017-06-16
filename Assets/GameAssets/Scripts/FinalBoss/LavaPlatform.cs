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

    // BossManager
    [SerializeField]
    private BossManager bm;

    [SerializeField]
    private float upperPlatformLimit = -10f;
    [SerializeField]
    private float lowerPlatformLimit = -12f;
    private bool platformMovesDown;

    private Character player;
    private bool playerIsOn;

    private Vector3 previousPlatformPosition;

    public bool shouldMove;

    /* Métodos */

    private void Start()
    {
        actualAngle = actualAngle * Mathf.Deg2Rad;
    }

    private void Update()
    {
        if (shouldMove)
        {
            // Se modifica la posición de la plataforma para que rote alrededor de un punto concreto (rotationPoint)
            this.transform.position = rotationPoint.position + new Vector3(Mathf.Cos(actualAngle), 0, Mathf.Sin(actualAngle)) * radius;

            // Se modifica el angulo actual
            actualAngle += angularSpeed * Mathf.Deg2Rad * Time.deltaTime;
        }

        if (player)
        {
            MovePlayer();
        }

        MovePlatform();
    }

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();

        if (character)
        {
            player = character;

            previousPlatformPosition = this.transform.position;

            // Inicia el combate
            bm.StartBoss();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Character character = other.GetComponent<Character>();

        if (character)
        {
            player = null;
        }
    }

    /// <summary>
    /// Modififca la posición de la plataforma
    /// </summary>
    private void MovePlatform()
    {
        Vector3 position = this.transform.localPosition;

        if (platformMovesDown)
        {
            position += -this.transform.up * Time.deltaTime;

            position.y = Mathf.Max(position.y, lowerPlatformLimit);
        }
        else
        {
            position += this.transform.up * Time.deltaTime;

            position.y = Mathf.Min(position.y, upperPlatformLimit);
        }

        this.transform.localPosition = position;
    }

    /// <summary>
    /// Decide si la plataforma debe subir o bajar
    /// </summary>
    /// <param name="newVal"></param>
    public void SetPlatformMovement(bool newVal)
    {
        platformMovesDown = newVal;

        Invoke("RestartPlatformMovement", 5);
    }

    /// <summary>
    /// Reinicia la posición de la plataforma
    /// </summary>
    private void RestartPlatformMovement()
    {
        platformMovesDown = false;
    }

    /// <summary>
    /// Mueve al player junto a la plataforma
    /// </summary>
    private void MovePlayer()
    {
        Vector3 diff = this.transform.position - previousPlatformPosition;
        previousPlatformPosition = this.transform.position;

        player.transform.position += diff;
    }

}
