using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager,ISSActionCallback {

	public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
		int intParam = 0, string strParam = null, Object objectParam = null){

	}
		
	protected void Update()
	{
		if (Director.getInstance().CurrentSceneController.getGameStatus() != "running") {
			actions.Clear ();
		}

		foreach (SSAction ac in waitingAdd) actions[ac.GetInstanceID()] = ac;
		waitingAdd.Clear();

		foreach (KeyValuePair<int, SSAction> kv in actions)
		{
			SSAction ac = kv.Value;
			if (ac.gameobject.activeSelf == false || ac.destroy)//gameobject的active是false就不更新action了
			{
				waitingDelete.Add(ac.GetInstanceID());
			}
			else if ( ac.enable)
			{
				ac.Update();
			}
		}
		foreach (int key in waitingDelete)
		{
			SSAction ac = actions[key]; actions.Remove(key); DestroyObject(ac);
		}
		waitingDelete.Clear();
	}
}
