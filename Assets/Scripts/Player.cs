using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;      // proiettile che spara la navicella
    //[SerializeField]
    private float speed=3;            // velocit� di movimento
    [SerializeField]
    GameObject destroyed;

    [SerializeField]
    GameObject particleHearth;

    [SerializeField]
    GameObject particleSpeed;

    public AudioClip shoot;     // audio per lo sparo
    public AudioClip destroy;   // audio per quando viene colpito

    private int lives=3;        // vite del giocatore   

    private const float MAX_LEFT = -4.45f;      // bordo sinistro
    private const float MAX_RIGHT = 4.45f;      // bordo destro
    private bool shooting; // per controllare se sta sparando
    public static bool pepsi;  //guardare pepsi capovolta per capire (isded)
    private bool invincible = false;

    private Vector2 start = new Vector2(0, -3.1f);  // posizione iniziale
    private Vector2 dead = new Vector2(0, -4);      // posizione fuori dalla telecamera
    private Vector2 ciao = new Vector2(0, -3.224f);

    // Start is called before the first frame update
    void Start()
    {
        pepsi = false;
        lives = 3;
        transform.position = start;
        UI.Instance().UpdateLives(lives);
        //Debug.Log("Vite: " + lives);
    }

    public void ResetPlayer()
    {
        lives = 3;
        transform.position = start;
        UI.Instance().UpdateLives(lives);
        //Debug.Log("Vite ripristinate: " + lives);
    }

    // Update is called once per frame
    void Update()
    {

        // Movimento verso sinistra
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > MAX_LEFT)
            transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
        // Movimento verso destra
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < MAX_RIGHT)
            transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
        // controlla se il tasto � spazio, se non sta gi� sparando e se non � morto
        // e poi avvia la coroutine per sparare
        if (Input.GetKey(KeyCode.Space) && shooting==false && pepsi == false && MenuManager.Instance().running==true && MenuManager.Instance().credit==false)
            StartCoroutine(Shoot());
    }

    // spara un proiettile ed attende prima di sparare di nuovo (come nel gioco originale) 
    private IEnumerator Shoot()
    {
        shooting = true;
        Instantiate(bullet, transform.position, Quaternion.identity);
        AudioManager.Instance().PlaySound(shoot);
        yield return new WaitForSeconds(0.5f);
        shooting = false;
    }


    // quando viene colpito dimunuisce il numero di vite, aggiorna le vite nella UI. 
    // se le vite sono < 0 apre la schermata di game over e resetta il giocatore
    // altrimenti richiama la coroutine respawn
    public void Damage()
    {
        if (invincible == false)
        {
            lives--;
            Debug.Log("Vite: " + lives);
            UI.Instance().UpdateLives(lives);
            if (lives <= 0)
            {
                AudioManager.Instance().PlaySound(destroy);
                MenuManager.Instance().OpenGameOver();
                ResetPlayer();
            }
            else
            {
                pepsi = true;
                StartCoroutine(Respawn());
            }
        }
    }

    private IEnumerator Respawn()
    {
        ciao.x = transform.position.x;
        AudioManager.Instance().PlaySound(destroy);    // avvia il suono della distruzione del giocatore
        Instantiate(destroyed, ciao, Quaternion.identity);
        transform.position = dead;          // sposta il giocatore fuori dalla telecamera
        yield return new WaitForSeconds(2); // aspetta 2 secondi
        pepsi = false;                      // imposta pepsi a false
        transform.position = start;         // sposta il giocatore nella posizione di partenza
        StartCoroutine(Invulnerable());
    }

    private IEnumerator Invulnerable()
    {
        invincible = true;
        yield return new WaitForSeconds(1.5f);
        invincible = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))     // se viene colpito da un proiettile nemico
        {                                                       
            Destroy(collision.gameObject);                      // distrugge il proiettile
            Damage();                                           // richiama la funzione Damage
        }
    }

    public void PickLife()
    {
        if (lives < 3)  // controlla se le vite sono < 3
        {
            Instantiate(particleHearth, transform.position, Quaternion.identity);
            lives++;    // aumenta il numero di vite disponibili
            UI.Instance().UpdateLives(lives);  // aggiorna le vite nella UI
        }
    }

    public void PickSpeed()
    {
        Instantiate(particleSpeed, transform.position, Quaternion.identity);
        speed = 6;
        StartCoroutine(SpeedUp());
    }

    public IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(3);
        speed = 3;
    }
}
