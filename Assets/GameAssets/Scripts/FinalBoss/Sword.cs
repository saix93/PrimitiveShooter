using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    /* Variables */

    [SerializeField]
    private int swordDamage = 20;
    [SerializeField]
    private float actualAngle;
    [SerializeField]
    private float angularSpeed = 45;
    [SerializeField]
    private float radius;
    [SerializeField]
    private Transform rotationPoint;

    private bool shouldMove;

    /* Métodos */

    private void Start()
    {
        actualAngle = actualAngle * Mathf.Deg2Rad;
        shouldMove = true;
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

        this.transform.forward = -Vector3.Normalize(this.transform.position - rotationPoint.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();

        if (character)
        {
            character.ReceiveDamage(swordDamage);
        }
    }
}
