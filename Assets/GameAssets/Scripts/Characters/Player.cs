using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character {

    /* Variables */

    // Arma actual
    private Weapon currentWeapon;
	
    /* Métodos */

	void Update () {
		if (currentLife > 0 && currentWeapon != null)
        {
            currentWeapon.ManageWeapon();
        }
	}

    public Weapon GetWeapon()
    {
        return currentWeapon;
    }

    /// <summary>
    /// Cambia el arma actual
    /// </summary>
    /// <param name="newWeapon"></param>
    public void SetWeapon(Weapon newWeapon)
    {
        currentWeapon = newWeapon;
    }

    public void AddWeapon(int weaponNumber)
    {
        this.GetComponentInChildren<PlayerHands>().AddWeapon(weaponNumber);
    }

    protected override void KillMe()
    {
        base.KillMe();

        // GAME-OVER
        SceneManager.LoadScene("gameover");
    }

    public override void MoveFaster(float speedModifier)
    {
        base.MoveFaster(speedModifier);

        this.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_WalkSpeed *= speedModifier;
        this.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_RunSpeed *= speedModifier;
    }

    public override void MoveSlower(float speedModifier)
    {
        base.MoveSlower(speedModifier);

        this.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_WalkSpeed /= speedModifier;
        this.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_RunSpeed /= speedModifier;
    }
}
