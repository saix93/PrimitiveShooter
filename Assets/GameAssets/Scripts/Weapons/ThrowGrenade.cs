using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenade : Weapon {

    /* Métodos */

    public override void ManageWeapon()
    {
        base.ManageWeapon();

        if (Input.GetButtonDown("Fire2"))
        {
            Shoot();
        }
    }
}
