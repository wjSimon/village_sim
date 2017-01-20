using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Task {

	public Task(string startTime, string action, string target)
	{
		this.startTime = startTime;
		this.action = action;
		this.target = target;
	}

	public string startTime;
	public string action;
	public string target;

	public float ParseTime()
	{
		string[] strings = startTime.Trim().Split(':');

		int h = 0;
        int.TryParse(strings[0], out h);

		int m = 0;
		if (strings.Length > 1)
		{
			int.TryParse(strings[1], out m);
		}

		return h * VillageManager.oneHour + m * VillageManager.oneMinute;
	}

	public Building ParseTarget()
	{
		if(target == "" || target == null) { return null; }
		//MAYBE add villagers later
		List<Building> builds = VillageManager.Get().GetBuildings();

		for (int i = 0; i < builds.Count; i++)
		{
			if(builds[i].objectName == target)
			{
				return builds[i];
			}
		}

		return null;
	}

	public string Info() { return startTime + " " + action + " " + target; }
}
