using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour
{
    public VideoPlayer logo;
    public VideoPlayer loading;
    public AudioSource song;

    // Start is called before the first frame update
    void Start()
    {
        song.Play();
        StartCoroutine(sequence());
    }

    private IEnumerator sequence()
    {
        logo.Play();
        yield return new WaitForSeconds(8);
        loading.Play();
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene(1);
    }
}
