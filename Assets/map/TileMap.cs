using UnityEngine;
using System.Collections;

public class TileMap : MonoBehaviour {
	public TileType[] tileTypes;
	int[,] tiles;
	int mapSizeX = 20;
	int mapSizeY = 30;
	// Use this for initialization
	void Start () {
		GenerateMapData();
		GenerateMapVisual();

		}


	void GenerateMapData() {
		// Allocate our map tiles
		tiles = new int[mapSizeX,mapSizeY];
		
		int x,y;
		
		// Initialize our map tiles to be grass
		for(x=0; x < mapSizeX; x++) {
			for(y=0; y < mapSizeX; y++) {
				tiles[x,y] = Random.Range(0,5);
			}
		}
		

		
		// Let's make a u-shaped mountain range

		
	}
	
	void GenerateMapVisual() {
		for(int x=0; x < mapSizeX; x++) {
			for(int y=0; y < mapSizeX; y++) {
				TileType tt = tileTypes[ tiles[x,y] ];
				Instantiate( tt.tileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity );
			}
		}
		
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(0,0,100,30),"Random"))
		{
			Application.LoadLevel(0);	
		}
	}
}
