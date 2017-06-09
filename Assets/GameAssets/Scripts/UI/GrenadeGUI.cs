using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrenadeGUI : MonoBehaviour {

    /* Variables */
    // Texto de la munición
    private Text grenadeAmmoText;

    // Throw Grenade
    [SerializeField]
    private GameObject throwGrenadeObject;

    // Grenade Manager
    private ThrowGrenade grenadeManager;

    /* Métodos */

    private void Awake()
    {
        grenadeAmmoText = this.transform.Find("GrenadeAmmoText").GetComponent<Text>();
        grenadeManager = throwGrenadeObject.GetComponent<ThrowGrenade>();
    }

    private void Update()
    {
        grenadeAmmoText.text = grenadeManager.GetCurrentGrenades().ToString();
    }
}
