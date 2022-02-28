using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScore : Pickup
{
    public override void Pick()
    {
        UI.Instance().UpdateScore(50);
        Destroy(gameObject);
    }
}
