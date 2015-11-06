﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class RandomEvents : MonoBehaviour {

	int temp = 0;
	GameObject myManager;
	[SerializeField] GameObject eventTopicObj;
	[SerializeField] GameObject eventContentObj;
	[SerializeField] GameObject eventOptionContent;

	public int eventNo;
	//List<RandEvent> EventList = new List<RandEvent> ();
	public RandEvent[] EventArray;
	int numOfEvents = 3;

	// Use this for initialization
	void Start () {
		myManager = this.gameObject;

		EventArray = new RandEvent[3];
		EventArray [0] = new VisitingMerchant (myManager);
		EventArray[1] = new StolenFood (myManager);
		EventArray [2] = new Refugees (myManager);

		//RandEvent[] eventTable = new RandEvent[2];

		//EventList.Add

		publishEvent ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void publishEvent(){

		//RandEvent eventInProgress = EventList.
		eventNo = Random.Range (0, numOfEvents);
		RandEvent eventInProgress = EventArray [eventNo];
		eventTopicObj.GetComponent<Text> ().text = EventArray [eventNo].title;
        eventContentObj.GetComponent<Text>().text = EventArray[eventNo].text;
        eventOptionContent.GetComponent<Text>().text = EventArray[eventNo].option1;
		//eventTopicObj.GetComponent<UILabel> ().text = 

	}

    public string outputOption(int value)
    {
        switch(value)
        {
            case(0):
                return EventArray[eventNo].option1;
                
            case(1):
                return EventArray[eventNo].option2;
                
            case(2):
                return EventArray[eventNo].option3;
        }
        return "Error";
        
    }

	public string getOptionText(int optNum){
		switch(optNum){
		case 1:
			return EventArray [eventNo].option1;
			
		case 2:
			return EventArray[eventNo].option2;
			
		case 3:
			return EventArray[eventNo].option3;
			
		default:
			Debug.Log("Error in getOptionText");
			return null;
			
		}
	}

	public void madeChoice(int optNum){
		switch (optNum) {
		case 0:
			EventArray[eventNo].ChoseO1();
			break;
		case 1:
			EventArray[eventNo].ChoseO2();
			break;
		case 2:
			EventArray[eventNo].ChoseO3();
			break;
		default:
			Debug.Log("Error in madeChoice");
			break;
		}
	}


	public void addFood(int amount)
		
	{
	}
	
	public void addWater(int x)
	{
		
	}




	
}

public abstract class RandEvent{
	public string title;
	public string text;
	public string option1;
	public string option2;
	public string option3;
    protected GameObject manager;
    protected Resource myResourceClass;
	/*
	RandEvent(string titl, string tex, string o1, string o2, string o3){
		title = titl;
		text = tex;
		option1 = o1;
		option2 = o2;
		option3 = o3;
	}
	*/

    public RandEvent(GameObject obj)
    {
        manager = obj;
        myResourceClass = manager.GetComponent<Resource>();

    }
   //public  RandEvent() { }
	
	abstract public void ChoseO1();
	abstract public void ChoseO2();
	abstract public void ChoseO3();
	
}


	

class VisitingMerchant : RandEvent{
	
	public VisitingMerchant(GameObject obj) : base(obj){
		title = "Traveling Merchant";
		text = "A Merchant is visiting. He offers to sell us 50 food for 50 fuel.";
		option1 = "Buy food";
		option2 = "Kill him and loot his stuff.";
		option3 = "Send him away."; 
	}
	
	
	override public void ChoseO1(){
		//Bought food
        if(myResourceClass.getFuel() >= 50)
        {
            myResourceClass.changeFuel(-50);
            myResourceClass.changeFood(50);
        }
		
	}
	override public void ChoseO2(){
        //Killed the poor guy
        myResourceClass.changeFood(50);
	}
	override public void ChoseO3(){
		//Nothing happens
	}
	
}

class StolenFood : RandEvent{
	public StolenFood(GameObject obj) : base(obj)
    {
        title = "Stolen Food";
		text = "A few of your people acted greedy and stole extra food from the stores. They awat your judgement.";
		option1 = "Execute them";
		option2 = "Forgive them.";
		option3 = "Send them on a dangerous mission.";
	}
	override public void ChoseO1(){
        myResourceClass.changeUnemployed(-3);
		//Lose population
	}
	override public void ChoseO2(){
		//They are forgiven
	}
	override public void ChoseO3(){
        //They are sent to a dangerous mission
        myResourceClass.changeUnemployed(-3);
    }
	
}

class Refugees:RandEvent{
	public Refugees(GameObject obj) : base(obj)
    {
        title = "Refugees";
		text = "A group of refugees arrive at your camp. They seek shelter.";
		option1 = "Accept them";
		option2 = "Kill them, loot their stuff.";
		option3 = "Send them away.";
	}
	override public void ChoseO1(){
        myResourceClass.changeUnemployed(10);
		//Gain pop
	}
	override public void ChoseO2(){
        myResourceClass.changeFood(10);
        myResourceClass.changeWater(10);
        myResourceClass.changeMedical(3);
        myResourceClass.changeFuel(2);
		//Got loot, lose rep
	}
	override public void ChoseO3(){
		//They are gone
	}
}