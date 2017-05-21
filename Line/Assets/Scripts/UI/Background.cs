using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour {

	public InputField lineCountInput;
	public InputField backCountInput;

	public void confirmButtonOnClick() {
		if (backCountInput.text != "" && lineCountInput.text != "") {
			GameController.manager.backCount = int.Parse (backCountInput.text);
			GameController.manager.lineCount = int.Parse (lineCountInput.text);
			GameController.manager.SimulateBack ();
		}
	}
}
