using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    /* Variables */
    // Daño de la bala
    private int bulletDamage;

    // Tiempo de vida de cada bala
    [SerializeField]
    private float lifetime = 6;

    // Sistema de partículas
    [SerializeField]
    private GameObject sparkPSPrefab;

    // Sistema de partículas
    [SerializeField]
    private GameObject bloodPSPrefab;

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

        if (other.gameObject.GetComponent<Character>() != null || other.gameObject.GetComponentInParent<Character>() != null)
        {
            Character targetCharacter = other.gameObject.GetComponent<Character>() != null ? other.gameObject.GetComponent<Character>() : other.gameObject.GetComponentInParent<Character>();

            targetCharacter.ReceiveDamage(bulletDamage);

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

    /* Métodos */

    /// <summary>
    /// Modifica el daño de la bala
    /// </summary>
    /// <param name="newBulletDamage"></param>
    public void SetBulletDamage(int newBulletDamage)
    {
        bulletDamage = newBulletDamage;
    }

}
