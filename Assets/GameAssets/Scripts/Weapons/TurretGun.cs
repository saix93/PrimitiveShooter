using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGun : Weapon {

    /* Métodos */

    public override void ManageWeapon(Player target)
    {
        this.transform.LookAt(target.transform);

        Shoot();
    }
}
