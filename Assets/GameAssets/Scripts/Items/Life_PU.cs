using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life_PU : PowerUp {
    
    /* Métodos */
    protected override void PickUp(Player player)
    {
        base.PickUp(player);

        player.AddLife(amount);

        Destroy(this.gameObject);
    }
}
