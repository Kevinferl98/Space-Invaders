using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public int scoreValue;          // punteggio dell'alieno
    public GameObject explosion;    // il prefab dell'esplosione
    public GameObject pickLife;     // Gemma della vita
    public GameObject pickSpeed;    // Gemma della velocita
    public GameObject pickScore;    // Gemma del punteggio

    public void Kill()
    {
        // Estrae un numero random e se cade dentro un intervallo spawna una gemma 
        int random = Random.Range(0, 300);

        if (random <= 3)
            Instantiate(pickLife, transform.position, Quaternion.identity);
        else if (random <= 15)
            Instantiate(pickSpeed, transform.position, Quaternion.identity);
        else if (random <= 20)
            Instantiate(pickScore, transform.position, Quaternion.identity);

        UI.Instance().UpdateScore(scoreValue);   // aggiorna il punteggio della UI
        AlienSet.Aliens.Remove(gameObject);   // rimuove l'alieno dalla lista di alieni
        AlienSet.anim.Remove(gameObject.GetComponent<Animator>());    // rimuove l'animator dalla lista di animator
        Instantiate(explosion, transform.position, Quaternion.identity);  // instanzia l'esplosione
        AudioManager.Instance().UpdateTime();  //velocizza l'audio degli alieni rimasti
        AlienSet.UpdateSpeed();                // velocizza il movimento degli alieni rimasti

        if (AlienSet.Aliens.Count == 0)   // se tutti gli alieni sono morti richiama il game manager
        {                                   // per spawnare nuovi nemici/boss 
            GameManager.Instance().NextLevel();
        }
        AlienSet.AnimSpeed();             // velocizza l'animazione degli alieni rimasti
        Destroy(gameObject);                // distrugge l'alieno
    }

}
