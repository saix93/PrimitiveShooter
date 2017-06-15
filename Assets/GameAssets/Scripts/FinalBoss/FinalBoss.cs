using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : Character {

    /* Variables */
    private BossGun[] weaponArray;

    private Player player;

    private bool shouldShoot;

    /* Métodos */
    private void Awake()
    {
        weaponArray = this.GetComponentsInChildren<BossGun>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        Vector3 whereToLook = player.transform.position;
        whereToLook.y = this.transform.position.y;

        this.transform.LookAt(whereToLook);

        if (shouldShoot)
        {
            foreach (BossGun weapon in weaponArray)
            {
                if (weapon)
                {
                    weapon.ManageWeapon(player);
                }
            }
        }
    }
}
