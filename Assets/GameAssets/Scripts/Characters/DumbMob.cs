using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbMob : Character {

    /* Variables */

    // Tiempo que tarda el Mob en explotar
    [SerializeField]
    private float timeToExplode = 2;

    // Daño que hace el mob al explotar
    [SerializeField]
    private int explosionDamage = 120;

    // Sistema de partículas de explosión
    [SerializeField]
    private GameObject explosionPSPrefab;

    // ¿Va a explotar?
    private bool isAboutToExplode = false;

    // ¿Está el jugador a rango?
    private bool playerIsOnRange = false;

    private Player player;

    /* Métodos */

    private void Update()
    {
        if (isAboutToExplode)
        {
            this.transform.LookAt(player.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            playerIsOnRange = true;

            if (!isAboutToExplode)
            {
                player = other.GetComponent<Player>();

                this.GetComponent<RandomMovement>().StopMoving();

                Invoke("Explode", timeToExplode);
                isAboutToExplode = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsOnRange = false;
        }
    }

    /// <summary>
    /// El mob explota
    /// </summary>
    private void Explode()
    {
        if (playerIsOnRange)
        {
            player.ReceiveDamage(explosionDamage);
        }

        GameObject newExplosion = Instantiate(explosionPSPrefab, this.transform.position, this.transform.rotation);

        Destroy(newExplosion, 2);

        Destroy(this.gameObject);
    }

    /// <summary>
    /// Función de Character sobreescrita
    /// </summary>
    protected override void KillMe()
    {
        base.KillMe();

        // MOB DOWN
        Explode();
    }

    /// <summary>
    /// Función de Character sobreescrita
    /// </summary>
    /// <param name="speedModifier"></param>
    public override void MoveFaster(float speedModifier)
    {
        base.MoveFaster(speedModifier);

        this.GetComponent<RandomMovement>().moveSpeed *= speedModifier;
    }

    /// <summary>
    /// Función de Character sobreescrita
    /// </summary>
    /// <param name="speedModifier"></param>
    public override void MoveSlower(float speedModifier)
    {
        base.MoveSlower(speedModifier);

        this.GetComponent<RandomMovement>().moveSpeed /= speedModifier;
    }
}
