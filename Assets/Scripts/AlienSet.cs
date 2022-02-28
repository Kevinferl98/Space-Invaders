using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSet : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;      // prefab del proiettile nemico
    [SerializeField]
    GameObject Mothership;  // prefab della navicella aliena

    private Vector3 horizontal = new Vector3(0.1f, 0, 0);    // movimento orizzontale
    private Vector3 vertical = new Vector3(0, 0.15f, 0);   // movimento verticale
    private Vector3 MotherShipLocation = new Vector3(7.66f,3.79f,0);   // posizione in cui spawna la navicella aliena

    private const float MAX_LEFT = -4.45f;          // bordo sinistro
    private const float MAX_RIGHT = 4.45f;          // bordo destro
    public float START;                             // posizione di partenza degli alieni
    private const float MAX_Y = -0.9f;              // bordo inferiore

    private float moveTimer = 0;             // timer per muoversi

    private static float timerMove = 0.225f;    


    private float shootTimer = 0;                  // timer per sparare

    private float MotherShipTimer = 0;            // timer per spawnare la navicella aliena

    private bool enter = true;                      // controlla l'entrata del nemico

    public static List<GameObject> Aliens = new List<GameObject>();     // lista per contenere gli alieni
    private bool destra;                                                // controlla il movimento orizzontale (destra/sinistra)
    public static List<Animator> anim = new List<Animator>();           // lista per contenere gli animator degli alieni

    // Start is called before the first frame update
    void Start()
    {
        timerMove = 0.225f;     // resetta il timerMove per ogni set di alieni
        // aggiungiamo tutti gli oggetti con il tag "Alien" alla lista di GameObject
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Alien"))
            Aliens.Add(go);

        // aggiungiamo tutti gli Animator degli oggetti con il tag "Alien" alla lista di Animator
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Alien"))
            anim.Add(go.GetComponent<Animator>());
    }

    // Update is called once per frame
    void Update()
    {

        // controlla se il nemico sta apparendo e lo sposta velocemente alla posizione di partenza
        if (enter)
        {
            transform.Entering();       //ExtensionMethods
            if (transform.position.y <= START)  // se arriva alla posizione di partenza inizia
                enter = false;                  // imposta enter a false per farli muovere left e right
        }
        else
        {
            if(moveTimer >= timerMove && Player.pepsi!=true)
                Move();

            if (shootTimer >= 1.8f && Player.pepsi!=true)
                Shoot();

            if (MotherShipTimer >= 30)
                Spawn();

            moveTimer += Time.deltaTime;
            shootTimer += Time.deltaTime;
            MotherShipTimer += Time.deltaTime;
        }
    }

    private void Move()
    {
        if(Aliens.Count > 0)    // controlla che sia presente almeno un alieno 
        {
            int bordo = 0;      // bordo orizzontale
            int bordo2 = 0;     // bordo verticale
            for(int i=0; i<Aliens.Count; i++)
            {
                if (destra)     // controlla se si stanno muovendo verso destra
                    Aliens[i].transform.position += horizontal;  // sposta a destra ogni alieno
                else          
                    Aliens[i].transform.position -= horizontal;  // sposta a sinistra ogni alieno

                // controlla se l'alieno ha raggiunto il bordo destro o sinistro
                if (Aliens[i].transform.position.x > MAX_RIGHT || Aliens[i].transform.position.x < MAX_LEFT)
                    bordo++;
                if (Aliens[i].transform.position.y < MAX_Y)    // controlla se l'alieno ha raggiunto il bordo in basso
                    bordo2++;
            }
            if (bordo > 0) // controlla se almeno un alieno ha raggiunto il bordo orizzontale
            {
                for (int i = 0; i < Aliens.Count; i++)
                    if (bordo2 <= 0)    // se nessuno ha raggiunto il bordo verticale
                        Aliens[i].transform.position -= vertical;  // sposta in basso tutti gli alieni
                destra = !destra;     // cambia la direzione di movimento degli alieni
            }
        }
        moveTimer = 0;      // ripristina il timer
    }

    // diminuisce il timer per il richiamo del metodo Move()
    public static void UpdateSpeed()
    {
        timerMove -= 0.005f;
        if(timerMove <= 0.009f)     // provare con 0.005 o 0.006
            timerMove = 0.009f;
    }

    // aumenta la velocita dell'animazione per tutti gli alieni presenti
    // nella scena
    public static void AnimSpeed()
    {
        for(int i=0; i<Aliens.Count; i++)
        {
            anim[i].speed += 0.03f;
        }
    }

    // spawna un proiettile nella posizione di un alieno random e resetta il timer
    private void Shoot()
    {
        Vector2 pos = Aliens[Random.Range(0, Aliens.Count)].transform.position;
        Instantiate(bullet, pos, Quaternion.identity);
        shootTimer = 0;
    }

    // spawna la navicella aliena e resetta il timer
    private void Spawn()
    {
        Instantiate(Mothership, MotherShipLocation, Quaternion.identity);
        MotherShipTimer = 0;
    }
}
