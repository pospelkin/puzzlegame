using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public enum GameState
{
    menu,
    inGame,
    gameOver,
    pouse
}

public class GameBehavior : MonoBehaviour
{
    public static GameBehavior instance;
    public Canvas menuCanvas;
    public Canvas inGameCanvas;
    public Canvas gameOverCanvas;
    public Canvas pouseCanvas;
    public GameState currentGameState = GameState.menu;
    public bool ispuse;
    public Timer mUiTimer = null;
    private string mTimeOver;
    private float mUnixtimeOver;
    public HighscoreTable transformTable;
    private List<HighscoreEntry> highscoreEntryList;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        currentGameState = GameState.menu;
    }
    public void StartGame()
    {
        mUiTimer.BeginTimer();
        Puzzle.instance.StartGame();
        SetGameState(GameState.inGame);
    }

    public void GameOver()
    {
        mUiTimer.EndTimer();

        GameObject[] boxes = GameObject.FindGameObjectsWithTag("numbox");
        foreach (GameObject box in boxes)
            GameObject.Destroy(box);

        GameObject[] tableList = GameObject.FindGameObjectsWithTag("tablelist");
        foreach (GameObject tL in tableList)
            GameObject.Destroy(tL);

        string mTimeOver = Timer.instance.timerCounter.text;
        float mUnixtimeOver = Timer.instance.elapsedTime;

        AddHighscoreEntry(mUnixtimeOver, mTimeOver);
        SetGameState(GameState.gameOver);
    }

    public void BackToMenu()
    {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("numbox");
        foreach (GameObject box in boxes)
            GameObject.Destroy(box);

        GameObject[] tableList = GameObject.FindGameObjectsWithTag("tablelist");
        foreach (GameObject tL in tableList)
            GameObject.Destroy(tL);

        transformTable.TransformTable();
        SetGameState(GameState.menu);
    }

    public void IsPouse()
    {
        mUiTimer.EndTimer();
        SetGameState(GameState.pouse);
        if (ispuse == false)
        {
            ispuse = true;
        }
    }
    public void NoPouse()
    {
        mUiTimer.NoPouseTimer();
        SetGameState(GameState.inGame);
        if (ispuse == true)
        {
            ispuse = false;
        }
    }
    void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            menuCanvas.enabled = true;
            inGameCanvas.enabled = false;
            gameOverCanvas.enabled = false;
            pouseCanvas.enabled = false;
        }
        if (newGameState == GameState.pouse)
        {
            menuCanvas.enabled = false;
            inGameCanvas.enabled = true;
            gameOverCanvas.enabled = false;
            pouseCanvas.enabled = true;
        }
        else if (newGameState == GameState.inGame)
        {
            menuCanvas.enabled = false;
            inGameCanvas.enabled = true;
            gameOverCanvas.enabled = false;
            pouseCanvas.enabled = false;
        }
        else if (newGameState == GameState.gameOver)
        {
            menuCanvas.enabled = false;
            inGameCanvas.enabled = false;
            gameOverCanvas.enabled = true;
            pouseCanvas.enabled = false;
        }


        currentGameState = newGameState;
    }

    private void AddHighscoreEntry(float score, string name)
    {   // Создю HighscoreEntry 
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        // Загрузка сохранений Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // Добавляю новую запись в Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        // Сохраняю обновления Highscores 
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }
    public class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    public class HighscoreEntry
    {
        public float score;
        public string name;
    }

    public void HighscoreDelete()
    {
        PlayerPrefs.DeleteKey("highscoreTable");

        Highscores highscores = new Highscores { highscoreEntryList = highscoreEntryList };
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        
        PlayerPrefs.Save();


        GameObject[] tableList = GameObject.FindGameObjectsWithTag("tablelist");
        foreach (GameObject tL in tableList)
            GameObject.Destroy(tL);

        transformTable.TransformTable();
    }
}
