using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_PU : PowerUp {

    /* Variables */
    // Arma que se va a recoger
    [SerializeField]
    private GameObject weaponToPickUpPrefab;

    private Weapon weaponToAdd;

    private int weaponId;

    /* Métodos */
    private void Awake()
    {
        weaponToAdd = Instantiate(weaponToPickUpPrefab, this.transform.position, this.transform.rotation, this.transform).GetComponent<Weapon>();
        weaponId = weaponToAdd.GetWeaponId();

        // Modifica el tamaño del arma para que se vea más grande
        weaponToAdd.transform.localScale *= 4;

        // Añade una rotación al arma
    }

    protected override void PickUp(Player player)
    {
        base.PickUp(player);

        player.AddWeapon(weaponId);

        Destroy(this.gameObject);
    }
}
