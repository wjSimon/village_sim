using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Schedule {

	//a schedule
	//every villager has a schedule built by the villageManager, every schedule has a duration
	//schedules assign actions to villagers based on time/conditions
	//new schedules are created in regular intervals (24h?)

	public List<Task> tasks = new List<Task>();
	public int currentTask = -1;

	public void Reset()
	{
		currentTask = -1;
	}
	public bool GetNextTask()
	{
		double t = VillageManager.Get().GetDayTime();
		Task cur = GetCurrent();
		int currentTask_tmp = currentTask;
        if (cur != null)
		{
			//0 uhr umsprung
			if (t < cur.ParseTime()) { currentTask_tmp = -1; }
		}

		int nextTask = currentTask_tmp + 1;
		if (nextTask < 0 || nextTask >= tasks.Count) { return false; }

		Task next = tasks[nextTask];
		if(t>=next.ParseTime())
		{
			//Debug.Log(next.Info() + " - t:" + t + " parse:" + next.ParseTime());
			currentTask = nextTask;
			return true;
        }

		return false;
	}

	public Building GetTarget()
	{
		Task tmp = tasks[currentTask];
		return tmp.ParseTarget();
	}

	public Task GetCurrent()
	{
		if(currentTask < 0 || currentTask >= tasks.Count) { return null; }
		return tasks[currentTask];
	}
}
