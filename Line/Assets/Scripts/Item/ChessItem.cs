using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChessItem : MonoBehaviour, IPointerClickHandler {

	//if it is lined to other items
	public bool lined = false;
	public int curItemIndex = 0;
	public List<int> linedItemIndexs = new List<int> (4);
	public Image cowImage;
	public Image henImage;
	public Image flagImage;

	void Start() {
	
	}

	void Update() {
	
	}

	public void OnPointerClick(PointerEventData data) {
		print ("Length : " + linedItemIndexs.Count);
		ActivateLineItem ();
	}

	public void ActivateLineItem() {
		GameController.manager.ActivateIndex (curItemIndex, linedItemIndexs [0]);
	}

	public void DeactivateSelf() {
		flagImage.gameObject.SetActive (false);
	}

	public void ActivateSelf() {
		flagImage.gameObject.SetActive (true);
	}

}
