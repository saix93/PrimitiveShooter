using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGun : Weapon {

    /* Métodos */

    private void Awake()
    {
        fireAudio = transform.Find("Sounds/Fire").GetComponent<AudioSource>();
        reloadAudio = transform.Find("Sounds/Reload").GetComponent<AudioSource>();
    }

    public override void ManageWeapon(Player target)
    {
        this.transform.LookAt(target.transform);

        Shoot();
    }
}
