using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class CheckerController: NetworkBehaviour{
	ChaserInfo chaser;
	GameObject score;
	Vector3 defaultLoc = Vector3.zero;
	void Awake(){
		ChaserFactory chaserFactory = Singleton<ChaserFactory>.Instance;
		chaser = chaserFactory.getChaser();
		chaser.setParent (this.gameObject, defaultLoc);
		chaser.setCheckerWorldLoc (this.transform.position);
		score = Object.Instantiate (Resources.Load ("Prefabs/score"), Vector3.zero, Quaternion.identity) as GameObject;
		NetworkServer.Spawn (score);
		score.transform.parent = this.gameObject.transform;
		score.transform.localPosition = Vector3.zero;
		score.name = "score" + this.gameObject.name.Substring (7);
	}

	public void reset(){
		chaser.reset ();
		chaser.setParent (this.gameObject, defaultLoc);
		//score.SetActive (true);
	}



	public GameObject getScore(){
		return score;
	}

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.name.Contains("player"))
			chaser.runner = collider.gameObject;
	}

	void OnTriggerExit(Collider collider){
		if(collider.gameObject.name.Contains( "player") )
			chaser.runner = null;
	}
}
