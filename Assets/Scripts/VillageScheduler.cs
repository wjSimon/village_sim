using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class VillageScheduler
{

	private VillageManager main; //the Village to be scheduled

	private List<Villager> sorted;

	public VillageScheduler()
	{ }

	public void Init(VillageManager reference)
	{
		main = reference;
		sorted = main.villagers.OrderByDescending(o => o.happiness).ToList();

		Schedule();
	}

	public void Update()
	{
		if (main == null) { return; } //can only update when initiated
	}

	private void Schedule()
	{
		sorted = main.villagers.OrderByDescending(o => o.happiness).ToList();
		Schedule[] schedules = new Schedule[sorted.Count];

		if (main.GetSeason() != VillageManager.Season.Winter)
		{
			//what needs to be produced
			int breadneeded = sorted.Count; //every villager needs 1 bread a day
			int flourneeded = (breadneeded / 4) + 1; // 1 flour = 4 breads
			int wheatneeded = flourneeded * 3; // 3 wheat for 1 four needed

			int breaddiff = breadneeded - main.bread;
			int flourdiff = flourneeded - main.flour;
			int wheatdiff = wheatneeded - main.wheat;

			float timebread = (breaddiff / 4) * VillageManager.oneHour;
			float timeflour = flourdiff * VillageManager.oneHour;
			float timewheat = (wheatdiff / 3) * VillageManager.oneHour;

			float time = sorted.Count * VillageManager.oneDay; //time available to all villagers

			float timeasleep = sorted.Count * (VillageManager.oneHour * 8);  //sleep
			time -= timeasleep;
            time -= timebread;
			time -= timeflour;
			time -= timewheat / 3;

			if (time > 0)
			{
				float duration = timebread + timeflour + timewheat + timeasleep;
				//enough time, proceed
				int villagersneeded = (int)duration / VillageManager.oneDay;
				List<Task> tasks = new List<Task>();

				for (int i = 0; i < villagersneeded; i++)
				{
					Villager v = sorted[i];
					Schedule s = v.schedule = new Schedule();

					s.tasks.Clear();
				}
			}
			else
			{
				//im not sure tbh
			}
        }
	}

	private string ParseStartTime(int hour, int minute)
	{
		return "";
	}
}
