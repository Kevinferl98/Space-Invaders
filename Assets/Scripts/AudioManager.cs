using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource alien;       // suono per il movimento degli alieni
    public AudioSource sfx;         // suono per gli effetti (sparo, esplosione, ecc...)
    public AudioSource ms;          // suono per la MotherShip (wiu wiu wiu wiu)
    public AudioSource battle;      // soundtrack dei livelli con gli alieni 
    public AudioSource final_boss;  // soundtrack per il boss finale
    public AudioSource credits;     // soundtrack dei crediti

    private bool play;
    private float time;             // serve per velocizzare il suono prodotto dagli alieni

    private static AudioManager instance;      // Singleton

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public static AudioManager Instance()
    {
        return instance;
    }

    // imposta il time a 1
    // e avvia la coroutine per riprodurre l'audio
    public void PlayAlien()
    {
        alien.volume = 0.3f;       
        time = 1;      
        play = true;
        StartCoroutine(AlienSound());
    }
    
    // ferma la coroutine
    public void StopAlien()
    {
        alien.volume = 0;  // risolve il bug dell'audio nel menu iniziale 
        play = false;
        StopCoroutine(AlienSound());
    }

    // riproduce la soundtrack dei livelli con gli alieni
    public void PlayBattle()
    {
        battle.Play();
    }

    // ferma la soundtrack dei livelli con gli alieni
    public void StopBattle()
    {
        battle.Stop();
    }

    // riproduce la soundtrack per il livello finale
    public void PlayFinalBoss()
    {
        final_boss.Play();
    }

    // ferma la soundtrack per il livello finale
    public void StopFinalBoss()
    {
        final_boss.Stop();
    }

    // riproduce l'effeto audio passato
    public void PlaySound(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
    }

    // ferma l'effetto audio sfx
    public void StopSound()
    {
        sfx.Stop();
    }

    // riproduce il suono della MotherShip
    public void PlayMS(AudioClip clip)
    {
        ms.Play();
    }

    // ferma il suondo della MotherShip (per fortuna)
    public void StopMS()
    {
        ms.Stop();
    }

    // riproduce la soundtrack dei crediti
    public void PlayCredits()
    {
        credits.Play();
    }

    // ferma la soundtrack dei crediti
    public void StopCredits()
    {
        credits.Stop();
    }

    // velocizza il suono degli alieni
    public void UpdateTime()
    {
        time -= 0.01f;
    }

    // riproduce il suondo degli alieni ogni tot di secondi
    private IEnumerator AlienSound()
    {
        while (play)
        {
            yield return new WaitForSeconds(time);
            alien.Play();
        }
    }
}
