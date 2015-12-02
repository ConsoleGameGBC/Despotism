﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class RandomEvents : MonoBehaviour {

	int temp = 0;
	GameObject myManager;
    
	[SerializeField] GameObject eventTopicObj;
	[SerializeField]  GameObject eventContentObj;
	[SerializeField] GameObject eventOptionContent;

	public int eventNo;
	//List<RandEvent> EventList = new List<RandEvent> ();
	public RandEvent[] EventArray;
	int numOfEvents;

	// Use this for initialization
	void Start () {
		myManager = this.gameObject;

        /*
        RandEvent[] EventArray = {
        new VisitingMerchant(myManager),
        new StolenFood(myManager),
        new Refugees (myManager),
        new Attacked(myManager),
        new Brahmin(myManager),
        new Brahmin2(myManager)
        };
        */
        /*
        EventArray = new RandEvent[4];
        EventArray[0] = new VisitingMerchant(myManager);
        EventArray[1] = new StolenFood(myManager);
        EventArray[2] = new Refugees(myManager);
        EventArray[3] = new Attacked(myManager);
        */

         EventArray = new RandEvent[] {
            new VisitingMerchant(myManager),
           new StolenFood(myManager),
          new Refugees(myManager),
           new Attacked(myManager),
           new Brahmin(myManager),
           new Brahmin2(myManager)
         };

        Debug.Log("EVENT ARRAY IS " + EventArray.Length.ToString());
        numOfEvents = EventArray.Length;

		//RandEvent[] eventTable = new RandEvent[2];

		//EventList.Add

		publishEvent ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void publishEvent(){

		//RandEvent eventInProgress = EventList.
		eventNo = UnityEngine.Random.Range (0, numOfEvents);
		RandEvent eventInProgress = EventArray[eventNo];
		eventTopicObj.GetComponent<Text> ().text = EventArray [eventNo].title;
        eventContentObj.GetComponent<Text>().text = EventArray[eventNo].text;
        eventOptionContent.GetComponent<Text>().text = EventArray[eventNo].option1;
		//eventTopicObj.GetComponent<UILabel> ().text = 

	}

    public void showResult(string result)
    {
        eventContentObj.GetComponent<Text>().text += "\n" + result;
        eventOptionContent.GetComponent<Text>().text = result;
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


    public void combatCalculator(bool isDefensive, int terrainType, int soldierNum, int strLevel)
    {
        float playerRange = 0.35f;
        float enemyRange = 0.0f;

        float playerMelee = 0.30f;
        float enemyMelee = 0.15f;

        int totalplayerLoss = 0;
        int playerCasMelee = 0;
        //GENERATE ENEMY TYPE FOR PROTOTYPE and NUMBER
        int enemyNum = UnityEngine.Random.Range(30, 80);


        float enemyCas = soldierNum * playerRange * UnityEngine.Random.Range(1.8f, 3.0f);
        float playerCas = enemyNum * enemyRange * UnityEngine.Random.Range(1.8f, 3.0f);
        Debug.Log("enemycas" + enemyCas);
        Debug.Log("playercas" + playerCas);

        if ((int)enemyCas > enemyNum)
            enemyCas = enemyNum;

        if ((int)playerCas > soldierNum)
            playerCas = soldierNum;

        soldierNum -= (int)playerCas;
        enemyNum -= (int)enemyCas;

        if (soldierNum > 0 && enemyNum > 0)
        {

            totalplayerLoss = (int)playerCas;
            enemyCas = 0;
            playerCas = 0;

            int enemyCasMelee = 0;
            //int playerCasMelee = 0;
            while ((int)enemyCas != enemyNum && (int)playerCas != soldierNum)
            {
                enemyCas = soldierNum * playerMelee * UnityEngine.Random.Range(0.8f, 2.5f);
                playerCas = enemyNum * enemyMelee * UnityEngine.Random.Range(0.8f, 2.5f);


                if ((int)enemyCas > enemyNum)
                    enemyCas = enemyNum;

                if ((int)playerCas > soldierNum)
                    playerCas = soldierNum;

                enemyCasMelee += (int)enemyCas;
                playerCasMelee += (int)playerCas;

                soldierNum -= (int)playerCas;
                enemyNum -= (int)enemyCas;
            }

        }


        totalplayerLoss += playerCasMelee;
        //change this later
        if(this.gameObject.GetComponent<Resource>().getSoldierPop() <= totalplayerLoss) //If number of deaths are lower then number of soldiers
        this.gameObject.GetComponent<Resource>().changeSoldier(-totalplayerLoss);       //kill that many soldiers
        else
        {
            this.gameObject.GetComponent<Resource>().changeSoldier(-this.gameObject.GetComponent<Resource>().getSoldierPop());
            totalplayerLoss -= this.gameObject.GetComponent<Resource>().getSoldierPop();
            this.gameObject.GetComponent<Resource>().decreasePop(totalplayerLoss);
        }



        myManager.GetComponent<Resource>().decreasePop(0);
    }

   




	
}

public abstract class RandEvent{
	public string title;
	public string text;
	public string option1;
	public string option2;
	public string option3;
    protected string result1;
    protected string result2;
    protected string result3;
    protected GameObject manager;
    public Resource myResourceClass;
    protected RandomEvents myEventClass;
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
        myResourceClass = GameObject.Find("GameManager").GetComponent<Resource>();
        myEventClass = GameObject.Find("GameManager").GetComponent<RandomEvents>();
    }

    
   //public  RandEvent() { }
	
	abstract public void ChoseO1();
	abstract public void ChoseO2();
	abstract public void ChoseO3();

    protected void updateResult(string myStr)
    {
        //manager.GetComponent<RandomEvents>().showResult(myStr);
        myEventClass.showResult(myStr);
    }
	
}


	

class VisitingMerchant : RandEvent{
	
	public VisitingMerchant(GameObject obj) : base(obj){
		title = "Traveling Merchant";
		text = "A Merchant is visiting. He offers to sell us 50 food for 50 fuel.";
		option1 = "Buy food";
		option2 = "Kill him and loot his stuff.";
		option3 = "Send him away.";
        result1 = "We bought food from the merchant.";
        result2 = "We killed the merchant and loot his stuff. This affected our people's morale.";
        result3 = "We sent him away.";
	}
	
	
	override public void ChoseO1(){
		//Bought food
        if(myResourceClass.getFuel() >= 50)
        {
            myResourceClass.changeFuel(-50);
            myResourceClass.changeFood(50);
        }
        updateResult(result1);
		
	}
	override public void ChoseO2(){
        updateResult(result2);
        //Killed the poor guy
        myResourceClass.changeFood(65);
        myResourceClass.changeWorkerMorale(-0.1f);
        myResourceClass.changeUnemployedMorale(-0.1f);

	}
	override public void ChoseO3(){
        //Nothing happens
        updateResult(result3);
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
        result1 = "They are executed by our soldiers. Our tribe learned a lesson.";
        result2 = "They are forgiven.";
        result3 = "Most of them don't come back from the mission. Not everyone's happy with your desicion.";
	}
	override public void ChoseO1(){
        updateResult(result1);
        myResourceClass.decreasePop(8);
		//Lose population
	}
	override public void ChoseO2(){
        updateResult(result2);
        myResourceClass.changeSoldierMorale(-0.1f);
        //They are forgiven
    }
	override public void ChoseO3(){
        updateResult(result3);
        //They are sent to a dangerous mission
        myResourceClass.decreasePop(4);
        myResourceClass.changeFood(20);
        myResourceClass.changeWater(10);
        myResourceClass.changeWorkerMorale(-0.05f);
        myResourceClass.changeUnemployedMorale(-0.05f);

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
        result1 = "We have new unemployed tribesmen";
        result2 = "We killed them and loot their stuff. Most people doesn't like your desicion.";
        result3 = "They are gone.";
	}
	override public void ChoseO1(){
        updateResult(result1);
        myResourceClass.changeUnemployed(10);
		//Gain pop
	}
	override public void ChoseO2(){
        updateResult(result2);
        myResourceClass.changeFood(10);
        myResourceClass.changeWater(10);
        myResourceClass.changeMedical(3);
        myResourceClass.changeFuel(2);
        myResourceClass.changeSoldierMorale(-0.05f);
        myResourceClass.changeWorkerMorale(-0.1f);
        myResourceClass.changeUnemployedMorale(-0.1f);
		//Got loot, lose rep
	}
	override public void ChoseO3(){
        updateResult(result3);
        //They are gone
    }
}

class Brahmin : RandEvent
{
    public Brahmin(GameObject obj):base(obj)
    {
        title = "Stray Animals";
        text = "It's uncanny! A group of stray cattle is seen close to our encampment! They seem to be healthy and not infected with the virus...";
        option1 = "It's time for a feast!";
        option2 = "Tell the men to add the animals to our inventory";
        option3 = "Do nothing. They might be infected.";
        result1 = "We have a feast! Everyone's morele increased.";
        result2 = "We have more food now.";
        result3 = "We don't risk the health of our tribe.";
    }
    public override void ChoseO1()
    {
        updateResult(result1);

    }

    public override void ChoseO2()
    {
        updateResult(result2);
        myResourceClass.changeFood(200);
    }

    public override void ChoseO3()
    {
        updateResult(result3);
    }
}

class Brahmin2 : RandEvent
{
    public Brahmin2(GameObject obj) : base(obj)
    {
        title = "Stray Animals";
        text = "It's uncanny! A group of stray cattle is seen close to our encampment! They seem to be healthy and not infected with the virus...";
        option1 = "It's time for a feast!";
        option2 = "Tell the men to add the animals to our inventory";
        option3 = "Do nothing. They might be infected.";
        result1 = "Some of the cattle were infected! Many of our people got sick and die, a few of them turn into zombies.";
        result2 = "some of the cattle were infected! Some people got sick and die.";
        result3 = "We don't risk the health of our tribe.";
    }
    public override void ChoseO1()
    {
        updateResult(result1);
        myResourceClass.decreasePop(50);
    }

    public override void ChoseO2()
    {
        updateResult(result2);
        myResourceClass.decreasePop(20);
    }

    public override void ChoseO3()
    {
        updateResult(result3);
    }
}

class Attacked : RandEvent
{
    public Attacked(GameObject obj) : base(obj)
    {
        title = "Attacked!";
        text = "Zombies are attacking the camp!";
        option1 = "Send only our soldiers to the defence";
        option2 = "Send every fit adult to the defences";
        option3 = "Send everyone to the defences, including the elderly and the youngsters";
        result1 = "Our defences held.";
        result2 = "Our defences held, but at what cost?";
        result3 = "Our defences held, but at what cost?";
    }

    override public void ChoseO1()
    {
        //manager.GetComponent<RandomEvents>().combatCalculator(true, 1, manager.GetComponent<Resource>().getSoldierPop(), 1);
        myEventClass.combatCalculator(true, 1, myResourceClass.getSoldierPop(), 1);
        updateResult(result1);
    }
    override public void ChoseO2()
    {
        //int temp = 50;
        //int temp = manager.GetComponent<Resource>().getSoldierPop() + manager.GetComponent<Resource>().getWorkerPop() + manager.GetComponent<Resource>().getUnemployedPop();
        int temp = myResourceClass.getSoldierPop() + myResourceClass.getWorkerPop() + myResourceClass.getUnemployedPop();
        //manager.GetComponent<RandomEvents>().combatCalculator(true, 1, temp, 1);
        myEventClass.combatCalculator(true, 1, temp, 1);

        updateResult(result2);
    }
    override public void ChoseO3()
    {
        // int temp = manager.GetComponent<Resource>().getSoldierPop() + manager.GetComponent<Resource>().getWorkerPop() + manager.GetComponent<Resource>().getUnemployedPop()
        //   + manager.GetComponent<Resource>().getYouthNElderPop();
        //manager.GetComponent<RandomEvents>().combatCalculator(true, 1, temp, 1);
        int temp = myResourceClass.getSoldierPop() + myResourceClass.getWorkerPop() + myResourceClass.getUnemployedPop() + myResourceClass.getYouthNElderPop();
        myEventClass.combatCalculator(true, 1, temp, 1);
        updateResult(result3);
    }

}
