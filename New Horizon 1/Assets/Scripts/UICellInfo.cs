using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/** 
 * UI Functions
 **/
public class UICellInfo : MonoBehaviour {
	private Cell cellObj;
	private Player playerObj;

	private Vector3 boxPosition;

	private string infoText;
	private Rect infoRect;

	private bool mOver;

	void Start() {
		// set the info rect to a blank rect
		this.infoRect = new Rect (0, 0, 0, 0);
	}

	private void RenderBox<T>(T obj) {
		StringBuilder sb = new StringBuilder();
		if (obj.GetType().IsSubclassOf(typeof(Cell))) { // cell type
			sb.AppendLine ("Name: " + this.cellObj.name);
			sb.AppendLine ("Health: " + Convert.ToInt32 (this.cellObj.Health));
		} else if (obj.GetType () == typeof(Player)) { // player type
			sb.AppendLine ("Name: " + this.playerObj.name);
			sb.AppendLine ("Health: "/* + Convert.ToInt32 (this.playerObj.Health)*/);
		}
		this.infoText = sb.ToString();
		this.infoRect.Set (this.boxPosition.x, Screen.height - this.boxPosition.y, 120, 150);
	}

	public void SetCellInfo(Cell cObj) {
		this.cellObj = cObj;
		this.boxPosition = Camera.main.WorldToScreenPoint (cellObj.transform.position);
		this.RenderBox (this.cellObj);
	}

	public void SetPlayerInfo(Player pObj) {
		this.playerObj = pObj;
		this.boxPosition = Camera.main.WorldToScreenPoint (pObj.transform.position);
		this.RenderBox (this.playerObj);
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
