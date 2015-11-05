using UnityEngine;
using System.Collections;

public class ClickableTile : MonoBehaviour {

	public int tileX;
	public int tileY;
	public TileMap map;

    void OnMouseUp() {
        Debug.Log(gameObject.tag);
   //  Debug.Log(gameObject.tag + "  " + tileX + "," + tileY);
    //   // Debug.Log(tileX + "," + tileY);
        
       //map.MoveSelectedUnitTo(tileX, tileY);
    }

    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            tileY++;
          
         // Debug.Log(gameObject.tag + "  " + tileX + "," + tileY);
            map.MoveSelectedUnitTo(tileX, tileY);
          //  Debug.Log(tileX + "," + tileY);
          
        }

        else  if (Input.GetKeyDown("s"))
        {
           tileY--;
        
        //    Debug.Log(gameObject.tag + "  " + tileX + "," + tileY);
            map.MoveSelectedUnitTo(tileX, tileY);
        }
        else if (Input.GetKeyDown("a"))
        {
            tileX--;
       
        //    Debug.Log(gameObject.tag + "  " + tileX + "," + tileY);
            map.MoveSelectedUnitTo(tileX, tileY);
        }
        else if (Input.GetKeyDown("d"))
        {
            tileX++;
           
           // Debug.Log(gameObject.tag + "  " + tileX + "," + tileY);
            map.MoveSelectedUnitTo(tileX, tileY);
        }
       
    }



}
