using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : Character {

    /* Variables */
    private BossGun[] weaponArray;
    private BossGunCharacter[] weaponCharacterArray;
    private Sword sword;
    private BossPowerUp bossPowerUp;

    private Player player;

    [SerializeField]
    private BossManager bm;

    private bool isBossActive;
    private bool shouldShootConventionalWeapons;
    private bool shootingRay;
    private float timeToShootRay;

    [SerializeField]
    private GameObject RaySparkPS;
    [SerializeField]
    private GameObject RayThunderPS;

    [SerializeField]
    private float timeToShootRayDelay = 10;
    [SerializeField]
    private float rayCastTime = 4;
    [SerializeField]
    private int rayDamage = 30;
    [SerializeField]
    private GameObject killPSPrefab;

    // Fase del combate |
    // 0 -> Fase 1
    // 1 -> Fase 2
    // 2 -> Fase 3
    private int phase = 0;

    /* Métodos */
    private void Awake()
    {
        weaponArray = this.GetComponentsInChildren<BossGun>();
        weaponCharacterArray = this.GetComponentsInChildren<BossGunCharacter>();

        sword = this.GetComponentInChildren<Sword>();
        bossPowerUp = this.GetComponentInChildren<BossPowerUp>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        sword.gameObject.SetActive(false);
        bossPowerUp.gameObject.SetActive(false);

        SetInvulnerableWeapons(true);

        this.invulnerable = true;
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
        }
    }

    private IEnumerator ShootRay()
    {
        shouldShootConventionalWeapons = false;
        shootingRay = true;

        this.transform.LookAt(player.transform);
        //TODO: ParticleSystem que indique que se está cargando el rayo

        Debug.DrawRay(this.transform.position, this.transform.forward * 20, Color.red, 5);

        yield return new WaitForSeconds(rayCastTime / 2);

        RaySparkPS.SetActive(true);

        StartCoroutine(DisablePS(RaySparkPS, 2));

        yield return new WaitForSeconds(rayCastTime / 2);

        RayThunderPS.SetActive(true);

        StartCoroutine(DisablePS(RayThunderPS, 3));

        RaycastHit[] hitArray = Physics.SphereCastAll(this.transform.position, 3, this.transform.forward, Mathf.Infinity, -1, QueryTriggerInteraction.Ignore);

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
        
        Invoke("DeactivateShootingRay", 2);
    }

    private IEnumerator DisablePS(GameObject ps, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ps.SetActive(false);
    }

    private void DeactivateShootingRay()
    {
        shootingRay = false;
        timeToShootRay = timeToShootRayDelay + Time.time;
        shouldShootConventionalWeapons = true;
    }

    public void SetInvulnerableWeapons(bool newVal)
    {
        foreach (Character weaponCharacter in weaponCharacterArray)
        {
            weaponCharacter.SetInvulnerable(newVal);
        }
    }

    /// <summary>
    /// El mob muere
    /// </summary>
    private void Die()
    {
        bm.SetWallsMovesDown(true);

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

    public BossPowerUp GetBossPowerUp()
    {
        return bossPowerUp;
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
