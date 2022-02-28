using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods 
{
   public static void Entering(this Transform tran)
    {
        tran.position -= new Vector3(0, 1, 0) * 10 * Time.deltaTime;
    }

}
