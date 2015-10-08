using UnityEngine;
using System.Collections;

public class Resource : MonoBehaviour {

	int Food;
	int Water;
	int Oil;
	
	int Population;
	int Power;
	
	
	
	
	// Use this for initialization
	void Start () {
		
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	public void endTurn(){
		if (Food >= Population) {
			Food -= Population;
		} else {
			Food = 0;
			// Some people starve. Decide what to do?
		}
		
		if (Water > Population) {
			Water -= Population;
		} else {
			Water = 0;
			// Some people are dehydrated. Add code.
		}
		
	}
	
	public void changeFood(int amount){
		Food += amount;
	}
	
	public void changeWater(int amount){
		Water += amount;
	}
	
	public void changeOil(int amount){
		Oil += amount;
	}
}
