using UnityEngine;

//Static Variables to be used for the game settings
public class Settings
{
    public static string _PLAYERNAME;
    public static int    _LIVES;
    public static float  _STARTTIME;

    //Save the settings into PlayerPrefs
    public static void SaveSettings()
    {
        PlayerPrefs.SetString("playerName", _PLAYERNAME);
        PlayerPrefs.SetInt("lives", _LIVES);
        PlayerPrefs.SetFloat("startTime", _STARTTIME);

        PlayerPrefs.Save();
    }
}
