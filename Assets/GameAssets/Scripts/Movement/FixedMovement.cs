using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedMovement : MonoBehaviour {

    /* Variables */
    // CharacterController
    private CharacterController myCC;

    // Velocidad de movimiento
    [SerializeField]
    public float moveSpeed = 7;

    // Player
    private Player player;

    /* Métodos */

    private void Awake()
    {
        myCC = this.GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(player.transform);

        myCC.SimpleMove(this.transform.forward * moveSpeed);
    }
}
