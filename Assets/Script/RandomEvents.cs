using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RandomEvents : MonoBehaviour {

	int temp = 0;
	GameObject myManager;
	[SerializeField] GameObject eventTopicObj;
	[SerializeField] GameObject eventContentObj;
	[SerializeField] GameObject eventOptionContent;

	//List<RandEvent> EventList = new List<RandEvent> ();
	RandEvent[] EventArray;
	int numOfEvents = 3;

	// Use this for initialization
	void Start () {
		myManager = this.gameObject;

		EventArray = new RandEvent[3];
		EventArray [0] = new VisitingMerchant ();
		EventArray[1] = new StolenFood ();
		EventArray [2] = new Refugees ();

		//RandEvent[] eventTable = new RandEvent[2];

		//EventList.Add

		publishEvent ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void publishEvent(){

		//RandEvent eventInProgress = EventList.
		int eventNo = Random.Range (0, numOfEvents);
		RandEvent eventInProgress = EventArray [eventNo];
		eventTopicObj.GetComponent<UILabel> ().text = EventArray [eventNo].title;
		eventContentObj.GetComponent<UILabel> ().text = EventArray [eventNo].text;
		eventOptionContent.GetComponent<UILabel> ().text = EventArray [eventNo].option1;
		//eventTopicObj.GetComponent<UILabel> ().text = 

	}




	public void addFood(int amount)
		
	{
	}
	
	public void addWater(int x)
	{
		
	}




	
}

abstract class RandEvent{
	public string title;
	public string text;
	public string option1;
	public string option2;
	public string option3;

	/*
	RandEvent(string titl, string tex, string o1, string o2, string o3){
		title = titl;
		text = tex;
		option1 = o1;
		option2 = o2;
		option3 = o3;
	}
	*/
	
	abstract public void ChoseO1();
	abstract public void ChoseO2();
	abstract public void ChoseO3();
	
}


	

class VisitingMerchant : RandEvent{
	
	public VisitingMerchant(){
		title = "Traveling Merchant";
		text = "A Merchant is visiting. He offers to sell us food.";
		option1 = "Buy food";
		option2 = "Kill him and loot his stuff.";
		option3 = "Send him away.";
	}
	
	
	override public void ChoseO1(){
		//Bought food

		
	}
	override public void ChoseO2(){
		//Killed the poor guy
	}
	override public void ChoseO3(){
		//Nothing happens
	}
	
}

class StolenFood : RandEvent{
	public StolenFood(){
		title = "Stolen Food";
		text = "A few of your people acted greedy and stole extra food from the stores. They awat your judgement.";
		option1 = "Execute them";
		option2 = "Forgive them.";
		option3 = "Send them on a dangerous mission.";
	}
	override public void ChoseO1(){
		//Lose population
	}
	override public void ChoseO2(){
		//They are forgiven
	}
	override public void ChoseO3(){
		//They are sent to a dangerous mission
	}
	
}

class Refugees:RandEvent{
	public Refugees(){
		title = "Refugees";
		text = "A group of refugees arrive at your camp. They seek shelter.";
		option1 = "Accept them";
		option2 = "Kill them, loot their stuff.";
		option3 = "Send them away.";
	}
	override public void ChoseO1(){
		//Gain pop
	}
	override public void ChoseO2(){
		//Got loot, lose rep
	}
	override public void ChoseO3(){
		//They are gone
	}
}