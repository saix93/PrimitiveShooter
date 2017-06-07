using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour {

    /* Variables */
    // CharacterController
    private CharacterController myCC;

    // Velocidad de movimiento
    [SerializeField]
    public float moveSpeed = 5;

    // Tiempo que tarda en cambiar de dirección
    [SerializeField]
    private float timeToChangeDirection = 3;

    private bool shouldMove = true;

    /* Métodos */

    private void Awake()
    {
        myCC = this.GetComponent<CharacterController>();
    }

    private void Start()
    {
        InvokeRepeating("ChangeDirection", 0, timeToChangeDirection);
    }

    // Update is called once per frame
    void Update () {
        if (shouldMove)
        {
            myCC.SimpleMove(this.transform.forward * moveSpeed);
        }
    }

    private void ChangeDirection()
    {
        // Se crea una dirección aleatoria
        Vector2 randomDirection = Random.insideUnitCircle;
        Vector3 xzDirection = new Vector3(randomDirection.x, 0, randomDirection.y);

        // Se normaliza el vector de la dirección
        xzDirection.Normalize();

        // Se modifica el forward del transform para que el objeto esté mirando hacia la nueva dirección
        this.transform.forward = xzDirection;
    }

    public void StopMoving()
    {
        shouldMove = false;

        CancelInvoke("ChangeDirection");
    }
}
