using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGunCharacter : Character
{
    /* Variables */

    // Sistema de partículas de explosión
    [SerializeField]
    private GameObject explosionPSPrefab;

    // Sistema de partículas de explosión
    [SerializeField]
    private GameObject explosionAudioPrefab;

    // Boss Manager
    [SerializeField]
    private BossManager bm;

    /* Métodos */

    /// <summary>
    /// El mob explota
    /// </summary>
    private void Explode()
    {
        GameObject newExplosion = Instantiate(explosionPSPrefab, this.transform.position, this.transform.rotation);

        Instantiate(explosionAudioPrefab, this.transform.position, this.transform.rotation);

        Destroy(newExplosion, 2);

        Destroy(this.gameObject);
    }

    /// <summary>
    /// Función de Character sobreescrita
    /// </summary>
    protected override void KillMe()
    {
        base.KillMe();

        // MOB DOWN
        Explode();

        DamageBoss();
    }

    private void DamageBoss()
    {
        bm.TurretDestroyed();
    }
}
