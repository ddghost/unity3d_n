using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class EventManager : NetworkBehaviour {
	public delegate void checkGameOver();
	public static event checkGameOver gameover;
	public delegate void ScoreChange(string name);
	public static event ScoreChange scoreChange;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setGameOver(){
		if (gameover != null) {
			gameover ();
		}
	}

	public void addScore(string name){
		if (scoreChange != null) {
			scoreChange (name);
		}
	}

}
