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

	void Awake() {
		VillageManager.Get().AddBuilding(this);
	}
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

	public void AddWorker() { if (currentProduction == ProductionEnum.NONE) { return; } workers++; Debug.Log("worker added"); }
	public uint GetWorkers() { return workers; }
	public void RemoveWorker() { if(workers == 0) { return; } workers--; Debug.Log("worker removed"); }
}
