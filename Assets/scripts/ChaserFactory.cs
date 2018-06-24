using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChaserFactory: NetworkBehaviour{

	public ChaserInfo getChaser(){
		return new ChaserInfo ();
	}
}
