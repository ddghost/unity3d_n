using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class SSActionManager : NetworkBehaviour {
	protected Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
	protected List<SSAction> waitingAdd = new List<SSAction>();
	protected List<int> waitingDelete = new List<int>();

	void Start()
	{
		
	}

	public SSActionManager(){
		
	}

	public void RunAction(GameObject gameobject, SSAction action, ISSActionCallback manager)
	{
		action.gameobject = gameobject;
		action.transform = gameobject.transform;
		action.callback = manager;
		waitingAdd.Add(action);
		action.Start();
	}


}