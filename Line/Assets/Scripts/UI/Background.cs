using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Background : MonoBehaviour {

	public InputField lineCountInput;
	public InputField backCountInput;
	public InputField patternInput;

	public void confirmButtonOnClick() {
		if (backCountInput.text != "" && lineCountInput.text != "" && patternInput.text != "") {
			GameController.manager.backCount = int.Parse (backCountInput.text);
			GameController.manager.lineCount = int.Parse (lineCountInput.text);
			GameController.manager.patternCount = int.Parse (lineCountInput.text);
			GameController.manager.SimulateBack ();
		}
	}

	public void endButtonOnClick() {
		Application.Quit ();
	}

	public void restartButtonOnClick() {
		SceneManager.LoadScene (0);
	}
}
