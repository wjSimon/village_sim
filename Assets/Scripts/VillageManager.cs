using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VillageManager : MonoBehaviour
{

	//Tracks the village
	//info on villagers, creates schedules for villagers, executes schedules on villagers
	//tracks meta data like seasons if we want
	//
	private static VillageManager instance;

	public List<Villager> villagers = new List<Villager>();
	public List<Building> buildings = new List<Building>();

	public InfoObject selectedObject;

	public double time;
	public float timeScale;

	public const int oneMinute = 60;
	public const int oneHour = 60 * oneMinute;
	public const int oneDay = 24 * oneHour;

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
	}
	void Start()
	{
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
		time += Time.deltaTime * timeScale;
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

	void OnGUI()
	{
		if (selectedObject != null)
		{
			selectedObject.DrawInfoGUI();
		}

		string timestamp = "" + GetHours() + ":" + ((GetMinutes() >= 10) ? "" + GetMinutes() : ("0" + GetMinutes()));
		GUI.Label(new Rect(Screen.width - 50, Screen.height - 30, 200, 20), timestamp);
	}
}
