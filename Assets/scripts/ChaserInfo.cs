using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ChaserInfo{
	public GameObject runner;
	private GameObject chaser;
	private PatrolAction patrolAc;
	private int nowAim;
	Vector3 checkerWorldLoc ;

	Vector3[] patrolLoc = { new Vector3 (4, 0, 0) , new Vector3 (0, 0, 4) 
								, new Vector3 (-4, 0, 0) , new Vector3 (0 , 0, -4) };

	// Use this for initialization
	public ChaserInfo(){
		chaser = (GameObject)Object.Instantiate (Resources.Load ("Prefabs/chaser") 
			, Vector3.zero , Quaternion.identity) ;
		NetworkServer.Spawn(chaser);

		nowAim = 0;
		chaser.name = "chaser";
	}

	public void setParent(GameObject parent , Vector3 loc){
		chaser.transform.parent = parent.transform;
		chaser.transform.localPosition = loc;
	}

	public void setNewAim(){
		nowAim = (nowAim + 1) % patrolLoc.Length;
		patrolAc.setAim(patrolLoc[nowAim] );
	}

	public void setCheckerWorldLoc(Vector3 loc){
		checkerWorldLoc = loc;
		setAction ();
	}

	void setAction(){
		patrolAc = PatrolAction.getAction (); 
		patrolAc.setChaserInfo (this);
		Singleton<CCActionManager>.Instance.RunAction(chaser , patrolAc , null );
	}

	public Vector3 getCheckerWorldLoc(){
		return checkerWorldLoc;
	}

	public void reset(){
		nowAim = 0;
		runner = null;
		setAction ();
		Singleton<CCActionManager>.Instance.RunAction(chaser , patrolAc , null );
	}
}
