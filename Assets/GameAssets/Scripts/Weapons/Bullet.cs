using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    /* Variables */
    // Sistema de partículas
    [SerializeField]
    private GameObject sparkPSPrefab;

    // Sistema de partículas
    [SerializeField]
    private GameObject bloodPSPrefab;

    /* Métodos */

    protected override void OnProjectileHit(Collider other)
    {
        base.OnProjectileHit(other);

        if (other.gameObject.GetComponent<Character>() != null || other.gameObject.GetComponentInParent<Character>() != null)
        {
            Character targetCharacter = other.gameObject.GetComponent<Character>() != null ? other.gameObject.GetComponent<Character>() : other.gameObject.GetComponentInParent<Character>();

            targetCharacter.ReceiveDamage(projectileDamage);

            GameObject hitPSPrefab;

            // Si el objetivo tiene sangre, se usa el PS de sangre
            if (targetCharacter.hasBlood)
            {
                hitPSPrefab = bloodPSPrefab;
            }
            else
            {
                // Si no, se usan chispas
                hitPSPrefab = sparkPSPrefab;
            }

            GameObject hitPS = Instantiate(hitPSPrefab, this.transform.position, this.transform.rotation);

            Destroy(hitPS, 2);

            Destroy(this.gameObject);
        }
        else
        {
            GameObject newSpark = Instantiate(sparkPSPrefab, this.transform.position, this.transform.rotation);

            Destroy(newSpark, 2);

            Destroy(this.gameObject);
        }
    }
}
