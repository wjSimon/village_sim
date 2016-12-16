using UnityEngine;
using System.Collections;


public enum ProductionEnum
{
	IRON,
	PICKAXE,
	WHEAT,
	FLOUR,
	BREAD,
	NONE,
}
public class Building : InfoObject
{
	//Buildings hold villagers during certain actions / are necessary for certain actions
	//

	// Use this for initialization
	public ProductionEnum currentProduction;
	public uint workers = 0;

	private double productionTimer = 0;

	void Awake() {
		VillageManager.Get().AddBuilding(this);
	}
	void Start () {
		workers = 0;
		productionTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Production();
	}

	void Production()
	{
		if (workers > 0)
		{
			productionTimer += VillageManager.Get().deltaTime;

			switch (currentProduction)
			{
				case ProductionEnum.NONE:
					break;
				case ProductionEnum.IRON:
					if (productionTimer > VillageManager.oneHour)
					{
						if (VillageManager.Get().pickaxe >= 1)
						{
							VillageManager.Get().pickaxe -= 1;
							VillageManager.Get().iron += 2;
							productionTimer = 0;
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
						}
					}

					break;
				case ProductionEnum.BREAD:
					break;
				case ProductionEnum.FLOUR:
					break;
				case ProductionEnum.WHEAT:
					break;
			}
		}
	}

	public override void DrawInfoGUI()
	{
		base.DrawInfoGUI();
		GUI.Label(new Rect(10, 10, 200, 20), "datenBuilding");
	}

	public void AddWorker() { if (currentProduction == ProductionEnum.NONE) { return; } workers += 1; Debug.Log("worker added"); }
	public uint GetWorkers() { return workers; }
	public void RemoveWorker() { if(workers == 0) { return; } workers -= 1; Debug.Log("worker removed"); }
}
