using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Character {

    /* Variables */
    // Armas
    [SerializeField]
    private Weapon turretWeapon1;
    [SerializeField]
    private Weapon turretWeapon2;

    // LayerMask para ignorar raycasts
    [SerializeField]
    private LayerMask layerMask;
    
    // Tiempo que va a esperar la torreta antes de empezar a disparar al objetivo
    [SerializeField]
    private float timeToWaitUntilShoot = 2;

    // Sistema de partículas de explosión de torreta
    [SerializeField]
    private GameObject turretExplosionPSPrefab;

    // ¿Está el jugador a la vista?
    private bool isPlayerInSight;

    // ¿Debe la torreta disparar al enemigo?
    private bool shouldTurretFire = false;

    // Máximo rango del raycast
    private float raycastMaxDistance = 100;

    // Jugador
    private Player player;

    /* Métodos */

    private void Awake()
    {
        hasBlood = false;

        // raycastMaxDistance;
    }

    void FixedUpdate()
    {
        RaycastHit hitInfo;

        // Posición, dirección, color, duración
        Debug.DrawRay(this.transform.position + this.transform.forward, this.transform.forward * 20, Color.blue, 0.5f);

        if (Physics.Raycast(this.transform.position + this.transform.forward, this.transform.forward, out hitInfo, raycastMaxDistance, layerMask, QueryTriggerInteraction.Ignore))
        {
            isPlayerInSight = false;

            if (hitInfo.collider.CompareTag("Player"))
            {
                isPlayerInSight = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();

            shouldTurretFire = false;
            Invoke("ShouldFireTheEnemy", timeToWaitUntilShoot);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (player != null)
        {
            this.transform.LookAt(player.transform);

            if (isPlayerInSight && shouldTurretFire)
            {
                turretWeapon1.ManageWeapon(player);
                turretWeapon2.ManageWeapon(player);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
        }
    }

    private void ShouldFireTheEnemy()
    {
        shouldTurretFire = true;
    }

    /// <summary>
    /// La torreta explota
    /// </summary>
    private void Explode()
    {
        GameObject newExplosion = Instantiate(turretExplosionPSPrefab, this.transform.position, this.transform.rotation);

        Destroy(newExplosion, 2);

        Destroy(this.gameObject);
    }

    protected override void KillMe()
    {
        base.KillMe();

        Explode();
    }
}
