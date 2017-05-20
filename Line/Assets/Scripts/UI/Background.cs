using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour {

	public InputField countInput;

	public void confirmButtonOnClick() {
		if (countInput.text != "") {
			GameController.manager.backCount = int.Parse (countInput.text);
			GameController.manager.SimulateBack ();
		}
	}
}
