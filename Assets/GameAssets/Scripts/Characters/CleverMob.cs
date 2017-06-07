using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleverMob : Character {

    /* Variables */

    // Daño que hace el mob al explotar
    [SerializeField]
    private int mobDamage = 20;

    // ¿Está el player a rango de ataqu básico?
    private bool isPlayerOnRange = false;

    // Momento a partir del cual se puede dar un golpe
    private float timeToHit;

    // Cuánto cooldown tiene el hit del mob
    private float hitCooldown = 2;

    // Distancia a partir de la cual el mob debe perseguir al jugador
    [SerializeField]
    private float distanceToFollowPlayer = 25;

    // Prefab de la explosión que hace el mob al morir
    [SerializeField]
    private GameObject killPSPrefab;

    // Player
    private Player player;

    // Scripts de movimiento del personaje
    private RandomMovement randomMovement;
    private FixedMovement fixedMovement;

    /* Métodos */
    private void Awake()
    {
        timeToHit = Time.time;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        randomMovement = GetComponent<RandomMovement>();
        fixedMovement = GetComponent<FixedMovement>();

        randomMovement.enabled = true;
        fixedMovement.enabled = false;
    }

    private void Update()
    {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);

        if (distance < distanceToFollowPlayer)
        {
            randomMovement.enabled = false;
            fixedMovement.enabled = true;

            if (isPlayerOnRange)
            {
                if (Time.time >= timeToHit)
                {
                    player.ReceiveDamage(mobDamage);

                    timeToHit = Time.time + hitCooldown;
                }
            }

            isPlayerOnRange = false;
        }
        else
        {
            randomMovement.enabled = true;
            fixedMovement.enabled = false;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Player"))
        {
            isPlayerOnRange = true;
        }
    }

    private void ChangeMovementBehaviour()
    {
        this.GetComponent<RandomMovement>().enabled = !this.GetComponent<RandomMovement>().enabled;
        this.GetComponent<FixedMovement>().enabled = !this.GetComponent<FixedMovement>().enabled;
    }

    /// <summary>
    /// El mob muere
    /// </summary>
    private void Die()
    {
        GameObject killPS = Instantiate(killPSPrefab, this.transform.position, this.transform.rotation);

        Destroy(killPS, 2);

        Destroy(this.gameObject);
    }

    /// <summary>
    /// Función de Character sobreescrita
    /// </summary>
    protected override void KillMe()
    {
        base.KillMe();

        // MOB DOWN
        Die();
    }

    /// <summary>
    /// Función de Character sobreescrita
    /// </summary>
    /// <param name="speedModifier"></param>
    public override void MoveFaster(float speedModifier)
    {
        base.MoveFaster(speedModifier);

        this.GetComponent<RandomMovement>().moveSpeed *= speedModifier;
        this.GetComponent<FixedMovement>().moveSpeed *= speedModifier;
    }

    /// <summary>
    /// Función de Character sobreescrita
    /// </summary>
    /// <param name="speedModifier"></param>
    public override void MoveSlower(float speedModifier)
    {
        base.MoveSlower(speedModifier);

        this.GetComponent<RandomMovement>().moveSpeed /= speedModifier;
        this.GetComponent<FixedMovement>().moveSpeed /= speedModifier;
    }
}
