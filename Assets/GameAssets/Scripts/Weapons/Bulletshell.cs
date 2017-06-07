using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletshell : MonoBehaviour {

    /* Variables */
    // Sonidos
    private AudioSource bulletshellAudio0;
    private AudioSource bulletshellAudio1;
    private AudioSource bulletshellAudio2;
    private AudioSource bulletshellAudio3;

    private AudioSource[] bulletshellArray;

    // Tiempo de vida máxima de cada casquillo
    [SerializeField]
    private float lifetime = 15;

    // Representa si ya se ha odio el sonido del casquillo al caer
    private bool alreadySounded = false;

    /* Métodos */

    private void Awake()
    {
        bulletshellAudio0 = transform.Find("Sounds/Bulletshells0").GetComponent<AudioSource>();
        bulletshellAudio1 = transform.Find("Sounds/Bulletshells1").GetComponent<AudioSource>();
        bulletshellAudio2 = transform.Find("Sounds/Bulletshells2").GetComponent<AudioSource>();
        bulletshellAudio3 = transform.Find("Sounds/Bulletshells3").GetComponent<AudioSource>();

        bulletshellArray = new AudioSource[4];

        bulletshellArray[0] = bulletshellAudio0;
        bulletshellArray[1] = bulletshellAudio1;
        bulletshellArray[2] = bulletshellAudio2;
        bulletshellArray[3] = bulletshellAudio3;
    }

    private void Start()
    {
        Destroy(this.gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!alreadySounded)
        {
            // bulletshellArray[Random.Range(0, 4)].Play();
            bulletshellArray[Random.Range(0, 4)].PlayOneShot(bulletshellArray[Random.Range(0, 4)].clip);

            this.GetComponent<Rigidbody>().isKinematic = true;

            alreadySounded = true;
        }
    }
}
