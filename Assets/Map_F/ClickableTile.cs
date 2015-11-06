using UnityEngine;
using System.Collections;

public class ClickableTile : MonoBehaviour {

	public int tileX;
	public int tileY;
	public TileMap map;

    //void OnMouseUp() {
     //   Debug.Log(gameObject.tag);
   //  Debug.Log(gameObject.tag + "  " + tileX + "," + tileY);
    //   // Debug.Log(tileX + "," + tileY);
        
       //map.MoveSelectedUnitTo(tileX, tileY);
   // }

    void Update()
    {
		//if(Input.GetKeyDown("m"))
		  // {
			//Debug.Log(gameObject.tag + "  " + tileX + "," + tileY);

		//}





        if (Input.GetKeyDown("w"))
        {
            tileY++;
          if(tileY>=19)
			{
				tileY=19;
			}
       // Debug.Log(gameObject.tag + "  " + tileX + "," + tileY);
            map.MoveSelectedUnitTo(tileX, tileY);
          //  Debug.Log(tileX + "," + tileY);
          
        }

        else  if (Input.GetKeyDown("s"))
        {
           tileY--;
        if(tileY <=0)
			{
				tileY=0;
			}
         //   Debug.Log(gameObject.tag + "  " + tileX + "," + tileY);
            map.MoveSelectedUnitTo(tileX, tileY);
        }
        else if (Input.GetKeyDown("a"))
        {
            tileX--;
			if(tileX<=0)
			{
				tileX=0;

			}
       
          // Debug.Log(gameObject.tag + "  " + tileX + "," + tileY);
            map.MoveSelectedUnitTo(tileX, tileY);
        }
        else if (Input.GetKeyDown("d"))
        {
            tileX++;
			if(tileX>=19)

			{
				tileX=19;
			}
           //Debug.Log(gameObject.tag + "  " + tileX + "," + tileY);
            map.MoveSelectedUnitTo(tileX, tileY);
        }
       
    }



}
