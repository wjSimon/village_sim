using UnityEngine;
using System.Collections;

public class Building : InfoObject
{

	//Buildings hold villagers during certain actions / are necessary for certain actions
	//

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void DrawInfoGUI()
	{
		base.DrawInfoGUI();
		GUI.Label(new Rect(10, 10, 200, 20), "datenBuilding");
	}
}
