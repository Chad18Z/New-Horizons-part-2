using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void HandleAlertButtonOnCLick()
    {
        Object.Instantiate(Resources.Load("TextBG"));
        Object.Instantiate(Resources.Load("MissionCanvas"));
        
    }

    public void HandleStartButtonOnClick()
    {
        SceneManager.LoadScene("New Gameplay");
    }

    public void HandleHelpButtonOnClick()
    {
        Object.Instantiate(Resources.Load("HelpBG"));
    }

    //go back to menu
    public static void GoToMenu()
    {
        SceneManager.LoadScene("MainBody");
    }

    //go back to menu
    public void Retry()
    {
        SceneManager.LoadScene("New Gameplay");
    }
}
