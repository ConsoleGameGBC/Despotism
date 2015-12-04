using UnityEngine;
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
           new Brahmin2(myManager),
           new bards(myManager),
           new hotHouse(myManager),
           new waterSource(myManager),
           new storageFire(myManager),
           new rainyDay(myManager)
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


class rainyDay : RandEvent
{
    public rainyDay(GameObject obj) : base(obj)
    {
        title = "Rainy Day";
        text = "It's raining today. We can save a lot of water. Some people want to use this chance to clean themselves up. Some even want to wash their clothes! Being clean may cheer them up, if you think we can spare the water.";
        option1 = "No. We need every drop.";
        option2 = "Let them clean themselves a bit. We can spare some.";
        option3 = "Let them bathe, and wash their clothes. We don't need today's water.";
        result1 = "We save the water, it's too valuable";
        result2 = "We save some water, and your people are happy to be cleaner";
        result3 = "We don't save any water but your people are very grateful to be clean";
    }

    public override void ChoseO1()
    {
        myResourceClass.changeWater(350);
        updateResult(result1);
    }
    public override void ChoseO2()
    {
        myResourceClass.changeWater(170);
        myResourceClass.changeSoldierMorale(0.10f);
        myResourceClass.changeUnemployedMorale(0.10f);
        myResourceClass.changeWorkerMorale(0.1f);
        updateResult(result2);
    }
    public override void ChoseO3()
    {
        myResourceClass.changeSoldierMorale(0.25f);
        myResourceClass.changeUnemployedMorale(0.25f);
        myResourceClass.changeWorkerMorale(0.25f);
        updateResult(result3);
    }
}

class storageFire : RandEvent
{
    public storageFire(GameObject obj) : base(obj)
    {
        title = "Storage Fire!";
        text = "A shack where you store some of the food is on fire! Do we waste water to save the food?";
        option1 = "Use water to take out the fire!";
        option2 = "Do nothing, water is more important.";
        option3 = "Force some of the men to run in to save the food.";
        result1 = "We save the food, at the expense of some water.";
        result2 = "The shack burns down, with the food in it.";
        result3 = "We save the food, but lost some men. People are not happy.";
    }
    string result4 = "We didn't have enough water.";
    public override void ChoseO1()
    {
        if(myResourceClass.getWater() >= 75)
        {
            myResourceClass.changeWater(-75);
            updateResult(result1);
        }
        else
        {
            myResourceClass.changeFood(-100);
            updateResult(result4);
        }
    }

    public override void ChoseO2()
    {
        myResourceClass.changeFood(-100);
        updateResult(result2);
    }

    public override void ChoseO3()
    {
        myResourceClass.decreasePop(3);
        myResourceClass.changeWorkerMorale(-0.05f);
        myResourceClass.changeUnemployedMorale(-0.05f);
    }

}

class bards: RandEvent
{
    public bards(GameObject obj) : base(obj)
    {
        title = "Travelling bards?!";
        text = "A group of wierd people arrived at our camp on their bikes. They say they are travelling bards, singing songs about survivors, safety and hope. They say they can sing their songs for us, for a 'small donation'";
        option1 = "Give them 100 fuel";
        option2 = "Give them 50 food and 50 water";
        option3 = "Send them away";
        result1 = "They sing their songs. Your people are encouraged";
        result2 = "They sing their songs. Your people are encouraged";
        result3 = "They jump on their bikes and ride away.";
    }
    string result4 = "We don't have that much resources";

    public override void ChoseO1()
    {
        if(myResourceClass.getFuel() >= 100)
        {
            myResourceClass.changeFuel(-100);
            myResourceClass.changeSoldierMorale(0.25f);
            myResourceClass.changeUnemployedMorale(0.25f);
            myResourceClass.changeWorkerMorale(0.25f);
            updateResult(result1);
        }
        else
        {
            updateResult(result4);
        }
    }
    public override void ChoseO2()
    {
        if (myResourceClass.getFood() >= 50 && myResourceClass.getWater() >= 50)
        {
            myResourceClass.changeWater(-50);
            myResourceClass.changeFood(-50);
            myResourceClass.changeSoldierMorale(0.25f);
            myResourceClass.changeUnemployedMorale(0.25f);
            myResourceClass.changeWorkerMorale(0.25f);
            updateResult(result2);
        }
        else
        {
            updateResult(result4);
        }
    }
    public override void ChoseO3()
    {
        updateResult(result3);
    }

}

class hotHouse: RandEvent
{
    public hotHouse(GameObject obj) : base(obj)
    {
        title = "Hothouse suggestion";
        text = "One of our people is a farmer. She suggests we build a hothouse to grow food. She says she will need fuel and water.";
        option1 = "Give her 100 water and 100 fuel.";
        option2 = "Give her 50 water and 50 fuel.";
        option3 = "Do nothing.";
        result1 = "She builds the hothouse and grows 350 food";
        result2 = "She builds the hothouse and grows 150 food";
        result3 = "You decide to do nothing.";
    }

    string result4 = "We don't have enough resources";
    public override void ChoseO1()
    {
        if(myResourceClass.getFuel() >= 100 && myResourceClass.getWater() >= 100)
        {
            myResourceClass.changeFood(350);
            myResourceClass.changeFuel(-100);
            myResourceClass.changeWater(-100);
            updateResult(result1);
        }
        else
        {
            updateResult(result4);
        }
    }

    public override void ChoseO2()
    {
        if (myResourceClass.getFuel() >= 50 && myResourceClass.getWater() >= 50)
        {
            myResourceClass.changeFood(150);
            myResourceClass.changeFuel(-50);
            myResourceClass.changeWater(-50);
            updateResult(result2);
        }
        else
        {
            updateResult(result4);
        }
    }

    public override void ChoseO3()
    {
        updateResult(result3);
    }
}


class waterSource : RandEvent
{

    public waterSource(GameObject obj) : base(obj)
    {
        title = "Possible Water Source";
        text = "One of our people thinks there's underground water nearby. He says if we give him fuel for his machines, he can dig us the water.";
        option1 = "Give him the fuel.";
        option2 = "Forget the fuel, force people to dig by hand.";
        option3 = "Do nothing.";
        result1 = "He digs up with his machines and finds a decent amount of drinkable water.";
        result2 = "We force people to dig up, with their bare hands when necessary. We reach the water but people are unhappy with your desicion.";
        result3 = "You decided not to take action";
    }

    public override void ChoseO1()
    {
        if(myResourceClass.getFuel() >= 50)
        {
            myResourceClass.changeFuel(-50);
            myResourceClass.changeWater(125);
            updateResult(result1);
        }
        else
        {
            updateResult("We couldn't gather enough fuel for him.");
        }
    }

    public override void ChoseO2()
    {
        myResourceClass.changeWater(105);
        myResourceClass.changeWorkerMorale(-0.08f);
        myResourceClass.changeUnemployedMorale(-0.06f);
        updateResult(result2);
    }

    public override void ChoseO3()
    {
        updateResult(result3);
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

    string result4 = "We didn't have enough fuel to trade.";
	
	override public void ChoseO1(){
		//Bought food
        if(myResourceClass.getFuel() >= 50)
        {
            myResourceClass.changeFuel(-50);
            myResourceClass.changeFood(50);
            updateResult(result1);
        }
        else
        {
            updateResult(result4);
        }

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
