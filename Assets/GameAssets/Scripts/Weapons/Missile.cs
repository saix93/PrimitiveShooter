using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{

    /* Variables */
    // Radio de explosión del misil
    [SerializeField]
    private float explosionRadius = 10;

    // Sistema de partículas
    [SerializeField]
    private GameObject explosionPSPrefab;

    private void Start()
    {
        Destroy(this.gameObject, lifetime);
    }

    /* Métodos */

    protected override void OnProjectileHit(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, explosionRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            Character character = colliders[i].GetComponent<Character>();
            if (colliders[i].isTrigger == false && character != null)
            {
                character.ReceiveDamage(projectileDamage);
            }
        }

        GameObject explosionPS = Instantiate(explosionPSPrefab);
        explosionPS.transform.position = this.transform.position;

        Destroy(this.gameObject);
        Destroy(explosionPS, 4);
    }

}
