using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 
 * UI Functions
 **/
public class UICellInfo : MonoBehaviour {
	private Cell cellObj;
	private Vector3 cellPosition;

	private string infoText;
	private Rect infoRect;

	private bool mOver;

	void Start() {
		// set the info rect to a blank rect
		this.infoRect = new Rect (0, 0, 0, 0);
	}

	public void SetCellInfo(Cell cObj) {
		this.cellObj = cObj;
		// gets the cell position on the screen from the camera's view
		this.cellPosition = Camera.main.WorldToScreenPoint (cellObj.transform.position);

		// filler info text shows the cell name. health -> cellObj.getHealth() or similar
		this.infoText = "Name: " + this.cellObj.name + "\nHealth: n/a"; 
		// set the rect to the position of the cell
		this.infoRect.Set (cellPosition.x, Screen.height - cellPosition.y, 120, 75);
	}

	void OnGUI() {
		if (this.mOver) {
			// only render the box when the mouse is over the cell
			GUI.Box (this.infoRect, this.infoText, new GUIStyle("box") { fontSize = 12 });
		}
	}

	// check if the mouse is over or not
	public void SetMouseOver(bool v) {
		this.mOver = v;
	}
}
