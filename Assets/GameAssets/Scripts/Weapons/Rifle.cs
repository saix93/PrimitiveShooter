using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon {

    /* Métodos */

    private void Awake()
    {
        fireAudio = transform.Find("Sounds/Fire").GetComponent<AudioSource>();
        reloadAudio = transform.Find("Sounds/Reload").GetComponent<AudioSource>();
    }

    public override void ManageWeapon()
    {
        base.ManageWeapon();

        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }
}
