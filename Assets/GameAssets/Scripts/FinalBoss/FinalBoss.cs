using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : Character {

    /* Variables */
    private BossGun[] weaponArray;
    private Sword sword;

    private Player player;

    private bool isBossActive;
    private bool shouldShootConventionalWeapons;
    private bool shootingRay;
    private float timeToShootRay;
    [SerializeField]
    private float timeToShootRayDelay = 10;
    [SerializeField]
    private float rayCastTime = 4;
    [SerializeField]
    private int rayDamage = 30;

    // Fase del combate |
    // 0 -> Fase 1
    // 1 -> Fase 2
    // 2 -> Fase 3
    private int phase = 0;

    /* Métodos */
    private void Awake()
    {
        weaponArray = this.GetComponentsInChildren<BossGun>();

        sword = this.GetComponentInChildren<Sword>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        sword.gameObject.SetActive(false);
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
            if (phase < 2)
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

            // Fase 3
            if (phase > 1)
            {

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

        RaycastHit[] hitArray = Physics.RaycastAll(this.transform.position, this.transform.forward, Mathf.Infinity, -1, QueryTriggerInteraction.Ignore);

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
    /// Activa el boss
    /// </summary>
    public void ActivateBoss()
    {
        isBossActive = true;
        shouldShootConventionalWeapons = true;
    }

    /* Getters - Setters */
    public Sword GetSword()
    {
        return sword;
    }

    public float GetTimeToShootRayDelay()
    {
        return timeToShootRayDelay;
    }

    public int GetPhase()
    {
        return phase;
    }

    public void SetPhase(int newPhase)
    {
        phase = newPhase;
    }

    public void SetTimeToShootRay(float newTime)
    {
        timeToShootRay = newTime;
    }
}
