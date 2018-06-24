using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAction : SSAction {
	Vector3 aim;
	float speed = 5f;
	ChaserInfo chaserInfo = null;

	// Use this for initialization
	public override void Start () {
		aim = this.transform.localPosition;
	}

	public static PatrolAction getAction(){
		return ScriptableObject.CreateInstance<PatrolAction>();
	}
	
	// Update is called once per frame
	public override void Update () {
		if (chaserInfo != null) {


			if (chaserInfo.runner != null && !chaserInfo.runner.GetComponent<playerMono> ().isDead ()) {
				aim = chaserInfo.runner.transform.position - chaserInfo.getCheckerWorldLoc ();
			} else {
				chaserInfo.runner = null;			
			}

			gameobject.transform.localPosition = Vector3.MoveTowards (gameobject.transform.localPosition 
				, aim, speed * Time.deltaTime);
			gameobject.transform.LookAt (chaserInfo.getCheckerWorldLoc() + aim);
			if (aim == gameobject.transform.localPosition) {
				chaserInfo.setNewAim ();
			} 
		}

	}
		
	public void setAim(Vector3 newAim){
		aim = newAim;
	}

	public void setChaserInfo(ChaserInfo info){
		chaserInfo = info;
	}
}
