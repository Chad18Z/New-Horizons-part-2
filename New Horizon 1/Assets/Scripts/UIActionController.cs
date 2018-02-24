using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIActionController : MonoBehaviour {
	// check if user really wants to exit game/return to main menu
	private Transform confirmCanvas;
	private ActionType curAction;

	public Text actionTextField;

	public enum ActionType { NULL, CONFIRM, CANCEL };

	public UIActionController(string action, Transform canvas) {
		this.confirmCanvas = canvas;
		confirmCanvas.gameObject.SetActive (true);

		/* TODO: set text of confirm dialog
		 * 
		 * get the text field for the confirm dialog
		Text txtField = actionTextField.GetComponent<Text>();
		txtField.text.Replace ("{ACTION}", action);*/

		this.curAction = ActionType.NULL;
	}

	public bool CheckAction() {
		return (this.curAction == ActionType.CONFIRM) ? true : false;
	}

	public void ConfirmClick() {
		print ("CONFIRM!");
		this.curAction = ActionType.CONFIRM;
	}

	public void CancelClick() {
		print ("CANCEL!");
		this.curAction = ActionType.CANCEL;
	}
}