using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : Character {

    /* Variables */
    private BossGun[] weaponArray;

    private Player player;

    private bool isBossActive = true;
    private bool shouldShootConventionalWeapons = true;
    private bool shootingRay;
    private float timeToShootRay;
    [SerializeField]
    private float timeToShootRayDelay = 10;
    [SerializeField]
    private float rayCastTime = 4;
    [SerializeField]
    private int rayDamage = 30;

    private int phase = 1;

    /* Métodos */
    private void Awake()
    {
        weaponArray = this.GetComponentsInChildren<BossGun>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        StartPhase2();
    }

    private void Update()
    {
        if (!shootingRay)
        {
            Vector3 whereToLook = player.transform.position;
            whereToLook.y = this.transform.position.y;

            this.transform.LookAt(whereToLook);
        }

        if (isBossActive)
        {
            // Fases 1 y 2
            if (phase > 2)
            {
                if (shouldShootConventionalWeapons)
                {
                    // Dispara armas convencionales
                    foreach (BossGun weapon in weaponArray)
                    {
                        if (weapon)
                        {
                            weapon.ManageWeapon(player);
                        }
                    }
                }
            }

            // Fases 2 y 3
            if (phase > 0)
            {
                // Dispara el rayo
                if (!shootingRay && timeToShootRay < Time.time)
                {
                    StartCoroutine(ShootRay());
                }
            }
        }
    }

    private IEnumerator ShootRay()
    {
        shouldShootConventionalWeapons = false;
        shootingRay = true;

        this.transform.LookAt(player.transform);
        //TODO: ParticleSystem que indique que se está cargando el rayo

        Debug.DrawRay(this.transform.position, this.transform.forward * 20, Color.red, 5);

        yield return new WaitForSeconds(rayCastTime);

        //TODO: Revisar
        RaycastHit[] hitArray = Physics.RaycastAll(this.transform.position, player.transform.position, Mathf.Infinity, -1, QueryTriggerInteraction.Ignore);

        Debug.Log(hitArray);

        foreach (RaycastHit hit in hitArray)
        {
            if (hit.transform.CompareTag("Player"))
            {
                Player hitPlayer = hit.transform.GetComponent<Player>();
                hitPlayer.ReceiveDamage(rayDamage);
            }
            else
            {
                LavaPlatform platform = hit.transform.GetComponent<LavaPlatform>();

                if (platform)
                {
                    platform.SetPlatformMovement(true);
                }
            }
        }

        timeToShootRay = timeToShootRayDelay + Time.time;
        shouldShootConventionalWeapons = true;
        shootingRay = false;
    }

    /// <summary>
    /// Comienza la fase 2
    /// </summary>
    private void StartPhase2()
    {
        timeToShootRay = Time.time + timeToShootRayDelay;
    }
    
    /// <summary>
    /// Comienza la fase 3
    /// </summary>
    private void StartPhase3()
    {
        
    }

    /// <summary>
    /// Activa el boss
    /// </summary>
    public void ActivateBoss()
    {
        isBossActive = true;
        shouldShootConventionalWeapons = true;
    }
}
