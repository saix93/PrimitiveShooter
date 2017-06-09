using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoGUI : MonoBehaviour {

    /* Variables */
    // Arma actual
    private Weapon weapon;

    // Texto de la munición
    private Text ammoText;

    // Player
    private Player player;

    /* Métodos */

    private void Awake()
    {
        ammoText = this.transform.Find("AmmoText").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        weapon = player.GetWeapon();
        ammoText.text = weapon.GetCurrentClipAmmo() + " / " + weapon.GetMaxClipAmmo() + " | " + weapon.GetCurrentAmmo();
    }
}
