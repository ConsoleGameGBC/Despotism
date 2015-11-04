using UnityEngine;
using System.Collections;

public class ClickableTile : MonoBehaviour {

	public int tileX;
	public int tileY;
	public TileMap map;

	void OnMouseUp() {
        Debug.Log(gameObject.tag + "  " + tileX + "," + tileY);
       // Debug.Log(tileX + "," + tileY);

		//map.MoveSelectedUnitTo(tileX, tileY);
	}

}
