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

    // Velocidad de rotación
    [SerializeField]
    private float rotationSpeed = 90;

    // LayerMask para ignorar raycasts
    [SerializeField]
    private LayerMask layerMask;

    // Máximo rango del raycast
    [SerializeField]
    private float turretRange = 75;

    // Sistema de partículas de explosión de torreta
    [SerializeField]
    private GameObject turretExplosionPSPrefab;

    // ¿Está el jugador a la vista?
    private bool isPlayerInSight = false;

    // ¿Está la torreta mirando al jugador?
    private bool isTurretLookingAtPlayer = false;

    // Jugador
    private GameObject player;

    /* Métodos */

    private void Awake()
    {
        hasBlood = false;

        player = GameObject.FindGameObjectWithTag("Player");

        // raycastMaxDistance;
    }

    private void Update()
    {
        if (isTurretLookingAtPlayer)
        {
            turretWeapon1.ManageWeapon(player.GetComponent<Player>());
            turretWeapon2.ManageWeapon(player.GetComponent<Player>());
        }
    }

    void FixedUpdate()
    {
        RaycastHit hitInfo;

        Vector3 shootingPoint = this.transform.position + this.transform.forward;
        Vector3 directionToPlayer = player.transform.position - shootingPoint;

        // Posición, dirección, color, duración
        Debug.DrawRay(shootingPoint, directionToPlayer, Color.blue, 0.2f);
        Debug.DrawRay(shootingPoint, this.transform.forward * 20, Color.green, 0.2f);

        isPlayerInSight = false;
        isTurretLookingAtPlayer = false;

        if (Physics.Raycast(shootingPoint, directionToPlayer, out hitInfo, turretRange, layerMask, QueryTriggerInteraction.Ignore))
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                // Aplicar una rotación poco a poco:
                Quaternion rotationLookAtPlayer = Quaternion.LookRotation(directionToPlayer);
                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotationLookAtPlayer, rotationSpeed * Time.deltaTime);

                isPlayerInSight = true;

                // this.transform.LookAt(player.transform);
                if (Physics.Raycast(shootingPoint, this.transform.forward, out hitInfo, turretRange, layerMask, QueryTriggerInteraction.Ignore))
                {
                    if (hitInfo.collider.CompareTag("Player"))
                    {
                        isTurretLookingAtPlayer = true;
                    }
                }
            }
        }
        
        // Cuando el player no está a la vista del raycast de la torreta
        if (!isPlayerInSight)
        {
            this.transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
        }
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
