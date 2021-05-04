using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    /* GAME SCENE

     * In the Game Scene,
     * give the points display a starting value of 0
     * give the lives display the choice from the previous scene. 
     * make the up and down buttons work for the points
     * make the up and down buttons work for the lives
     * give the countdown the value from the previous scene as starting value, and let it count down to 0. Stop when it hits zero.
     
     * In the PauseCanvas,
     * make it invisible when the scene starts and call it by using the Escape key,
     * create buttons for Continue, Load, Save, New Game, Save as JSON (which prints to the console); and a toggle for the music.
     * Use the music file provided.
     */

    public Text NameText;
    public Text ScoreText;
    public Text LivesText;
    public Text TimeText;

    public float timeRemaining;
    public bool hasTime;
    public int score;
    public int lives;

    public GameObject PauseMenu;
    public bool isPaused;

    public Toggle musicToggle;
    public AudioSource musicSource;

    void Start()
    {
        Debug.Log("Game Start!");

        NameText.text = PlayerPrefs.GetString("playerName", "Player");
        if (NameText.text == "")
        {
            NameText.text = "Player";
            Debug.Log("Player Name was Empty.");
        }
        
        score = 0;
        ScoreText.text = score.ToString();

        lives = PlayerPrefs.GetInt("lives", 1);
        LivesText.text = lives.ToString();

        timeRemaining = PlayerPrefs.GetFloat("startTime", 30f);
        TimeText.text = timeRemaining.ToString("0");
        hasTime = true;

        SetMusicStart();

        UnpauseGame();
    }

    void Update()
    {
        if (!isPaused && hasTime)
        {
            timeRemaining -= Time.deltaTime;
            TimeText.text = timeRemaining.ToString("0");

            if(timeRemaining <= 0)
            {
                //ASK IF WE NEED TO END THE GAME OR JUST THE COUNTDOWN!
                hasTime = false;
                timeRemaining = 0;
                TimeText.text = "0";
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused) PauseGame();
            else         UnpauseGame();
        }
    }

    //Score Button: if set, increase score. else, decrease.
    public void Buttons_Score(bool set)
    {
        if(!isPaused)
        {
            if (set) score += 1;
            else score -= 1;

            ScoreText.text = score.ToString();
        }
    }

    //Lives Button: if set, increase lives. else, decrease.
    public void Buttons_Lives(bool set)
    {
        if (!isPaused)
        {
            if (set) lives += 1;
            else lives -= 1;

            LivesText.text = lives.ToString();
        }
    }

    public void SetUI_Text()
    {
        TimeText.text = timeRemaining.ToString("0");
        ScoreText.text = score.ToString();
        LivesText.text = lives.ToString();
    }

    public void SetMusicStart()
    {
        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetInt("music", 1);
            musicToggle.isOn = true;
            musicSource.enabled = true;
            PlayerPrefs.Save();
        }

        else
        {
            if (PlayerPrefs.GetInt("music") == 0)
            {
                musicSource.enabled = false;
                musicToggle.isOn = false;
            }
            else
            {
                musicSource.enabled = true;
                musicToggle.isOn = true;
            }
        }
    }

    //*******************************************************************************//
    //  PAUSE MENU FUNCTIONS

    public void PauseGame()
    {
        isPaused = true;
        PauseMenu.SetActive(true);
    }

    public void UnpauseGame()
    {
        isPaused = false;
        PauseMenu.SetActive(false);
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        save.PlayerName = NameText.text;
        save.Score = score;
        save.Lives = lives;
        save.TimeRemaining = timeRemaining;

        return save;
    }

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            NameText.text = save.PlayerName;
            score = save.Score;
            lives = save.Lives;
            timeRemaining = save.TimeRemaining;
            SetUI_Text();

            Debug.Log("Game Loaded");

            hasTime = true;

            UnpauseGame();
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

    public void SaveAsJSON()
    {
        Save save = CreateSaveGameObject();
        string json = JsonUtility.ToJson(save);

        Debug.Log("Saving as JSON: " + json);
    }

    public void SetMusic()
    {
        musicSource.enabled = musicToggle.isOn;

        if (musicToggle.isOn) PlayerPrefs.SetInt("music", 1);
        else PlayerPrefs.SetInt("music", 0);
    }
}

[System.Serializable]
public class Save
{
    public string PlayerName = "";
    public int Score = 0;
    public int Lives = 0;
    public float TimeRemaining = 1;

    public int Music = 1;
}

public class Score
{
    public static string pName;
    public static int score;
}