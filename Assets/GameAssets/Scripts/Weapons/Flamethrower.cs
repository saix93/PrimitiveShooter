using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Weapon {

    /* Variables */
    // Sistema de partículas de las llamas
    [SerializeField]
    private GameObject flamePSPrefab;

    // Layer mask
    [SerializeField]
    private LayerMask layerMask;

    // Radio del ataque
    [SerializeField]
    private float attackRadius = 2;

    // Distancia del ataque
    [SerializeField]
    private float attackDistance = 10;

    // Sonidos
    // Firing - Start
    private AudioSource firingStartAudio;
    // Firing
    private AudioSource firingAudio;
    // Firing - End
    private AudioSource firingEndAudio;

    // Objeto del sistema de partículas
    private GameObject flamePS;

    private bool hasToInstantiateNewPS = true;
    private bool isFiring = false;

    private bool isPlayingFiringAudio = false;

    /* Métodos */

    private void Awake()
    {
        firingStartAudio = transform.Find("Sounds/FiringStart").GetComponent<AudioSource>();
        firingAudio = transform.Find("Sounds/Firing").GetComponent<AudioSource>();
        firingEndAudio = transform.Find("Sounds/FiringEnd").GetComponent<AudioSource>();
        reloadAudio = transform.Find("Sounds/Reload").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isFiring)
        {
            if (!isPlayingFiringAudio)
            {
                firingAudio.Play();

                isPlayingFiringAudio = true;
            }
        }
        else
        {
            firingAudio.Stop();
                
            isPlayingFiringAudio = false;
        }
    }

    public override void ManageWeapon()
    {
        base.ManageWeapon();

        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            // TODO: Reproducir sólo cuando puede disparar (no está recargando)
            firingEndAudio.Play();

            isFiring = false;
            DestroyFlameParticleSystem();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            // TODO: Reproducir sólo cuando puede disparar (no está recargando)
            firingStartAudio.Play();

            DestroyFlameParticleSystem();
        }
    }

    /// <summary>
    /// Dispara el lanzallamas
    /// </summary>
    protected override void Shoot()
    {

        if (Time.time < timeToShoot)
        {
            DestroyFlameParticleSystem();

            if (currentClipAmmo == maxClipAmmo)
            {
                isFiring = false;
            }

            return;
        }

        if (!infiniteAmmo && currentClipAmmo <= 0)
        {
            isFiring = false;

            Reload(); // TODO: Reproducir sonido cuando no hay munición en el cargador
            return;
        }

        isFiring = true;

        if (hasToInstantiateNewPS)
        {
            flamePS = Instantiate(flamePSPrefab, shootingPoint.position, shootingPoint.rotation);

            flamePS.transform.SetParent(shootingPoint.transform);

            hasToInstantiateNewPS = false;
        }

        if (!infiniteAmmo)
        {
            currentClipAmmo--;
        }
        timeToShoot = Time.time + firerate;

        RaycastHit[] hits = Physics.SphereCastAll(shootingPoint.position, attackRadius, shootingPoint.forward, attackDistance, layerMask, QueryTriggerInteraction.Ignore);

        foreach (RaycastHit hit in hits)
        {
            Character character = hit.collider.GetComponent<Character>();
            if (character && !character.CompareTag("Player"))
            {
                character.ReceiveDamage(weaponDamage);
            }
        }
    }

    private void DestroyFlameParticleSystem()
    {
        if (hasToInstantiateNewPS)
        {
            return;
        }

        ParticleSystem[] flamePSArray = flamePS.GetComponentsInChildren<ParticleSystem>();

        for (int i = 0; i < flamePSArray.Length; i++)
        {
            flamePSArray[i].Stop();
        }

        hasToInstantiateNewPS = true;

        Destroy(flamePS, 2);
    }
}
