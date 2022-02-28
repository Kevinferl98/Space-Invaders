using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpeed : Pickup
{
    public override void Pick()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().PickSpeed();
        Destroy(gameObject);
    }
}
