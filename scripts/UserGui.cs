using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGui : MonoBehaviour {
	ParticleSystem particleSystem;
	ParticleSystem.ForceOverLifetimeModule forceMode;
	// Use this for initialization
	void Start () {
		particleSystem = GetComponent<ParticleSystem>();
		forceMode = particleSystem.forceOverLifetime;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
		if( GUI.Button(new Rect(10 , 30 , 50 , 30) ,  "left") ){
			ParticleSystem.MinMaxCurve temp = forceMode.x ;
			temp.constantMax = temp.constantMax - 0.5f;
			forceMode.x = temp;
		}
			
		if( GUI.Button(new Rect(10 , 70 , 50 , 30) ,  "right") ){
			ParticleSystem.MinMaxCurve temp = forceMode.x ;
			temp.constantMax = temp.constantMax + 0.5f;
			forceMode.x = temp;
		}
			
		if( GUI.Button(new Rect(10 , 110 , 50 , 30) ,  "big") ){
			particleSystem.startSize =  particleSystem.startSize * 1.11f;
			particleSystem.startLifetime = particleSystem.startLifetime * 1.11f;
		}

		if( GUI.Button(new Rect(10 , 150 , 50 , 30) ,  "small") ){
			particleSystem.startSize =  particleSystem.startSize * 0.9f;
			particleSystem.startLifetime = particleSystem.startLifetime * 0.9f;
		}
	}

}
