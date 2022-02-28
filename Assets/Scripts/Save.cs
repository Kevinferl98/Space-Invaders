using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Deve salvare il punteggio massimo ottenuto dal giocatore, cosi che ad ogni avvio del gioco 
// l'hi-score non si resetti

public class Save : MonoBehaviour
{
    public static void save(int highscore)
    {
        PlayerPrefs.SetInt("high", highscore);
        Debug.Log("Highscore" + PlayerPrefs.GetInt("high"));
    }

}
