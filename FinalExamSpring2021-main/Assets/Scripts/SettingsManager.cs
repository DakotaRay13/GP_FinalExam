using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    /* GAME SETTINGS
     
     * on intro, input field for player name which will show in the game scene, 
     * dropdown for lives which will select 1-9 lives which will be the starting value in the game scene, 
     * and slider for starting time for the countdown timer in the game going from 30 secs to 90 secs.
     */

    public InputField nameField;
    public Dropdown livesDropdown;
    public Slider timeSlider;
    
    void Start()
    {
        
    }

    public void NameChange()
    {
        Settings._PLAYERNAME = nameField.text;
    }

    public void LivesChange()
    {
        Settings._LIVES = livesDropdown.value + 1;
    }

    public void TimeChange()
    {
        Settings._STARTTIME = timeSlider.value;
    }
}
