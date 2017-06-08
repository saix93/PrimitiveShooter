using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

    /* Variables */

    // Tiempo que tarda en explotar
    [SerializeField]
    private float timeToExplode = 3;

    // Daño de la granada
    [SerializeField]
    private int grenadeDamage = 30;

    // Radio de explosión
    [SerializeField]
    private float explosionRadius = 5;

    // Sistema de partículas de la explosión
    [SerializeField]
    private GameObject explosionPSPrefab;

    /* Métodos */
    
	void Start () {
        this.transform.forward = Random.insideUnitSphere;
        Invoke("Explode", timeToExplode);
	}

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, explosionRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            Character character = colliders[i].GetComponent<Character>();
            if (colliders[i].isTrigger == false && character != null)
            {
                character.ReceiveDamage(grenadeDamage);
            }
        }

        GameObject explosionPS = Instantiate(explosionPSPrefab);
        explosionPS.transform.position = this.transform.position;
        Destroy(explosionPS, 4);
    }
}
