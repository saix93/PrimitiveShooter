using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /* Variables */
    // Daño del proyectil
    protected int projectileDamage;

    // Tiempo de vida de cada proyectil
    [SerializeField]
    protected float lifetime = 6;

    /* Métodos */

    private void Start()
    {
        Destroy(this.gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        OnProjectileHit(other);
    }

    /// <summary>
    /// Cuando el proyectil impacta en algo
    /// </summary>
    protected virtual void OnProjectileHit(Collider other)
    {
        // Se sobreescribe en clases que heredan de esta
    }

    /// <summary>
    /// Modifica el daño de la bala
    /// </summary>
    /// <param name="newProjectileDamage"></param>
    public void SetProjectileDamage(int newProjectileDamage)
    {
        projectileDamage = newProjectileDamage;
    }
}
