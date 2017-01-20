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
	private Vector3 target; //<- swap with A* Path
	private Building workplace;
	private float speed = 0.008f; // <- ~60 mins through whole village
	private bool arrived = false;

	/*
	RaycastHit info = new RaycastHit();

			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out info))
			{
	/**/

	public float happiness = 100; //100 = max
	private float happinessFactor = 1; //so I can make bipolar villagers; NOT IN USE ANYMORE

	void Awake()
	{
		target = transform.position;
	}
	void Start()
	{
		VillageManager.Get().AddVillager(this);
		schedule.Reset();
	}

	void Update()
	{
		if (schedule.GetNextTask())
		{
			Building b = schedule.GetTarget();
			if (b != null)
			{
				target = b.transform.position;
				if (workplace != null && arrived)
				{
					workplace.RemoveWorker(this);
				}
				workplace = b;
				arrived = false;
			}

			if (b == null && arrived)
			{
				workplace.RemoveWorker(this);
			}
		}

		Movement();

	}

	void Movement()
	{
		Vector3 tmp = target;
		if (schedule.GetTarget() == null) { return; }
		//Debug.Log(tmp);
		if (Vector3.Distance(transform.position, tmp) != 0)
		{
			target = new Vector3(target.x, 1, target.z);
			transform.position = Vector3.MoveTowards(transform.position, target, speed * VillageManager.Get().deltaTime);

			if (transform.position == target && !arrived)
			{
				//arrived at building
				Debug.Log("arrived");
				arrived = true;
				workplace.AddWorker(this);
			}
		}
	}

	public float ChangeHappiness(float value)
	{
		happiness += value * happinessFactor;
		if(happiness > 100) { happiness = 100; }

		if(happiness <= 0) { Kill(); } //this actually cannot happen right now unless u force it by setting it by hand

		return happiness;
	}

	public void SetHappiness(int value)
	{
		happiness = value > 100 ? 100 : value;

		if(happiness <= 0) { Kill(); }
	}
	public float GetHappiness()
	{
		return happiness;
	}

	public void Kill()
	{
		if(workplace != null) workplace.RemoveWorker(this);
		Destroy(gameObject);
	}

	public override void DrawInfoGUI()
	{
		base.DrawInfoGUI();
		int y = 10;
		GUI.Label(new Rect(10, y, 200, 20), objectName); y += 20;
		Task current = schedule.GetCurrent();

		if (current != null)
		{
			GUI.Label(new Rect(10, y, 200, 20), current.Info()); y += 20; y += 20;
		}

		for (int i = 0; i < schedule.tasks.Count; i++)
		{
			GUI.Label(new Rect(10, y, 200, 20), schedule.tasks[i].Info()); y += 20;
		}

		GUI.Label(new Rect(10, y, 200, 20), "Happiness: " + happiness.ToString()); y += 20;
	}
}
