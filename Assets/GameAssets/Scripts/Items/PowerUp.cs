using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    /* Variables */
    // ¿Es recogible?
    [SerializeField]
    protected bool isPickable = true;

    // Cantidad
    [SerializeField]
    protected int amount = 25;

    // Sistema de partículas que se ve al recogerlo
    [SerializeField]
    protected GameObject pickUpPSPrefab;

    /* Métodos */

    private void Update()
    {
        this.transform.Rotate(0, 5, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            PickUp(other.transform.GetComponent<Player>());
        }
    }

    /// <summary>
    /// Recoge el PowerUp
    /// </summary>
    protected virtual void PickUp(Player player)
    {
        // Se sobreescribe en clases hijas
        
        if (pickUpPSPrefab != null)
        {
            GameObject pickUpPS = Instantiate(pickUpPSPrefab, this.transform.position, this.transform.rotation);

            Destroy(pickUpPS, 7);
        }
    }
}
