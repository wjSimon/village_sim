using UnityEngine;
using System.Collections;

[System.Serializable]
public class Task {

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

	public string Info() { return startTime + " " + action + " " + target; }
}
