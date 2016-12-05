using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Villager : MonoBehaviour {

	//villager's are the representation of the schedule/npc framework
	//every villager has an actions he's currently performing as well as a schedule he's currently working on completing
	//villagers can have job/moods/other meta assigned, these can be modified from the outside or completed actions and events throughout his day
	//based on that is existing schedule or future schedules can be modified by he schedule manager

	// Use this for initialization

	public List<Villager> villagers = new List<Villager>();
	public List<Building> buildings = new List<Building>();
	public List<Schedule> schedules = new List<Schedule>();

	void Start () {
	
	}

	void Update () {
	
	}
}
