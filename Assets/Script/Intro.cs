using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Intro : MonoBehaviour {
    [SerializeField]
    GameObject goalTextObject;

    [SerializeField]
    GameObject introTextObject;

    string introText = "Welcome, o fearless leader of our tribe. Since the day dead walked our people suffered horribly."
        +" A bunch of survivors gathered here to form a camp. Here we formed our tribe.";
    string goalText = "A group of scientists in a nearby lab claims they are working on a cure. They ask for our protection."
        +"They are hopeful of their resarch but they say they will need a month before it is done. Lead your tribe and survive for 30 days. \n \nLosing most of your survivor tribe, and dropping to a population of 100 will mean failure.";


	// Use this for initialization
	void Start () {
        assignIntroText();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void assignIntroText()
    {
        introTextObject.GetComponent<Text>().text = introText;
		goalTextObject.GetComponent<Text>().text = goalText;
    }
}
