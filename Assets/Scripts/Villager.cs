using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Villager : InfoObject
{
	//villager's are the representation of the schedule/npc framework
	//every villager has an actions he's currently performing as well as a schedule he's currently working on completing
	//villagers can have job/moods/other meta assigned, these can be modified from the outside or completed actions and events throughout his day
	//based on that is existing schedule or future schedules can be modified by he schedule manager

	// Use this for initialization


	public Schedule schedule;
	/*
	RaycastHit info = new RaycastHit();

			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out info))
			{
	/**/
	void Start () {
		schedule.Reset();
    }

	void Update () {
		schedule.GetNextTask();
	}

	public override void DrawInfoGUI()
	{
		base.DrawInfoGUI();
		int y = 10;
		GUI.Label(new Rect(10, y, 200, 20), objectName);y += 20;
		Task current = schedule.GetCurrent();

		if (current != null)
		{
			GUI.Label(new Rect(10, y, 200, 20), current.Info());y += 20;
		}
	}
}
