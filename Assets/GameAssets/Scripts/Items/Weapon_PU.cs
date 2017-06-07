using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_PU : PowerUp {

    /* Variables */
    // Arma que se va a recoger
    [SerializeField]
    private Weapon weaponToPickUpPrefab;

    private int weaponToAdd;

    /* Métodos */
    private void Awake()
    {
        weaponToAdd = Instantiate(weaponToPickUpPrefab, this.transform.position, this.transform.rotation, this.transform).GetComponent<Weapon>().GetWeaponId();
    }

    protected override void PickUp(Player player)
    {
        base.PickUp(player);

        player.AddWeapon(weaponToAdd);

        Destroy(this.gameObject);
    }
}
