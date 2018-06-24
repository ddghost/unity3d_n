using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerController{
	GameObject player;
	Vector3 defaultLoc = Vector3.zero;
	public playerMono playerScript;

	public PlayerController(GameObject player , int id){
		this.player = player;
		playerScript = player.GetComponent<playerMono> ();
		player.GetComponent<Animator>().SetBool ("ifStop", false);
		player.name = "player" + id.ToString() ;
		defaultLoc = player.transform.position;
	}



	public GameObject getPlayer(){
		return player;
	}


	public void reset(){
		
		playerScript.RpcResetPosition (defaultLoc);
		playerScript.RpcSetAnimatorBool ("ifStop", false);

	}
}
