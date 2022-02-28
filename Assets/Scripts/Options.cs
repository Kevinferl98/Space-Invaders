using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public void full_hd()
    {
        Screen.SetResolution(1920, 1080,Screen.fullScreen);
    }

    public void hd()
    {
        Screen.SetResolution(1280, 720,Screen.fullScreen);
    }

    public void cambio()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
