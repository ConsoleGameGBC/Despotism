using UnityEngine;
using System.Collections;

public class TileMap : MonoBehaviour {

	public GameObject selectedUnit;
	public GameObject master;
	public TileType[] tileTypes;

	int[,] tiles;

	int mapSizeX = 20;
	int mapSizeY = 30;

	void Start() {
		GenerateMapData();
		GenerateMapVisual();
		master.transform.localScale = new Vector3 (0.04f,0.04f,0.001f);
        Quaternion temp = Quaternion.identity;
        temp.eulerAngles = new Vector3(90, 0, 0);
        master.transform.rotation = temp;
        master.transform.parent = GameObject.Find("MulitaryLeftPage").transform;
        temp.eulerAngles = new Vector3(0, 0, 1);
        GameObject.Find("MulitaryLeftPage").transform.localRotation = temp;
	}

	void GenerateMapData() {
		// Allocate our map tiles
		tiles = new int[mapSizeX,mapSizeY];
		
		int x,y;
		
		// Initialize our map tiles to be grass
		for(x=0; x < mapSizeX; x++) {
			for(y=0; y < mapSizeX; y++) {
                tiles[x, y] = Random.Range(0, 4); ; 
			}
		}

		// Make a big swamp area
        //for(x=3; x <= 5; x++) {
        //    for(y=0; y < 4; y++) {
        //        tiles[x,y] = 1;
        //    }
        //}
		
		// Let's make a u-shaped mountain range
       tiles[Random.Range(5, 15), Random.Range(5, 15)] =5;
	}

	void GenerateMapVisual() {
		for(int x=0; x < mapSizeX; x++) {
			for(int y=0; y < mapSizeX; y++) {
				TileType tt = tileTypes[ tiles[x,y] ];
				GameObject go = (GameObject)Instantiate( tt.tileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity );
				go.transform.parent= master.transform;
				ClickableTile ct = go.GetComponent<ClickableTile>();
				ct.tileX = x;
				ct.tileY = y;
				ct.map = this;
			}
		}

	}

	public Vector3 TileCoordToWorldCoord(int x, int y) {
		return new Vector3(x, y, 0);
	}

	public void MoveSelectedUnitTo(int x, int y) {
		selectedUnit.GetComponent<Unit>().tileX = x;
		selectedUnit.GetComponent<Unit>().tileY = y;
		selectedUnit.transform.position = TileCoordToWorldCoord(x,y);
	}

    void OnGUI()
    {
        //if (GUI.Button(new Rect(0, 0, 100, 30), "Random"))
        //{
        //    Application.LoadLevel(0);
        //}
    }
}
