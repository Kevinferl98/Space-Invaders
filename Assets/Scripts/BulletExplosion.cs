using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    // distrugge il proiettile distrutto 
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.1f);
    }
}
