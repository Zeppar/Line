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

	public int backCount = 0;

	public int lineCount = 2;

	public bool gameStart = false;

	public int patternCount = 2;

	void Awake() {
		if (manager == null) {
			manager = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	void Start() {
		//start game after clicking start button 
	}

	void InitChessBoard() {
		for (int i = 0; i < Level * Level; i++) {
			ChessItem item = Instantiate (chessPrefab);
			item.transform.SetParent (boardTransform, true);
			item.curItemIndex = i;
			item.lineCount = lineCount;
			chessList.Add (item);
		}
	}

	void RandomLine() {
		List<int> indexList = new List<int> (4);
		for (int i = 0; i < chessList.Count; i++) {
			if (!chessList [i].lined) {
				chessList [i].lined = true;
				int random = 0;
				indexList.Clear ();
				//Add self
				indexList.Add (i);
				for (int j = 0; j < lineCount - 1; j++) {
					random = Random.Range (0, Level * Level);
					while (chessList [random].lined || random == i) {
						random = Random.Range (0, Level * Level);
					}
					chessList [random].lined = true;
					indexList.Add (random);
				}


				/*chessList [i].lined = true;
				chessList [random].lined = true;
				chessList [i].linedItemIndexs.Add (chessList [random].curItemIndex);
				chessList [random].linedItemIndexs.Add (chessList [i].curItemIndex);*/

				for (int k = 0; k < indexList.Count; k++) {
//					chessList [indexList [k]].lined = true;
					for (int l = 0; l < indexList.Count; l++) {
						if (chessList [indexList [k]].curItemIndex != chessList [indexList [l]].curItemIndex) {
							//Add
							chessList [indexList [k]].linedItemIndexs.Add(chessList [indexList [l]].curItemIndex);
						}
					}
				}
			}
		}
	}

	public void SimulateBack() {
		InitChessBoard ();
		RandomLine ();
		StartCoroutine (StartSimulate ());
	}

	IEnumerator StartSimulate() {
		int i = 0;
		while (i < backCount) {
			i++;
			if (Random.Range (0, 100) > 50) {
				//点击 
				print("Click");
				int index = Random.Range(0,16);
				if (!chessList [index].isSelected) {
					chessList [index].ActivateLineItem ();
					GameController.manager.ConvertIndex (chessList [index].curItemIndex, chessList [index].linedItemIndexs);
				} else {
					GameController.manager.ConvertIndex (chessList [index].curItemIndex, chessList [index].linedItemIndexs);
				}
			} else {
				//交换
				print("Change");
				int index = Random.Range(0,16);
				if (index >= 0 && index < 4) {
					if (!chessList [index].isSelected) {
						chessList [index].ActivateLineItem ();
						ChangeItemFunc(DragDirection.Up);
					} else {
						ChangeItemFunc(DragDirection.Up);
					}
				} else if (index >= 4 && index < 8) {
					if (!chessList [index].isSelected) {
						chessList [index].ActivateLineItem ();
						ChangeItemFunc(DragDirection.Down);
					} else {
						ChangeItemFunc(DragDirection.Down);
					}
				} else if (index >= 8 && index < 12) {
					if (!chessList [index].isSelected) {
						chessList [index].ActivateLineItem ();
						ChangeItemFunc(DragDirection.Left);
					} else {
						ChangeItemFunc(DragDirection.Left);
					}
				} else if (index >= 12 && index < 16) {
					if (!chessList [index].isSelected) {
						chessList [index].ActivateLineItem ();
						ChangeItemFunc(DragDirection.Right);
					} else {
						ChangeItemFunc(DragDirection.Right);
					}
				}
			}
			yield return new WaitForSeconds (0.1f);
		}
		gameStart = true;
	}

	public void ActivateIndex(int index1, List<int> indexList) {
		hasSelectedItem = true;
		for (int i = 0; i < chessList.Count; i++) {
			if (chessList [i].curItemIndex == index1) {
				chessList [i].SelectSelf ();
				chessList [i].ActivateSelf ();
				selectItem = chessList [i];
			}
			else {
				bool find = false;
				for (int j = 0; j < indexList.Count; j++) {
					if (chessList [i].curItemIndex == indexList[j]) {
						find = true;
						chessList [i].DeselectSelf ();
						chessList [i].ActivateSelf ();
					}
				}

				if (!find) {
					chessList [i].DeselectSelf ();
					chessList [i].DeactivateSelf ();
				}
			}
		}
	}

	public void ConvertIndex(int index1, List<int> indexList) {
		for (int i = 0; i < chessList.Count; i++) {
			if (chessList [i].curItemIndex == index1) {
				chessList [i].Convert ();
			}
			for (int j = 0; j < indexList.Count; j++) {
				if (chessList [i].curItemIndex == indexList [j])
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
//			if (CheckAvailable (DragDirection.Up)) {
//				nearItem = GetNearItem (DragDirection.Up);
//				if (nearItem != null) {
//					Type temp = nearItem.curType;
//					nearItem.ChangeToType (selectItem.curType);
//					selectItem.ChangeToType (temp);
//				}
//			}
			ChangeItemFunc(DragDirection.Up);
		} else if(Input.GetKeyDown(KeyCode.DownArrow)){
//			if (CheckAvailable (DragDirection.Down)) {
//				nearItem = GetNearItem (DragDirection.Down);
//				if (nearItem != null) {
//					Type temp = nearItem.curType;
//					nearItem.ChangeToType (selectItem.curType);
//					selectItem.ChangeToType (temp);
//				}
//			}
			ChangeItemFunc(DragDirection.Down);
		} else if(Input.GetKeyDown(KeyCode.LeftArrow)){
//			if (CheckAvailable (DragDirection.Left)) {
//				nearItem = GetNearItem (DragDirection.Left);
//				if (nearItem != null) {
//					Type temp = nearItem.curType;
//					nearItem.ChangeToType (selectItem.curType);
//					selectItem.ChangeToType (temp);
//				}
//			}
			ChangeItemFunc(DragDirection.Left);
		} else if(Input.GetKeyDown(KeyCode.RightArrow)){
//			if (CheckAvailable (DragDirection.Right)) {
//				nearItem = GetNearItem (DragDirection.Right);
//				if (nearItem != null) {
//					Type temp = nearItem.curType;
//					nearItem.ChangeToType (selectItem.curType);
//					selectItem.ChangeToType (temp);
//				}
//			}
			ChangeItemFunc(DragDirection.Right);
		} 
	}

	void ChangeItemFunc(DragDirection dir) {
		if (CheckAvailable (dir)) {
			nearItem = GetNearItem (dir);
			if (nearItem != null) {
				Type temp = nearItem.curType;
				nearItem.ChangeToType (selectItem.curType);
				selectItem.ChangeToType (temp);
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
