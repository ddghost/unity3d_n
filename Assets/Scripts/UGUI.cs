using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UGUI : MonoBehaviour {
	private float barUpLength = 3f;
	public Slider healthSlider ;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + barUpLength , transform.position.z);

		Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
	
		healthSlider.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
	}
}
