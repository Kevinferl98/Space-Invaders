using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private bool muted;

    // Start is called before the first frame update
    void Start()
    {
        muted = PlayerPrefs.GetInt("Muted") == 1;

        if (muted)
            AudioListener.pause = true;
    }

    public void ToggleMute()
    {
        muted = !muted;

        if (muted)
            PlayerPrefs.SetInt("Muted", 1);
        else
            PlayerPrefs.SetInt("Muted", 0);

        if (muted)
            AudioListener.pause = true;
        else
            AudioListener.pause = false;
    }
}
