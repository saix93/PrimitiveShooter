using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPowerUp : MonoBehaviour {

    /* Variables */
    
    [SerializeField]
    private float actualAngle;
    [SerializeField]
    private float angularSpeed = 45;
    [SerializeField]
    private float radius;
    [SerializeField]
    private Transform rotationPoint;

    [SerializeField]
    private GameObject lifePowerUpPrefab;
    [SerializeField]
    private GameObject ammoPowerUpPrefab;

    [SerializeField]
    private float spawnCooldown = 30;

    private float timeToSpawn;
    private GameObject[] powerUpArray;

    /* Métodos */

    private void Awake()
    {
        powerUpArray = new GameObject[2];
    }

    private void Start()
    {
        timeToSpawn = Time.time + spawnCooldown;

        powerUpArray[0] = lifePowerUpPrefab;
        powerUpArray[1] = ammoPowerUpPrefab;
    }

    void Update () {
        this.transform.position = rotationPoint.position + new Vector3(Mathf.Cos(actualAngle), 0, Mathf.Sin(actualAngle)) * radius;

        actualAngle += angularSpeed * Mathf.Deg2Rad * Time.deltaTime;

        if (timeToSpawn < Time.time)
        {
            Instantiate(powerUpArray[Random.Range(0,2)], this.transform.position, this.transform.rotation);

            timeToSpawn = Time.time + spawnCooldown;
        }
    }
}
