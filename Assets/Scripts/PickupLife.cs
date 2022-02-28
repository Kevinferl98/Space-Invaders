using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupLife : Pickup
{
    public override void Pick()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().PickLife();
        Destroy(gameObject);
    }
}
