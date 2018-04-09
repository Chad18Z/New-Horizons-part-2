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

	public Transform pImage;
	public Transform eImage;

	private Vector3 boxPosition;

	private string infoText;
	private Rect infoRect;

	private bool mOver;

	void Start() {
		// set the info rect to a blank rect
		this.infoRect = new Rect (0, 0, 0, 0);

		this.pImage = GameObject.Find("pImage").GetComponent<Transform>();
		this.eImage = GameObject.Find("eImage").GetComponent<Transform>();

		this.pImage.GetComponent<Image> ().enabled = false;
		this.eImage.GetComponent<Image> ().enabled = false;
	}

	private void RenderBox<T>(T obj) {
		StringBuilder sb = new StringBuilder();
		if (obj.GetType().IsSubclassOf(typeof(Cell))) { // cell type
			sb.AppendLine ("Name: " + this.cellObj.name);
			sb.AppendLine ("Health: " + (int)(this.cellObj.Health * 100));

			this.pImage.GetComponent<Image> ().enabled = false;
			this.eImage.GetComponent<Image> ().enabled = true;
		} else if (obj.GetType () == typeof(Player)) { // player type
			sb.AppendLine ("Name: " + this.playerObj.name);
			sb.AppendLine ("Health: "/* + Convert.ToInt32 (this.playerObj.Health)*/);

			this.pImage.GetComponent<Image> ().enabled = true;
			this.eImage.GetComponent<Image> ().enabled = false;
		}

		this.infoText = sb.ToString();
		this.infoRect.Set (this.boxPosition.x, Screen.height - this.boxPosition.y, 120, 150);

	}

	public void SetCellInfo(Cell cObj) {
		this.cellObj = cObj;
		this.boxPosition = Camera.main.WorldToScreenPoint (cellObj.transform.position);
		this.boxPosition.x += 12 * cellObj.GetComponent<CircleCollider2D> ().radius * cellObj.transform.localScale.x;

		this.RenderBox (this.cellObj);
	}

	public void SetPlayerInfo(Player pObj) {
		this.playerObj = pObj;
		this.boxPosition = Camera.main.WorldToScreenPoint (pObj.transform.position);
		this.boxPosition.x += 12 * playerObj.GetComponent<CircleCollider2D> ().radius * playerObj.transform.localScale.x;

		this.RenderBox (this.playerObj);
	}

	void OnGUI() {
		if (this.mOver) {
			// only render the box when the mouse is over the cell
			GUI.Box (this.infoRect, this.infoText, new GUIStyle ("box") { fontSize = 12 });

			Vector3 tempPos = boxPosition;
			tempPos.x += this.infoRect.width / 2;
			tempPos.y -= this.infoRect.height / 2;

			this.pImage.transform.position = tempPos;
			this.eImage.transform.position = tempPos;
		} else {
			this.pImage.GetComponent<Image> ().enabled = false;
			this.eImage.GetComponent<Image> ().enabled = false;
		}
	}

	// check if the mouse is over or not
	public void SetMouseOver(bool v) {
		this.mOver = v;
	}
}
