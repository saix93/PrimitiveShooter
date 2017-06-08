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

    /* Métodos */

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Rigidbody newGrenade = Instantiate(grenadePrefab, this.transform.position, this.transform.rotation);

        newGrenade.AddForce(this.transform.forward * launchForce, ForceMode.Impulse);
    }
}
