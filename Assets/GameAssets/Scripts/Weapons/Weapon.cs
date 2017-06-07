using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {

    /* Variables */
    // ID del arma
    [SerializeField]
    private int weaponID;

    // Munición
    [SerializeField]
    protected int maxAmmo = 200;
    [SerializeField]
    protected int currentAmmo = 100;
    [SerializeField]
    protected int maxClipAmmo = 10;
    [SerializeField]
    protected int currentClipAmmo = 10;

    // Punto donde aparecerán las balas
    [SerializeField]
    protected Transform shootingPoint;

    // Punto donde aparecerán los casquillos de bala
    [SerializeField]
    protected Transform bulletshellPoint;

    // Bala
    [SerializeField]
    protected Rigidbody bulletPrefab;

    // Casquillo de bala
    [SerializeField]
    protected Rigidbody bulletshellPrefab;

    //Fuerza con la que sale disparado el bulletshell
    [SerializeField]
    protected float minBulletshellForce = 30;
    [SerializeField]
    protected float maxBulletshellForce = 70;

    // ¿Debe usar gravedad la bala?
    [SerializeField]
    protected bool shouldBulletUseGravity = true;

    // Velocidad de la bala
    [SerializeField]
    protected float shootForce = 50;

    // Daño del arma
    [SerializeField]
    protected int weaponDamage = 25;

    // Texto donde mostrar la munición
    protected Text ammoInfoText;

    // Tiempo por cada disparo
    [SerializeField]
    public float firerate = 0.4f;

    // Tiempo que el soldado tarda en recargar el arma
    [SerializeField]
    public float reloadTime = 0.5f;

    // ¿Munición infinita?
    [SerializeField]
    public bool infiniteAmmo = false;

    // Instante de tiempo a partir del cual podré disparar
    private float timeToShoot = 0;

    // Sonidos
    private AudioSource fireAudio;
    private AudioSource reloadAudio;


    /* Métodos */

    private void Awake()
    {
        fireAudio = transform.Find("Sounds/Fire").GetComponent<AudioSource>();
        reloadAudio = transform.Find("Sounds/Reload").GetComponent<AudioSource>();
    }
            
    private void Update()
    {
        // TODO: Cambiar a un script aplicado en el propio texto del canvas
        ammoInfoText = GameObject.FindGameObjectWithTag("CanvasAmmoText").GetComponent<Text>();
        ammoInfoText.text = String.Format("{0} / {1} | {2}", currentClipAmmo, maxClipAmmo, currentAmmo);
        ammoInfoText.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
    }

    /// <summary>
    /// Dispara
    /// </summary>
    protected void Shoot()
    {
        if (Time.time < timeToShoot)
        {
            return;
        }

        if (!infiniteAmmo && currentClipAmmo <= 0)
        {
            Reload();
            return;
        }
        
        fireAudio.PlayOneShot(fireAudio.clip);

        currentClipAmmo--;
        timeToShoot = Time.time + firerate;

        // Se crea la bala
        Rigidbody newBullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);

        newBullet.useGravity = shouldBulletUseGravity;

        // Se crea el casquillo de la bala
        Rigidbody newBulletshell = Instantiate(bulletshellPrefab, bulletshellPoint.position, bulletshellPoint.rotation);

        // Se añade una fuerza al casquillo de la bala
        newBulletshell.AddForce(newBulletshell.transform.right * UnityEngine.Random.Range(minBulletshellForce, maxBulletshellForce));
        newBulletshell.AddForce(newBulletshell.transform.up * UnityEngine.Random.Range(minBulletshellForce, maxBulletshellForce));

        // Se añade daño a la bala
        newBullet.GetComponent<Bullet>().SetBulletDamage(weaponDamage);

        // Se añade una fuerza a la bala
        newBullet.AddForce(this.transform.forward * shootForce, ForceMode.Impulse);
    }

    /// <summary>
    /// Maneja el arma
    /// </summary>
    public virtual void ManageWeapon()
    {
        // Se sobreescribe en clases de armas concretas
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    /// <summary>
    /// Maneja el arma
    /// </summary>
    /// <param name="target"></param>
    public virtual void ManageWeapon(Player target)
    {
        // Se sobreescribe en clases de armas concretas
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
    }

    /// <summary>
    /// Recarga el arma
    /// </summary>
    protected void Reload()
    {
        // Si no hay munición
        if (currentAmmo == 0 || currentClipAmmo == maxClipAmmo || currentAmmo == 0 && currentClipAmmo == 0)
        {
            return;
        }

        reloadAudio.Play();
        int bulletsToLoad = maxClipAmmo - currentClipAmmo;

        currentAmmo -= bulletsToLoad;

        // Si la munición total actual es menor que 0 (Tenía 8 y he recargado un cargador de 10)
        if (currentAmmo < 0)
        {
            bulletsToLoad += currentAmmo;

            // La munición total actual es 0
            currentAmmo = 0;
        }

        currentClipAmmo += bulletsToLoad;

        timeToShoot = Time.time + reloadTime;
    }

    public int GetWeaponId()
    {
        return weaponID;
    }
}
