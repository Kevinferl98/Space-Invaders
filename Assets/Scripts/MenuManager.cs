using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject GameOver;
    public GameObject inGame;
    public GameObject Credits;
    public GameObject quit;
    public GameObject tutorial;
    public GameObject tutorial2;
    public GameObject pause;
    public GameObject options;
    public GameObject player;
    [SerializeField]
    new AudioClip audio;        // audio del menu principale

    [SerializeField]
    AudioClip GameOverAudio;

    [SerializeField]
    AudioClip buttonSound;

    public bool running = false;
    public bool over = false;
    public bool credit = false;
    bool ps = false;
    bool panelOpen = false;

    private static MenuManager instance;     // Singleton

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public static MenuManager Instance(){
        return instance;
    }

    private void Start()
    {
        quit.transform.localScale = Vector2.zero;
        tutorial.transform.localScale = Vector2.zero;
        tutorial2.transform.localScale = Vector2.zero;
        options.transform.localScale = Vector2.zero;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && running == false)
        {
            AudioManager.Instance().StopSound();
            OpenInGame();
            running = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && ps == false && running==true)
        {
            OpenPause();
        }
        if (Input.GetKeyDown(KeyCode.Return) && over == true)
        {
            //AudioManager.PlaySound(audio);
            AudioManager.Instance().StopSound();
            ReturnToMainMenu();
            over = false;
        }
        if(Input.GetKeyDown(KeyCode.Escape) && credit == true)
        {
            AudioManager.Instance().StopCredits();
            AudioManager.Instance().PlaySound(audio);
            ReturnToMainMenu();
            credit = false;
        }
    }

    public void OpenMainMenu()
    {
        AudioManager.Instance().PlaySound(audio);
        MainMenu.SetActive(true);
        inGame.SetActive(false);
        GameOver.SetActive(false);
        Credits.SetActive(false);
        quit.SetActive(false);
        tutorial.SetActive(false);
        tutorial2.SetActive(false);
        pause.SetActive(false);
        options.SetActive(false);
    }

    public void OpenGameOver()
    {
        AudioManager.Instance().StopBattle();
        AudioManager.Instance().StopFinalBoss();
        AudioManager.Instance().StopMS();
        AudioManager.Instance().PlaySound(GameOverAudio);
        Time.timeScale = 0;
        GameOver.SetActive(true);
        inGame.SetActive(false);
        Credits.SetActive(false);
        quit.SetActive(false);
        tutorial.SetActive(false);
        pause.SetActive(false);
        options.SetActive(false);
        over = true;
    }

    public void OpenPause()
    {
        //AudioManager.StopBattle();
        pause.transform.localScale = Vector2.one;
        Time.timeScale = 0f;
        GameOver.SetActive(false);
        inGame.SetActive(false);
        Credits.SetActive(false);
        quit.SetActive(false);
        tutorial.SetActive(false);
        options.SetActive(false);
        pause.SetActive(true);
        ps = true;
    }

    public void ClosePause()
    {
        //AudioManager.PlayBattle();
        Time.timeScale = 1f;
        pause.transform.LeanScale(Vector2.zero, 0.5f).setEaseInBack();
        GameOver.SetActive(false);
        inGame.SetActive(true);
        Credits.SetActive(false);
        quit.SetActive(false);
        tutorial.SetActive(false);
        pause.SetActive(false);
        options.SetActive(false);
        ps = false;
    }

    public void OpenInGame()
    {
        AudioManager.Instance().PlayBattle();
        MainMenu.SetActive(false);
        GameOver.SetActive(false);
        Credits.SetActive(false);
        inGame.SetActive(true);
        quit.SetActive(false);
        tutorial.SetActive(false);
        pause.SetActive(false);
        options.SetActive(false);
        GameManager.Instance().NextLevel();
    }

    public void OpenCredits()
    {
        //AudioManager.StopMS();
        //AudioManager.StopBattle();
        //AudioManager.StopAlien();
        //AudioManager.PlayCredits();
        MainMenu.SetActive(false);
        GameOver.SetActive(false);
        inGame.SetActive(false);
        quit.SetActive(false);
        tutorial.SetActive(false);
        pause.SetActive(false);
        options.SetActive(false);
        Credits.SetActive(true);
        credit = true;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        GameManager.Instance().Cancel();
        //Time.timeScale = 1;
        Credits.SetActive(false);
        GameOver.SetActive(false);
        inGame.SetActive(false);
        quit.SetActive(false);
        tutorial.SetActive(false);
        pause.SetActive(false);
        options.SetActive(false);
        MainMenu.SetActive(true);
        running = false;
        ps = false;
        AudioManager.Instance().StopBattle();
        AudioManager.Instance().StopFinalBoss();
        //GameManager.Cancel();
        AudioManager.Instance().PlaySound(audio);
    }

    public void OpenQuit()
    {
        if (panelOpen == false)
        {
            panelOpen = true;
            AudioManager.Instance().PlaySound(buttonSound);
            quit.SetActive(true);
            quit.transform.LeanScale(Vector2.one, 0.5f);
        }
    }

    public void OpenOptions()
    {
        if (panelOpen == false)
        {
            panelOpen = true;
            AudioManager.Instance().PlaySound(buttonSound);
            options.SetActive(true);
            options.transform.LeanScale(Vector2.one, 0.5f);
        }
    }

    public void CloseOptions()
    {
        panelOpen = false;
        AudioManager.Instance().PlaySound(buttonSound);
        options.transform.LeanScale(Vector2.zero, 0.5f).setEaseInBack();
        //instance.options.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CloseQuit()
    {
        panelOpen = false;
        AudioManager.Instance().PlaySound(buttonSound);
        quit.transform.LeanScale(Vector2.zero, 0.5f).setEaseInBack();
        //instance.quit.SetActive(false);
    }

    public void OpenTutorial()
    {
        AudioManager.Instance().PlaySound(buttonSound);
        tutorial.SetActive(true);
        tutorial.transform.LeanScale(Vector2.one, 0.5f);
    }

    public void CloseTutorial()
    {
        AudioManager.Instance().PlaySound(buttonSound);
        tutorial.transform.LeanScale(Vector2.zero, 0.5f).setEaseInBack();
        tutorial2.transform.LeanScale(Vector2.zero, 0.5f).setEaseInBack();
        //tutorial.SetActive(false);
    }

    public void OpenTutorial2()
    {
        AudioManager.Instance().PlaySound(buttonSound);
        CloseTutorial();
        tutorial2.SetActive(true);
        tutorial2.transform.LeanScale(Vector2.one, 0.5f);
    }

    public void backToTutorial()
    {
        AudioManager.Instance().PlaySound(buttonSound);
        tutorial2.transform.LeanScale(Vector2.zero, 0.5f).setEaseInBack();
        tutorial.transform.LeanScale(Vector2.one, 0.5f);
    }

}
