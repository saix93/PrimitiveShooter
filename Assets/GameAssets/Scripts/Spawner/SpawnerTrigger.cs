using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTrigger : MonoBehaviour {

    /* Variables */
    // Array de spawners que va a utilizar el trigger
    private List<Spawner> spawnerList = new List<Spawner>();

    // Cuantos mobs puede spawnear
    [SerializeField]
    private int totalNumberOfSpawns = 1;

    private int currentSpawns;

    // Cuantos mobs spawnea a la vez
    [SerializeField]
    private int numberOfMobsToSpawn = 1;

    /* Métodos */

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && totalNumberOfSpawns > currentSpawns)
        {
            foreach (Spawner spawner in spawnerList)
            {
                spawner.Spawn(numberOfMobsToSpawn);
            }

            currentSpawns++;
        }
    }

    public void AddSpawner(Spawner spawner)
    {
        spawnerList.Add(spawner);
    }
}
