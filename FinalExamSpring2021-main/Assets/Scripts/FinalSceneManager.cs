using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneManager : MonoBehaviour
{
    /* SCENE MANAGEMENT

     * on intro, button "play" switches to game scene; 
     * on game scene, button "stop" takes game to exit scene , 
     * in exit scene button "new game" restarts the game from 0 and button "quit" exits the game 
     */

    public string StartScene = "1Intro";
    public string GameScene = "2Game";
    public string ExitScene = "3Exit";

    public void PlayGame()
    {
        Debug.Log(Settings._PLAYERNAME);
        Debug.Log(Settings._LIVES);
        Debug.Log(Settings._STARTTIME);

        Settings.SaveSettings();

        SceneManager.LoadScene(GameScene);
    }

    public void ExitGame()
    {
        Score.pName = FindObjectOfType<GameManager>().NameText.text;
        Score.score = FindObjectOfType<GameManager>().score;
        SceneManager.LoadScene(ExitScene);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(GameScene);
    }
    
    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
