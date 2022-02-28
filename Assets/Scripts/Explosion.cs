using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AudioClip sfx;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance().PlaySound(sfx);
        Destroy(gameObject, 0.2f);
    }
}
