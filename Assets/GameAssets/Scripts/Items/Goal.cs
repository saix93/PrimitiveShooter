using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

    /* Variables */

    // Tiempo que debe esperar para cambiar a la escena de victoria
    [SerializeField]
    private float timeToWaitUntilVictory = 5;

    // Mensaje de victoria
    [SerializeField]
    private GameObject winMessage;

    // Crosshair
    [SerializeField]
    private GameObject crosshair;

    // Sistema de partículas de victoria
    [SerializeField]
    private GameObject winPSPrefab;

    // Sonido de victoria
    private AudioSource winAudio;

    // Controla si ya se ha entrado en el trigger
    private bool victoryShown = false;

    /* Métodos */

    private void Start()
    {
        winAudio = GetComponent<AudioSource>();

        winMessage.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !victoryShown)
        {
            victoryShown = true;
            
            other.GetComponent<Player>().SetInvulnerable(true);

            Instantiate(winPSPrefab);

            winMessage.SetActive(true);
            crosshair.SetActive(false);

            Invoke("WinGame", timeToWaitUntilVictory);
        }
    }

    private void WinGame()
    {
        winAudio.Play();

        SceneManager.LoadScene("victory");
    }
}
