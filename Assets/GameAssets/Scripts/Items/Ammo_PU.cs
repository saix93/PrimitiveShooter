using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_PU : PowerUp {

    /* Variables */
    // Arma sobre la que se añade la munición recogida
    [SerializeField]
    private Weapon weapon;
    
    /* Métodos */
    protected override void PickUp(Player player)
    {
        base.PickUp(player);

        if (weapon != null)
        {
            weapon.AddAmmo(amount);
        }
        else
        {
            player.currentWeapon.AddAmmo(amount);
        }

        Destroy(this.gameObject);
    }
}
