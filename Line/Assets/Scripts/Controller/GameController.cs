using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DragDirection {
	Up,
	Down,
	Left,
	Right
}

public class GameController : MonoBehaviour {

	public ChessItem chessPrefab;

	public static GameController manager = null;

	public Transform boardTransform;

	public bool hasSelectedItem = false;

	//TODO
	List<ChessItem> chessList = new List<ChessItem> (16);

	public int Level = 4;

	ChessItem selectItem = null;

	ChessItem nearItem = null;

	public int count = 0;

	public bool gameStart = false;

	void Awake() {
		if (manager == null) {
			manager = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	void Start() {
		InitChessBoard ();
		RandomLine ();

	}

	void InitChessBoard() {
		for (int i = 0; i < Level * Level; i++) {
			ChessItem item = Instantiate (chessPrefab);
			item.transform.SetParent (boardTransform, true);
			item.curItemIndex = i;
			chessList.Add (item);
		}
	}

	void RandomLine() {
		for (int i = 0; i < chessList.Count; i++) {
			if (!chessList [i].lined) {
				int random = Random.Range (0, Level * Level);
				while (chessList [random].lined || random == i) {
						random = Random.Range (0, Level * Level);
				}
				chessList [i].lined = true;
				chessList [random].lined = true;
				chessList [i].linedItemIndexs.Add (chessList [random].curItemIndex);
				chessList [random].linedItemIndexs.Add (chessList [i].curItemIndex);
			}
		}
	}

	void SimulateBack() {
		int i = 0;
		while (i < count) {

			i++;
		}
		gameStart = true;
	}

	public void ActivateIndex(int index1, int index2) {
		hasSelectedItem = true;
		for (int i = 0; i < chessList.Count; i++) {
			if (chessList [i].curItemIndex == index1) {
				chessList [i].SelectSelf ();
				chessList [i].ActivateSelf ();
				selectItem = chessList [i];
			}
			else if (chessList [i].curItemIndex == index2) {
				chessList [i].DeselectSelf ();
				chessList [i].ActivateSelf ();
			} else {
				chessList [i].DeselectSelf ();
				chessList [i].DeactivateSelf ();
			}
		}
	}

	public void ConvertIndex(int index1, int index2) {
		for (int i = 0; i < chessList.Count; i++) {
			if (chessList [i].curItemIndex == index1 || chessList [i].curItemIndex == index2) {
				chessList [i].Convert ();
			}
		}
	}


	void Update() {
		if (!hasSelectedItem)
			return;
		if (!gameStart)
			return;
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			if (CheckAvailable (DragDirection.Up)) {
				nearItem = GetNearItem (DragDirection.Up);
				if (nearItem != null) {
					Type temp = nearItem.curType;
					nearItem.ChangeToType (selectItem.curType);
					selectItem.ChangeToType (temp);
				}
			}
		} else if(Input.GetKeyDown(KeyCode.DownArrow)){
			if (CheckAvailable (DragDirection.Down)) {
				nearItem = GetNearItem (DragDirection.Down);
				if (nearItem != null) {
					Type temp = nearItem.curType;
					nearItem.ChangeToType (selectItem.curType);
					selectItem.ChangeToType (temp);
				}
			}
		} else if(Input.GetKeyDown(KeyCode.LeftArrow)){
			if (CheckAvailable (DragDirection.Left)) {
				nearItem = GetNearItem (DragDirection.Left);
				if (nearItem != null) {
					Type temp = nearItem.curType;
					nearItem.ChangeToType (selectItem.curType);
					selectItem.ChangeToType (temp);
				}
			}
		} else if(Input.GetKeyDown(KeyCode.RightArrow)){
			if (CheckAvailable (DragDirection.Right)) {
				nearItem = GetNearItem (DragDirection.Right);
				if (nearItem != null) {
					Type temp = nearItem.curType;
					nearItem.ChangeToType (selectItem.curType);
					selectItem.ChangeToType (temp);
				}
			}
		} 
	}

	ChessItem GetNearItem(DragDirection dir) {
		switch(dir) {
		case DragDirection.Up:
			return chessList[selectItem.curItemIndex - Level];
			break;
		case DragDirection.Down:
			return chessList[selectItem.curItemIndex + Level];
			break;
		case DragDirection.Left:
			return chessList[selectItem.curItemIndex - 1];
			break;
		case DragDirection.Right:
			return chessList[selectItem.curItemIndex + 1];
			break;
		default:
			break;
		}
		return null;
	}

	bool CheckAvailable(DragDirection dir) {
		int row = selectItem.curItemIndex / 4;
		int col = selectItem.curItemIndex % 4;
		switch(dir) {
		case DragDirection.Up:
			if (row == 0)
				return false;
				break;
		case DragDirection.Down:
			if (row == 3)
				return false;
				break;
		case DragDirection.Left:
			if (col == 0)
				return false;
				break;
		case DragDirection.Right:
			if (col == 3)
				return false;
				break;
			default:
				break;
		}
		return true;
	}
}
