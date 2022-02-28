using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GameObject[] Enemy;           // contiene i nemici
    public NewShield[] shield;           // contiene gli scudi 
    private GameObject currentEnemy;     // nemico corrente 
    public GameObject pl;               // giocatore
    private Vector2 spawnPosition = new Vector2(0, 5.85f);  // posizione di spawn alieni
    private Vector2 spawnDMI = new Vector2(0, 2.3f);        // posizione di spawn boss
    private static GameManager instance;                    // singleton
    private int level = 0;                          // numero del livello 
    private int score = 0;                          // punteggio
    private bool finish = false;
    private bool arcade = false;
    [SerializeField]
    Text arcadeOFF;

    [SerializeField]
    Text arcadeON;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public static GameManager Instance(){
        return instance;
    }

    private void Start()
    {
        MenuManager.Instance().OpenMainMenu();    // all'inizio apre il MainMenu
        arcadeON.GetComponent<Text>().enabled = false;
    }

    // Spawna il nemico successivo 
    public void NextLevel()
    {
        level++;           // aumenta il numero del livello 
        if (level == 5 && arcade==false)    
        {
            finish = true;
        }
        if (finish == true)      
        {
            if (currentEnemy != null)  
                Destroy(currentEnemy);     // distrugge il nemico corrente
            StartCoroutine(time());   // inizia la coroutine
        }
        else
        {
            for(int i=0; i<4; i++)
            {
                shield[i].restart();   // ripristina gli scudi
            }
            StartCoroutine(Spawn());  // inizia la coroutine
        }
    }

    // serve a ripristinare la partita ad ogni Game Over o ritorno al MainMenu
    public void Cancel()
    {
        finish = false;
        StopAllCoroutines();   // interrompe tutte le coroutine
        AlienSet.Aliens.Clear();      // rimuove tutti gli elementi dalla lista Aliens
        AlienSet.anim.Clear();        // rimuove tutti gli elementi dalla lista anim
        level = 0;             // riporta il numero del livello a 0

        if (currentEnemy != null)
            Destroy(currentEnemy);     // distrugge il nemico corrente

        score = UI.Instance().getScore();         // salva il punteggio dentro score
        UI.Instance().ResetUI();                           // resetta la UI
        pl.GetComponent<Player>().ResetPlayer();   // resetta il giocatore
        AudioManager.Instance().StopAlien();               // ferma il suono degli alieni
        UI.Instance().UpdateHighScore(score);     // aggiorna l'high-score
        Save.save(score);              // salva il punteggio
        for (int i = 0; i < 4; i++)             // ripristina gli scudi
        {
            shield[i].restart();
        }

    }

    // serve a spawnare i nemici ad ogni livello
    private IEnumerator Spawn()
    {
        AudioManager.Instance().StopAlien();       // ferma il suono degli alieni
        AlienSet.Aliens.Clear();      // rimuove tutti gli elementi dalla lista Aliens
        //AudioManager.Instance().UpdateTime();    // aggiorna il delay del suondo degli alieni a 1 secondo

        if (currentEnemy != null)
            Destroy(currentEnemy);      // distrugge il nemico corrente

        yield return new WaitForSeconds(2);     // attende 2 secondi prima di spawnare il nuovo nemico
        if (level == 4 && arcade==false)                         // al livello 4 spawna il boss finale
        {
            AudioManager.Instance().StopBattle();
            AudioManager.Instance().PlayFinalBoss();
            currentEnemy = Instantiate(Enemy[1], spawnDMI, Quaternion.identity);
        }
        else
        {
            currentEnemy = Instantiate(Enemy[0], spawnPosition, Quaternion.identity);
            AudioManager.Instance().PlayAlien();
        }
        UI.Instance().UpdateLevel();       // aggiorna il livello nella UI
    }

    // serve per avviare i crediti finali
    private IEnumerator time()
    {
        AudioManager.Instance().StopAlien();
        AudioManager.Instance().StopBattle();
        AudioManager.Instance().PlayCredits();
        AudioManager.Instance().StopMS();
        AudioManager.Instance().StopFinalBoss();
        yield return new WaitForSeconds(1.5f);
        MenuManager.Instance().OpenCredits();
    }

    // permette di cambiare il gioco da Arcade a modalità con crediti finali
    public void changeArcade()
    {
        arcade = !arcade;
        if (arcade == true)
        {
            arcadeON.GetComponent<Text>().enabled = true;
            arcadeOFF.GetComponent<Text>().enabled = false;
        }
        else
        {
            arcadeON.GetComponent<Text>().enabled = false;
            arcadeOFF.GetComponent<Text>().enabled = true;
        }
    }
}
