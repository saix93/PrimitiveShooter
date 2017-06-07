using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    /* Variables */
    // Personaje a spawnear
    [SerializeField]
    private GameObject characterPrefab;

    // Trigger que va a utilizar este spawner
    [SerializeField]
    private SpawnerTrigger trigger;

    // ¿Debe usar el trigger para spawnear enemigos?
    [SerializeField]
    private bool useTrigger = true;

    [Header("Not using trigger")]
    [Space(10)]
    // Mobs que debe spawnear al crearse
    [SerializeField]
    private int maxNumberOfMobsToSpawn = 3;
    // Número actual de enemigos spawneados
    private int spawnedMobs;

    // Tiempo entre spawn y spawn
    [SerializeField]
    private int mobSpawnTime = 1;

    /* Métodos */
    private void Awake()
    {
        if (useTrigger)
        {
            trigger.AddSpawner(this);
        }
    }

    private void Start()
    {
        if (!useTrigger)
        {
            InvokeRepeating("Spawn", 0, mobSpawnTime);
        }
    }

    private void Update()
    {
        if (spawnedMobs == maxNumberOfMobsToSpawn)
        {
            CancelInvoke("Spawn");
        }
    }

    /// <summary>
    /// Crea un personaje en la escena y añade 1 al contador de enemigos spawneados
    /// </summary>
    public void Spawn()
    {
        Instantiate(characterPrefab, this.transform.position, this.transform.rotation);

        spawnedMobs++;
    }

    /// <summary>
    /// Crea X personajes en la escena
    /// </summary>
    public void Spawn(int number)
    {
        for (var i = 0; i < number; i++)
        {
            Instantiate(characterPrefab, this.transform.position, this.transform.rotation);
        }
    }
}
