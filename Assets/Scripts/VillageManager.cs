using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VillageManager : MonoBehaviour {

	//Tracks the village
	//info on villagers, creates schedules for villagers, executes schedules on villagers
	//tracks meta data like seasons if we want
	//

	public List<Villager> villagers = new List<Villager>();
	public List<Building> buildings = new List<Building>();

	public InfoObject selectedObject;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit info = new RaycastHit();

			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out info))
			{
				InfoObject temp = info.transform.GetComponent<InfoObject>();
				selectedObject = temp;
			}
		}

	}
	void OnGUI()
	{
		if (selectedObject != null)
		{
			selectedObject.DrawInfoGUI();
			
		}
	}
}
