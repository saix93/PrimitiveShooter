using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHands : MonoBehaviour {

    /* Variables */
    // Array de armas
    private Weapon[] weaponArray;

    private bool[] ownedWeapons;

    // Player
    private Player player;

    /* Métodos */

    private void Start()
    {
        weaponArray = new Weapon[this.GetComponentsInChildren<Weapon>().Length];
        weaponArray = this.GetComponentsInChildren<Weapon>();

        ownedWeapons = new bool[weaponArray.Length];

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        ownedWeapons[0] = true;

        ChooseWeapon(0);
    }

    void Update () {
        for (int i = 0; i < weaponArray.Length; i++)
        {
            if (Input.GetKeyDown("" + (i + 1) + ""))
            {
                ChooseWeapon(i);
            }
        }
    }

    /// <summary>
    /// Recoge un arma del suelo
    /// </summary>
    public void AddWeapon(int weaponNumber)
    {
        ownedWeapons[weaponNumber] = true;
    }

    /// <summary>
    /// Elige el arma que va a usar el usuario
    /// </summary>
    /// <param name="arrayIndex"></param>
    private void ChooseWeapon(int arrayIndex)
    {
        if (!ownedWeapons[arrayIndex])
        {
            return;
        }

        DisableWeapons();

        weaponArray[arrayIndex].gameObject.SetActive(true);

        player.SetWeapon(weaponArray[arrayIndex]);
    }

    /// <summary>
    /// Desactiva todas las armas
    /// </summary>
    private void DisableWeapons()
    {
        for (int i = 0; i < weaponArray.Length; i++)
        {
            weaponArray[i].gameObject.SetActive(false);
        }
    }
}
