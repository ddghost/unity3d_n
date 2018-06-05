using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMGUI : MonoBehaviour {

	private Rect bloodBar;
	private float barUpLength = 70f;

	void Start()
	{
		bloodBar = new Rect (0, 0, 60, 20);
	}

	void OnGUI()
	{
		
		Vector2 player2DPosition = Camera.main.WorldToScreenPoint (transform.position);
		player2DPosition.y = Screen.height - player2DPosition.y - barUpLength;
		bloodBar.center = player2DPosition + new Vector2(0 , 0);
		if ( player2DPosition.x > Screen.width || player2DPosition.y > Screen.height
		    || player2DPosition.x < 0 || player2DPosition.y < 0) {

		} else {
			GUI.HorizontalScrollbar(bloodBar, 0.0f, 1.0f, 0.0f, 1.0f);  
		}
	}


}
