using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMI_Exp : MonoBehaviour
{
    public AudioClip sfx;

    // riproduce l'audio della esplosione e distrugge il gameObject
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance().PlaySound(sfx);
        Destroy(gameObject, 0.3f);   
    }
}
