using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class firstSceneController :  NetworkBehaviour,  sceneController {
	Vector3[] checkerLoc = 
					{new Vector3(10 , 0 , -10 ) ,new Vector3(10 , 0 , 0 ) , new Vector3(10 , 0 , 10) ,
					new Vector3(0 , 0 , -10 )  ,new Vector3(0 , 0 , 10 ) ,
					new Vector3(-10 , 0 , -10 ) ,new Vector3(-10 , 0 , 0 ) ,new Vector3(-10 , 0 , 10 ) ,};
	CheckerController[] checkerCtrl;
	List<PlayerController> playerCtrlList;
	string gameStatus = "running";
	ScoreController scoreCtrl;
	// Use this for initialization

	void Start () {
		init ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (scoreCtrl.getScore () == checkerLoc.Length) {
			gameStatus = "gameover";
		}
	}

	void OnEnable(){
		EventManager.gameover += setGameOver;
	}

	void OnDisable(){
		EventManager.gameover -= setGameOver;
	}

	void setGameOver(){
		for(int loop = 0 ; loop < playerCtrlList.Count ; loop++){
			Debug.Log (loop.ToString () + playerCtrlList [loop].getPlayer ().GetComponent<Animator> ().GetBool ("ifStop").ToString() );
			if (!playerCtrlList [loop].playerScript.isDead() ) {
				
				return;
			}
		}
		gameStatus = "gameover";
	}

	void init(){
		this.gameObject.AddComponent<ChaserFactory>();
		this.gameObject.AddComponent<CCActionManager>();
		this.gameObject.AddComponent<EventManager>();
		this.gameObject.AddComponent<UserGui>();
		scoreCtrl = this.gameObject.AddComponent<ScoreController>();
		Director director = Director.getInstance ();
		director.CurrentSceneController = this;
		loadResources ();
	}

	public void addPlayer(GameObject player){
		playerCtrlList.Add (new PlayerController(player, playerCtrlList.Count) );

	}

	public void loadResources(){
		var map = (GameObject)Instantiate (Resources.Load ("Prefabs/map"), Vector3.zero, Quaternion.identity);
		NetworkServer.Spawn (map);
		checkerCtrl = new CheckerController[checkerLoc.Length];
		playerCtrlList = new List<PlayerController> ();
		for (int loop = 0 ; loop < checkerLoc.Length ; loop++) {
			GameObject checker = (GameObject)Instantiate (Resources.Load ("Prefabs/checker")
				, checkerLoc[loop] , Quaternion.identity) as GameObject;
			NetworkServer.Spawn(checker);
			checker.name = "checker" + loop.ToString ();
			checkerCtrl[loop] = checker.AddComponent(typeof (CheckerController)) as CheckerController;
		}
	}

	public void reset(){
		gameStatus = "running";
		for (int loop0 = 0; loop0 < playerCtrlList.Count; loop0++) {
			if (playerCtrlList [loop0] != null) {
				playerCtrlList [loop0].reset ();
			}

		}

		for (int loop1 = 0 ; loop1 < checkerLoc.Length ; loop1++) {
			checkerCtrl [loop1].reset ();
			playerCtrlList [0].playerScript.RpcSetScore (checkerCtrl [loop1].getScore() , true );
		}	

		scoreCtrl.reset ();
	}

	public string getGameStatus(){
		return gameStatus;
	}


	public int getScore(){
		return scoreCtrl.getScore ();
	}
}
