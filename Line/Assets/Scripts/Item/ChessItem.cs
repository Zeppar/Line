using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum Type {
	Cow,
	Hen
}

public class ChessItem : MonoBehaviour, IPointerClickHandler/*, IBeginDragHandler, IEndDragHandler*/ {

	//if it is lined to other items
	public bool lined = false;
	public int curItemIndex = 0;
	public List<int> linedItemIndexs = new List<int> (4);
	public Image cowImage;
	public Image henImage;
	public Image flagImage;
	public Image selectImage;
	bool isActive = false;
	bool isSelected = false;
	public Type curType = Type.Cow;
	Vector2 startPos;
	Vector2 endPos;

	void Start() {
	
	}

	void Update() {
	
	}

	public void OnPointerClick(PointerEventData data) {
		print ("Length : " + linedItemIndexs.Count);
		if (!GameController.manager.gameStart)
			return;
		if (!isSelected) {
			ActivateLineItem ();
		}
		else
			GameController.manager.ConvertIndex (curItemIndex, linedItemIndexs [0]);
	}

//	public void OnBeginDrag(PointerEventData data) {
//		print ("Begin");
//		print ("Begin : " + data.position);
//	}
//
//	public void OnEndDrag(PointerEventData data) {
//		print ("End");
//		print ("End : " + data.position);
//	}

	public void Convert() {
		if (curType == Type.Cow) {
			curType = Type.Hen;
			cowImage.gameObject.SetActive (false);
			henImage.gameObject.SetActive (true);
		} else {
			curType = Type.Cow;
			cowImage.gameObject.SetActive (true);
			henImage.gameObject.SetActive (false);
		}
	}

	public void ChangeToType(Type t) {
		curType = t;
		if (curType == Type.Cow) {
			cowImage.gameObject.SetActive (true);
			henImage.gameObject.SetActive (false);
		} else {
			cowImage.gameObject.SetActive (false);
			henImage.gameObject.SetActive (true);
		}
	}

	public void ActivateLineItem() {
		GameController.manager.ActivateIndex (curItemIndex, linedItemIndexs [0]);
	}

	public void SelectSelf() {
		isSelected = true;
		selectImage.gameObject.SetActive (true);
	}

	public void DeselectSelf() {
		isSelected = false;
		selectImage.gameObject.SetActive (false);
	}

	public void DeactivateSelf() {
		isActive = false;
		flagImage.gameObject.SetActive (false);
	}

	public void ActivateSelf() {
		isActive = true;
		flagImage.gameObject.SetActive (true);
	}

		
}
