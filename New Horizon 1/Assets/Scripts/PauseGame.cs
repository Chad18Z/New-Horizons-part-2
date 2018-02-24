using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseGame : MonoBehaviour {

	// canvas instance
	public Transform screenCanvas;
	public Transform confirmCanvas;

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) { // escape is pressed
				this.Pause();
		}
	}

	public void Pause() {
		if (!screenCanvas.gameObject.activeInHierarchy) { // pausemenu is not being shown
			this.screenCanvas.gameObject.SetActive (true); // show pause
			Time.timeScale = 0; // prevent game from running
		} else {
			if(confirmCanvas.gameObject.activeInHierarchy)
				confirmCanvas.gameObject.SetActive (true);
			else{
				this.screenCanvas.gameObject.SetActive (false); // resume game
				Time.timeScale = 1; // return to game
			}
		}
	}

	// TODO: Add confirmation dialog
	public void ReturnToMainMenu() {
		/*UIActionController actionCheck = new UIActionController ("Return to Main Menu", this.confirmCanvas);
		if (actionCheck.CheckAction()) {
			print ("WENT THROUGH!");
		}*/
		Time.timeScale = 1;
		MainMenu.GoToMenu ();
	}

	// TODO: Add confirmation dialog
	public void ExitGame() {
		/*UIActionController actionCheck = new UIActionController ("Exit Game", this.confirmCanvas);
		if (actionCheck.CheckAction ()) {

		}*/
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}