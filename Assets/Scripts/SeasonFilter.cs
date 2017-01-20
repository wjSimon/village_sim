using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Building))]
public class SeasonFilter : MonoBehaviour
{

	public List<VillageManager.Season> filter = new List<VillageManager.Season>();
	// Use this for initialization
	void Start()
	{
		CutList();
	}

	private void CutList()
	{
		//List for serializability, list gets cut runtime on start-up to 4 entries max, one for each season; means i dont have to write custom editor
		bool[] tracker = new bool[4];

		if (filter.Contains(VillageManager.Season.Winter)) { tracker[0] = true; }
		if (filter.Contains(VillageManager.Season.Spring)) { tracker[1] = true; }
		if (filter.Contains(VillageManager.Season.Summer)) { tracker[2] = true; }
		if (filter.Contains(VillageManager.Season.Fall)) { tracker[3] = true; }

		filter.Clear();

		for (int i = 0; i < 4; i++)
		{
			if (tracker[i]) { filter.Add((VillageManager.Season)i); }
		}
	}
	public void AddFilter()
	{

	}
}
