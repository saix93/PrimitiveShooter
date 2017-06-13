using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

    /* Variables */

    // Tiempo que tarda en explotar
    [SerializeField]
    private float timeToExplode = 3;

    // Radio de explosión
    [SerializeField]
    private float explosionRadius = 5;

    // Sistema de partículas de la explosión
    [SerializeField]
    private GameObject explosionPSPrefab;

    // Prefab del audio de explosion
    [SerializeField]
    private GameObject explosionAudioPrefab;

    // Daño de la granada
    private int grenadeDamage;

    /* Métodos */

    void Start () {
        this.transform.forward = Random.insideUnitSphere;
        Invoke("Explode", timeToExplode);
	}

    public void SetGrenadeDamage(int newDamage)
    {
        grenadeDamage = newDamage;
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

        Instantiate(explosionAudioPrefab, this.transform.position, this.transform.rotation);

        Destroy(this.gameObject);
        Destroy(explosionPS, 4);
    }
}
