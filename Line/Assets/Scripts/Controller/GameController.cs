using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public ChessItem chessPrefab;

	public static GameController manager = null;

	public Transform boardTransform;

	//TODO
	List<ChessItem> chessList = new List<ChessItem> (16);

	public int Level = 4;

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
				while (chessList [random].lined) {
//					while (random == i) {
						random = Random.Range (0, Level * Level);
//					}
				}
				chessList [i].lined = true;
				chessList [random].lined = true;
				chessList [i].linedItemIndexs.Add (chessList [random].curItemIndex);
				chessList [random].linedItemIndexs.Add (chessList [i].curItemIndex);
			}
		}
	}

	public void ActivateIndex(int index1, int index2) {
		for (int i = 0; i < chessList.Count; i++) {
			if (chessList [i].curItemIndex == index1 || chessList [i].curItemIndex == index2) {
				chessList [i].ActivateSelf ();
			} else {
				chessList [i].DeactivateSelf ();
			}
		}
	}
}
