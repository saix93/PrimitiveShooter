﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour {

    /* Variables */
    [SerializeField]
    private GameObject bossDelimitterWalls;

    [SerializeField]
    private float upperWallsLimit = -5.7f;
    [SerializeField]
    private float lowerWallsLimit = -45.7f;
    [SerializeField]
    private float wallsMovementSpeed = 10;

    [SerializeField]
    private GameObject boss;

    private LavaPlatform[] platformArray;

    private bool wallsGoDown;
    private bool shouldPlatformMove;
    private bool hasStarted;
    private bool isPrepared;


    /* Métodos */

    private void Awake()
    {
        platformArray = this.transform.Find("Platforms").GetComponentsInChildren<LavaPlatform>();
    }

    private void Update()
    {
        ManageWallsPosition();

        ManagerPlatformMovement(shouldPlatformMove);
    }

    private void ManagerPlatformMovement(bool shouldMove)
    {
        foreach (LavaPlatform platform in platformArray)
        {
            platform.shouldMove = shouldMove;
        }
    }

    /// <summary>
    /// Controla la posición de los muros en update
    /// </summary>
    private void ManageWallsPosition()
    {
        Vector3 position = bossDelimitterWalls.transform.localPosition;

        if (wallsGoDown)
        {
            position += -bossDelimitterWalls.transform.up * wallsMovementSpeed * Time.deltaTime;

            position.y = Mathf.Max(position.y, lowerWallsLimit);
        }
        else
        {
            position += bossDelimitterWalls.transform.up * wallsMovementSpeed * Time.deltaTime;

            position.y = Mathf.Min(position.y, upperWallsLimit);
        }

        bossDelimitterWalls.transform.localPosition = position;
    }

    /// <summary>
    /// Prepara el combate con el boss
    /// </summary>
    public void PrepareBoss()
    {
        if (!isPrepared)
        {
            wallsGoDown = true;
            shouldPlatformMove = true;

            boss.SetActive(true);

            isPrepared = true;
        }
    }

    /// <summary>
    /// Empieza el combate con el boss
    /// </summary>
    public void StartBoss()
    {
        if (!hasStarted)
        {
            wallsGoDown = false;

            hasStarted = true;
        }
    }
}