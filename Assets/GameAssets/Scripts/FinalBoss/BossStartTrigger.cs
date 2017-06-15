using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartTrigger : MonoBehaviour {

    /* Variables */
    // BossManager
    [SerializeField]
    private BossManager bm;

    /* Métodos */
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player)
        {
            // Prepara el boss
            bm.PrepareBoss();
        }
    }
}
