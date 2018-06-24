using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class playerMono : NetworkBehaviour {
	

	private float mosveSpeed = 10f;
	private int nowDirection = 0;
	private bool ifRun = false;

	[SyncVar]
	public bool status = true; // false mean dead

	// Use this for initialization
	void Start () {
		if(isLocalPlayer)
			CmdAddPlayerToScene();
		status = true;

	}

	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
			return;
		moveControl ();
	}

	[ClientRpc]
	public void RpcResetPosition(Vector3 position){
		
		gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		this.transform.rotation = Quaternion.identity;
		this.transform.position = position;
		ifRun = false;
		nowDirection = 0;
		status = true;
	}

	[ClientRpc]
	public void RpcSetAnimatorBool(string name , bool boolValue){
		if (isLocalPlayer)
			this.GetComponent<Animator> ().SetBool (name, boolValue);
	}

	[ClientRpc]
	public void RpcSetScore(GameObject score , bool boolValue){
		score.SetActive (boolValue);
	}


	[Command]
	void CmdAddPlayerToScene(){
		Director.getInstance ().CurrentSceneController.addPlayer (gameObject);
	}
		
	void moveControl(){
		if ( !isDead ()) { 
			nowDirection = getDirection ();
			if (ifRun) {
				
				this.transform.eulerAngles = new Vector3 (0, nowDirection, 0);

				this.transform.Translate (0, 0, mosveSpeed * Time.deltaTime);
			}
		}
	}

	int getDirection(){
		int direction = 0;
		if (Input.GetKey(KeyCode.W))  
		{  
			if (Input.GetKey (KeyCode.A)) {  
				direction = -45;
			}
			else if (Input.GetKey (KeyCode.D)) {
				direction = 45;
			}
			else {
				direction = 360;
			}
		}  
		else if (Input.GetKey(KeyCode.S))  
		{  
			if (Input.GetKey (KeyCode.A)) {  
				direction = -135;
			}
			else if (Input.GetKey (KeyCode.D)) {
				direction = 135;
			}
			else {
				direction = 180;
			}
		}  
		else if (Input.GetKey(KeyCode.D))  
		{  
			direction = 90;
		}  
		else if (Input.GetKey(KeyCode.A))  
		{  
			direction = -90;
		}  

		if (direction != 0) {
			ifRun = true;
			return direction;
		}
		else {
			ifRun = false;
			return nowDirection;
		}
	}

	public bool isDead(){
		return !status;
	}




	void OnCollisionEnter(Collision collision){
		/*if (!isLocalPlayer)
			return;*/
		string name = collision.gameObject.name;
		if (name.Contains("chaser")) {
			//this.gameObject.SetActive (false);
			//Destroy(gameObject.GetComponent<Rigidbody>());
			status = false;
			gameObject.GetComponent<Animator> ().SetBool ("ifStop", true);

			if(isServer)
				Singleton<EventManager>.Instance.setGameOver ();
		}
		else if (name.Contains("score")  ) {
			RpcSetScore(collision.gameObject, false);
			if(isServer)
				Singleton<EventManager>.Instance.addScore (collision.gameObject.name);
		}
	}

}
