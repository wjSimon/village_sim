using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VillageManager : MonoBehaviour
{

	//Tracks the village
	//info on villagers, creates schedules for villagers, executes schedules on villagers
	//tracks meta data like seasons if we want
	//
	/*
	Village Simulation in Unity3D mit folgenden Features:

- Das selbst-erstellte NavMesh benutzen (für Villager Movement)
- Villager haben Daily Schedules // - auto assign schedules 
- Villager haben Arbeit, Arbeitsplätze in Form von Gebäuden < DONE
- Ressourcen-System < DONE 
- Villager anklicken für Infos // 
- Jahreszeiten, beeinflussen Schedules/Ressourcen
- Villager Relations
- Emotion/Happiness System
	/**/

	private static VillageManager instance;

	public List<Villager> villagers = new List<Villager>();
	private List<Building> buildings = new List<Building>();

	public InfoObject selectedObject;

	public double time;
	//[HideInInspector]
	public float deltaTime;
	public float timeScale;

	public const int oneMinute = 60;
	public const int oneHour = 60 * oneMinute;
	public const int oneDay = 24 * oneHour;

	private double simStartTime = 7 * oneHour;


	//Resources
	public int iron = 0;
	public int pickaxe = 0;
	public int wheat = 0;
	public int flour = 0;
	public int bread = 0;

	public static VillageManager Get()
	{
		return instance;
	}

	public VillageManager()
	{
		instance = this;
	}
	void Awake()
	{
		time += simStartTime;
	}
	void Start()
	{
		Debug.Log(buildings.Count);
	}

	// Update is called once per frame
	void Update()
	{
		TimeUpdate();

		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit info = new RaycastHit();

			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out info))
			{
				InfoObject temp = info.transform.GetComponent<InfoObject>();
				selectedObject = temp;
			}
		}
	}

	void TimeUpdate()
	{
		double timelast = time;
		time += Time.deltaTime * timeScale;
		double timenew = time;

		deltaTime = (float)(timenew - timelast);
	}

	public double[] DecomposeTime()
	{
		int itime = (int)time;
		int day = itime / oneDay;
		itime %= oneDay;
		int h = itime / oneHour;
		itime %= oneHour;
		int m = itime / oneMinute;
		int s = itime % oneMinute;

		double fraction = time - (int)time;

		return new double[4] { day, h, m, fraction + s };
	}

	public int GetDays() { return (int)DecomposeTime()[0]; }
	public int GetHours() { return (int)DecomposeTime()[1]; }
	public int GetMinutes() { return (int)DecomposeTime()[2]; }
	public double GetSeconds() { return DecomposeTime()[3]; }
	public double GetDayTime() { return time - GetDays()*oneDay; }

	public void AddBuilding(Building b) { buildings.Add(b); }
	public List<Building> GetBuildings() { return buildings; }

	void OnGUI()
	{
		if (selectedObject != null)
		{
			selectedObject.DrawInfoGUI();
		}

		string timestamp = "" + GetHours() + ":" + ((GetMinutes() >= 10) ? "" + GetMinutes() : ("0" + GetMinutes()));
		GUI.Label(new Rect(Screen.width - 50, Screen.height - 30, 200, 20), timestamp);
		string date = (GetDays()+1).ToString();
		GUI.Label(new Rect(Screen.width - 50, Screen.height - 45, 200, 20), "Day " + (date));

		GUI.Label(new Rect(Screen.width - 90, 50, 200, 20),  "Resources");
		GUI.Label(new Rect(Screen.width - 90, 80, 200, 20),  "Iron:    " + iron);
		GUI.Label(new Rect(Screen.width - 90, 110, 200, 20), "Pickaxe: " + pickaxe);
		GUI.Label(new Rect(Screen.width - 90, 140, 200, 20), "Wheat:   " + wheat);
		GUI.Label(new Rect(Screen.width - 90, 170, 200, 20), "Flour:   " + flour);
		GUI.Label(new Rect(Screen.width - 90, 200, 200, 20), "Bread:   " + bread);
	}
}
