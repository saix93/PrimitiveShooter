using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon {
	
    /* Métodos */

    public override void ManageWeapon()
    {
        base.ManageWeapon();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
}
