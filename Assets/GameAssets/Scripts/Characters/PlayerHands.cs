using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHands : MonoBehaviour {

    /* Variables */
    // ID del arma con la que empieza
    [SerializeField]
    private int startingWeaponID = 0;

    // WeaponsManager
    [SerializeField]
    private WeaponsManager weaponsManager;

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

        ownedWeapons[startingWeaponID] = true;

        ChooseWeapon(startingWeaponID);
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
        weaponsManager.RefreshWeaponsGUI(ownedWeapons);
    }

    public bool IsWeaponOwned(int id)
    {
        return ownedWeapons[id];
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

        weaponsManager.ChooseWeapon(arrayIndex);

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
