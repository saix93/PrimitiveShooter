using System.Collections;
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
    private int turretDamageWhenDestroyed = 1400;

    [SerializeField]
    private GameObject boss;
    private FinalBoss fBoss;

    private LavaPlatform[] platformArray;

    private int numberOfTurrets = 2;

    private bool wallsGoDown;
    private bool shouldPlatformMove;
    private bool hasStarted;
    private bool isPrepared;


    /* Métodos */

    private void Awake()
    {
        platformArray = this.transform.Find("Platforms").GetComponentsInChildren<LavaPlatform>();

        fBoss = boss.GetComponent<FinalBoss>();
    }

    private void Update()
    {
        ManageWallsPosition();

        ManagerPlatformMovement(shouldPlatformMove);
    }

    /// <summary>
    /// Controla el movimiento de las plataformas
    /// </summary>
    /// <param name="shouldMove"></param>
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
    /// Activa el boss
    /// </summary>
    private void ActivateBoss()
    {
        fBoss.ActivateBoss();
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

            Invoke("ActivateBoss", 3);

            hasStarted = true;
        }
    }

    /// <summary>
    /// Controla la destrucción de una torreta del boss
    /// </summary>
    public void TurretDestroyed()
    {
        fBoss.ReceiveDamage(turretDamageWhenDestroyed);
        numberOfTurrets--;

        if (numberOfTurrets == 1)
        {
            StartPhaseTwo();
        }
        else if (numberOfTurrets == 0)
        {
            StartPhaseThree();
        }
    }

    /// <summary>
    /// Comienza la fase 2
    /// </summary>
    private void StartPhaseTwo()
    {
        fBoss.SetTimeToShootRay(Time.time + fBoss.GetTimeToShootRayDelay());
        fBoss.SetPhase(1);
    }

    /// <summary>
    /// Comienza la fase 3
    /// </summary>
    private void StartPhaseThree()
    {
        fBoss.GetSword().gameObject.SetActive(true);
        fBoss.SetPhase(2);
    }
}
