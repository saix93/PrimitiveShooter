using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon {

    /* Métodos */

    public override void ManageWeapon()
    {
        base.ManageWeapon();

        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }
}
