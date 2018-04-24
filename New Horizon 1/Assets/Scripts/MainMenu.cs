using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void PlayBtn_Handler() {
		SceneManager.LoadScene("New Gameplay");
    }

    public void TutorialBtn_Handler() {
		// load tutorial scene
        SceneManager.LoadScene("Tutorial");
    }

    public void OptionsBtn_Handler() {
        // Object.Instantiate(Resources.Load("HelpBG"));
    }

	public void QuitBtn_Handler() {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}

    //go back to menu
    public static void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
