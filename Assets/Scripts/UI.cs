using UnityEngine;
using UnityEngine.UI;

// Singleton che gestice tutta l'interfaccia durante il gioco
// deve contenere score, hi-score, level, vite (numeri o sprite)

public class UI : MonoBehaviour
{
    public Text scoreText;
    private int score;

    public Text highscoreText;
    private int highscore;

    public Text levelText;
    private int level;

    public Text livesText;
    private int lives;
    public Image[] livesImage;

    private static UI instance;     // Singleton

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        highscore = PlayerPrefs.GetInt("high");
        UpdateHighScore(highscore);

    }

    public static UI Instance()
    {
        return instance;
    }

    // aggiorna il numero di vite presenti su schermo ad ogni morte o riavvio
    public void UpdateLives(int l)
    {
        lives = l;
        livesText.text = lives.ToString();
        foreach (Image i in livesImage)
            i.color = new Color(1, 1, 1, 0);

        for(int i=0; i<l; i++)
        {
            livesImage[i].color = new Color(1,1,1,1);
        }
    }

    // aggiorna il punteggio presente su schermo
    public void UpdateScore(int s)
    {
        score += s;
        scoreText.text = score.ToString("0000");
    }

    // ritorna il punteggio della seguente partita completata
    public int getScore()
    {
        return score;
    }

    // aggiorna il punteggio massimo 
    public void UpdateHighScore(int h)
    {
        if (h >= highscore)
        {
            highscore = h;
            highscoreText.text = highscore.ToString("0000");
        }
    }

    // aggiorna il numero del livello 
    public void UpdateLevel()
    {
        level++;
        levelText.text = level.ToString("00");
    }


    // ritorna il punteggio massimo
    public int GetHigh()
    {
        return highscore;
    }


    // resetta tutti i punteggi e le vite a schermo
    public  void ResetUI()
    {
        score = 0;
        level = 0;
        scoreText.text = score.ToString("0000");
        levelText.text = level.ToString("00");
        UpdateLives(3);
    }
}
