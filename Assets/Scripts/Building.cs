using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum ProductionEnum
{
	IRON,
	PICKAXE,
	WHEAT,
	FLOUR,
	BREAD,
	HAPPINESS,
	NONE,
}
public class Building : InfoObject
{
	//Buildings hold villagers during certain actions / are necessary for certain actions
	//

	// Use this for initialization
	public ProductionEnum currentProduction;
	public List<Villager> workers = new List<Villager>();

	private double productionTimer = 0;
	public float happinessGain = -5;

	void Awake()
	{
		VillageManager.Get().AddBuilding(this);
	}
	void Start()
	{
		workers.Clear();
		productionTimer = 0;

		//Set all happiness gains by productiontype here; <- EVERYTHING is productiontype so u can make like a tavern that produces nothing/consumes stuff but increases happiness
	}

	// Update is called once per frame
	void Update()
	{
		Production();
	}

	void Production()
	{ 
		if(GetComponent<SeasonFilter>() != null && GetComponent<SeasonFilter>().filter.Contains(VillageManager.Get().GetSeason())) { return; } //if this building can't produce during current season
		if (workers.Count > 0)
		{
			productionTimer += VillageManager.Get().deltaTime;

			switch (currentProduction)
			{
				case ProductionEnum.NONE:
					break;
				case ProductionEnum.HAPPINESS:
					if (productionTimer > VillageManager.oneHour)
					{
						productionTimer = 0;
						TickCycle();
					}
					break;

				case ProductionEnum.IRON:
					if (productionTimer > VillageManager.oneHour)
					{
						if (VillageManager.Get().pickaxe >= 1)
						{
							VillageManager.Get().pickaxe -= 1;
							VillageManager.Get().iron += 2;
							productionTimer = 0;

							TickCycle(); //only ticks if villager is actually working, since he's chilling otherwise having a "jolly good" time*
						}
					}
					break;

				case ProductionEnum.PICKAXE:
					if (productionTimer > VillageManager.oneHour)
					{
						if (VillageManager.Get().iron >= 1)
						{
							VillageManager.Get().pickaxe += 1;
							VillageManager.Get().iron -= 1;
							productionTimer = 0;

							TickCycle();
						}
					}

					break;
				case ProductionEnum.BREAD:
					if (productionTimer > VillageManager.oneHour)
					{
						if (VillageManager.Get().flour >= 1)
						{
							VillageManager.Get().bread += 4;
							VillageManager.Get().flour -= 1;
							productionTimer = 0;

							TickCycle();
						}
					}
					break;
				case ProductionEnum.FLOUR:
					if (productionTimer > VillageManager.oneHour)
					{
						if (VillageManager.Get().wheat >= 3)
						{
							VillageManager.Get().flour += 1;
							VillageManager.Get().wheat -= 3;
							productionTimer = 0;

							TickCycle();
						}
					}
					break;
				case ProductionEnum.WHEAT:
					if (productionTimer > VillageManager.oneHour)
					{
						VillageManager.Get().wheat += 1;
						productionTimer = 0;

						TickCycle();
					}
					break;
			}
		}
	}

	private void TickCycle()
	{
		Debug.Log("Ticking: " + workers.Count + " worker(s) at [" + objectName.ToString() + "]");
		for (int i = 0; i < workers.Count; i++)
		{
			float h = workers[i].ChangeHappiness(happinessGain);
			if (h <= 25) { RemoveWorker(workers[i]); } //doesn't work anymore if worker is really ｓａｄｂｏｙｓ, but doesn't stop schedule so theres punishment for overworking villagers
		}
	}

	public override void DrawInfoGUI()
	{
		base.DrawInfoGUI();
		GUI.Label(new Rect(10, 10, 200, 20), "datenBuilding");
	}

	public void AddWorker(Villager worker) { if (currentProduction == ProductionEnum.NONE) { return; } workers.Add(worker); Debug.Log("worker added"); }
	public int GetWorkerCount() { return workers.Count; }
	public void RemoveWorker(Villager worker) { if (workers.Count == 0) { return; } workers.Remove(worker); Debug.Log("worker removed"); }
}














//* it's really annoying to check whether something was produced when cycle runs out so i dont wanna do it