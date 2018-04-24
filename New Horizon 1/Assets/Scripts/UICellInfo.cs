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
	private Vector3 tempPosition;

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
		bool pEnabled = false, eEnabled = false;
		StringBuilder sb = new StringBuilder();
		if (obj.GetType().IsSubclassOf(typeof(Cell))) { // cell type
			sb.AppendLine ("Name: " + this.cellObj.name);
			sb.AppendLine ("Health: " + (int)(this.cellObj.Health * 100));

			pEnabled = false;
			eEnabled = true;

			UpdatePlayerObject();

			if (this.playerObj.transform.position.x > this.cellObj.transform.position.x) {
				this.UpdateBoxPosition ();
				this.tempPosition.x -= this.infoRect.width + (10 * this.cellObj.GetComponent<CircleCollider2D> ().radius * this.cellObj.transform.localScale.x);
			}
			if (this.playerObj.transform.position.y < this.cellObj.transform.position.y) {
				this.tempPosition.y += this.infoRect.height + (10 * this.cellObj.GetComponent<CircleCollider2D> ().radius * this.cellObj.transform.localScale.y);
			}

			if (this.isOutsideScreen (this.tempPosition)) {
				this.CheckScreenBounds (this.tempPosition);
			}

		} else if (obj.GetType () == typeof(Player)) { // player type
			sb.AppendLine ("Name: " + this.playerObj.name);
			sb.AppendLine ("Health: "/* + Convert.ToInt32 (this.playerObj.Health)*/);

			pEnabled = true;
			eEnabled = false;
		}

		this.boxPosition = this.tempPosition;

		this.infoText = sb.ToString();
		this.infoRect.Set (this.boxPosition.x, Screen.height - this.boxPosition.y, 120, 150);
		this.pImage.GetComponent<Image> ().enabled = pEnabled;
		this.eImage.GetComponent<Image> ().enabled = eEnabled;
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
			// TODO: Fix info image "snapping" from previous pos to the new pos
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

	private void UpdateBoxPosition() {
		if(this.cellObj != null)
			this.tempPosition = Camera.main.WorldToScreenPoint (this.cellObj.transform.position);
	}
					
	private void CheckScreenBounds(Vector3 coords) {
		this.UpdateBoxPosition ();
		// right
		if ((coords.x + this.infoRect.width) > Screen.width) {
			this.tempPosition.x -= this.infoRect.width + (10 * this.cellObj.GetComponent<CircleCollider2D> ().radius * this.cellObj.transform.localScale.x);
		}
		// left
		if (coords.x < 0) {
			this.tempPosition.x += 12 * this.playerObj.GetComponent<CircleCollider2D> ().radius * this.playerObj.transform.localScale.x;
		}
		// top
		if (coords.y > Screen.height) {
			this.tempPosition.y -= (10 * this.cellObj.GetComponent<CircleCollider2D> ().radius * this.cellObj.transform.localScale.y);
		}
		// bottom
		if ((coords.y - this.infoRect.height) < 0) {
			this.tempPosition.y += this.infoRect.height + (10 * this.cellObj.GetComponent<CircleCollider2D> ().radius * this.cellObj.transform.localScale.y);
		}
	}
	private bool isOutsideScreen(Vector3 coords) {
		if ( ((coords.x + this.infoRect.width) > Screen.width) || (coords.x < 0) || (coords.y > Screen.height) || ((coords.y - this.infoRect.height) < 0) )
			return true;
		return false;
	}

	private void UpdatePlayerObject() {
		if (this.playerObj == null)
			this.playerObj = GameObject.Find ("Player").GetComponent<Player>();
	}
}