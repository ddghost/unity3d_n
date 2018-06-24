using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ScoreController : NetworkBehaviour {
	int score = 0;
	ArrayList arr = new ArrayList ();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void reset(){
		score = 0;
		arr.Clear ();
	}

	void OnEnable(){
		EventManager.scoreChange += addScore;
	}

	void OnDisable(){
		EventManager.scoreChange -= addScore;
	}

	void addScore(string name){
		if (!arr.Contains (name)) {
			score++;
			arr.Add (name);
		}
	}

	public int getScore(){
		return score;
	}
}
