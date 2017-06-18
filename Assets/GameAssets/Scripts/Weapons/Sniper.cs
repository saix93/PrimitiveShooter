using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon {

    /* Variables */

    [SerializeField]
    private float zoom = 20;
    private float normal;
    [SerializeField]
    private float smooth = 5;

    private bool isZoomed;

    /* Métodos */

    private void Awake()
    {
        fireAudio = transform.Find("Sounds/Fire").GetComponent<AudioSource>();
        reloadAudio = transform.Find("Sounds/Reload").GetComponent<AudioSource>();

        normal = Camera.main.fieldOfView;
    }

    public override void ManageWeapon()
    {
        base.ManageWeapon();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            isZoomed = !isZoomed;
        }

        if (isZoomed)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, zoom, Time.deltaTime * smooth); 
        }
        else
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, normal, Time.deltaTime * smooth);
        }
    }
}
