using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour {

    /* Variable */
    // Prefab de la granada
    [SerializeField]
    private Rigidbody grenadePrefab;

    // Fuerza de la granada
    [SerializeField]
    private float launchForce = 10;

    // Número de granadas actual
    [SerializeField]
    private int currentGrenades = 4;

    // Número máximo de granadas
    [SerializeField]
    private int maxGrenades = 8;

    /* Métodos */

    private void Update()
    {
        if (currentGrenades > 0 && Input.GetButtonDown("Fire2"))
        {
            Shoot();
        }
    }

    /// <summary>
    /// Lanza una granada
    /// </summary>
    private void Shoot()
    {
        Rigidbody newGrenade = Instantiate(grenadePrefab, this.transform.position, this.transform.rotation);

        newGrenade.AddForce(this.transform.forward * launchForce, ForceMode.Impulse);

        currentGrenades--;
    }

    /// <summary>
    /// Añade una granada
    /// </summary>
    public void AddGrenade()
    {
        currentGrenades = Mathf.Min(currentGrenades + 1, maxGrenades);
    }

    /// <summary>
    /// Añade un número X de granadas
    /// </summary>
    /// <param name="amount"></param>
    public void AddGrenade(int amount)
    {
        currentGrenades = Mathf.Min(currentGrenades + amount, maxGrenades);
    }

    // Getters
    public int GetCurrentGrenades()
    {
        return currentGrenades;
    }

    public int GetMaxGrenades()
    {
        return maxGrenades;
    }
}
