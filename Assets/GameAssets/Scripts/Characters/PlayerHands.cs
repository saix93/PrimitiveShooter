using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHands : MonoBehaviour {

    /* Variables */
    // WeaponsManager
    [SerializeField]
    private WeaponsManager weaponsManager;

    private float fieldOfView;

    // Array de armas
    private Weapon[] weaponArray;
    
    [SerializeField]
    private bool[] ownedWeapons;

    // Player
    private Player player;

    /* Métodos */

    private void Awake()
    {
        fieldOfView = Camera.main.fieldOfView;

        weaponArray = this.GetComponentsInChildren<Weapon>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        for (int i = 0; i < ownedWeapons.Length; i++)
        {
            if (ownedWeapons[i])
            {
                ChooseWeapon(i);
                break;
            }
        }
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

        Camera.main.fieldOfView = fieldOfView;

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
