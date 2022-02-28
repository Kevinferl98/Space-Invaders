using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector2.zero;
    }

    public void Open()
    {
        gameObject.SetActive(true);
        transform.LeanScale(Vector2.one, 0.5f);
    }

    public void Close()
    {
        transform.LeanScale(Vector2.zero, 0.5f).setEaseInBack();
    }
}
