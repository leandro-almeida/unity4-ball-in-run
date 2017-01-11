using UnityEngine;
using System.Collections;

public class RankingController : MonoBehaviour {

	public TextMesh scoreDisplay;

	// Use this for initialization
	void Start () {
		scoreDisplay.text = "";

		int[] tempRanking = ApplicationController.GetRanking ();

		for (int i = ApplicationController.numberPositionRanking-1; i >= 0; i--) {
			scoreDisplay.text += tempRanking[i].ToString("D4") + "\n";
		}
	}
	
	// Update is called once per frame
	void Update () {
		BasicInputs ();
	}

	void BasicInputs() {
		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel("GamePlay");
		}
	}
}
