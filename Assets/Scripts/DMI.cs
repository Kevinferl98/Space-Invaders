using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMI : MonoBehaviour
{
    private const float MAX_LEFT = -3.29f;      // bordo sinistro
    private const float MAX_RIGHT = 3.29f;      // bordo destro
    private const float START = 2.3f;           // posizione di partenza

    enum State { calm, angry};      // stati in cui si puo trovare

    private bool enter = true;     // per controllare l'entrata
    private bool right = true;      // per controllare il movimento orizzontale

    private int health = 15;        // saluta del boss

    private float speed = 10;        // velocita di movimento
    private float shootTimer = 0;  // timer per sparare
    private float shootTime = 1f;

    public int score;               // punteggio 
    public GameObject bullet;       // proiettile
    public GameObject explode;      // espolsione
    SpriteRenderer spriter;         // serve per cambiare colore e per farlo apparire lentamente

    State current = State.calm;     // parte nello stato di calmo

    [SerializeField]
    SpriteRenderer damaged;

    // Start is called before the first frame update
    void Start()
    {
        spriter = GetComponent<SpriteRenderer>();
        StartCoroutine(Appearing());
    }

    // Update is called once per frame
    void Update()
    {
        if (current==State.angry)   // nello stato di angry diventa piu veloce e spara piu spesso
        {
            spriter.sprite = damaged.GetComponent<SpriteRenderer>().sprite;
            speed = 13;
            shootTime = 0.7f;
        }

        MoveDMI();

        if (shootTimer >= shootTime && enter==false)   
            shoot();
        shootTimer += Time.deltaTime;
    }

    void MoveDMI()
    {
        if (enter == false)
        {
            if (right)  // lo sposta verso destra
            {
                transform.position += new Vector3(0.5f, 0, 0) * speed * Time.deltaTime;
                if (transform.position.x >= MAX_RIGHT)
                    right = !right;
            }
            else        // se ha colpito il bordo destro lo sposta verso sinistra
            {
                transform.position -= new Vector3(0.5f, 0, 0) * speed * Time.deltaTime;
                if (transform.position.x <= MAX_LEFT)
                    right = !right;
            }
        }
    }

    public void shoot()     // lancia il libro di analisi (ovviamente)
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
        shootTimer = 0;
    }

    public void Damage()
    {
        if (enter == false)
        {
            StartCoroutine(changecolor());
            health--;
            if (health <= 5)    // se scende sotto 5hp cambia stato 
            {
                current = State.angry;
            }
            if (health <= 0)    // se muore instanzia l'esplosione, viene distrutto e richiama il GameManager
            {
                Instantiate(explode, transform.position, Quaternion.identity);
                UI.Instance().UpdateScore(score);
                GameManager.Instance().NextLevel();
                Destroy(gameObject);
            }
        }
    }

    // cambia il canale alpha per far in modo che appaia lentamente all'inzio del livello
    private IEnumerator Appearing()
    {
        float alpha = 0.1f;
        for(int i=0; i<10; i++)
        {
            spriter.color = new Color(1f, 1f, 1f, alpha);
            yield return new WaitForSeconds(0.15f);
            alpha += 0.1f;
        }
        enter = false;
    }

    private IEnumerator changecolor()   // cambia colore in rosso cosi da dare l'effetto di averlo colpito
    {
        spriter.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        spriter.color = Color.white;
    }
}
