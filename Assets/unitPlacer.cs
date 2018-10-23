using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitPlacer : MonoBehaviour {

    private WorldGenerator worldGen;
    private bool[,] occupied;

    public GameObject testObject;

	void Start () {
        StartCoroutine(LateStart());
        worldGen = GameObject.Find("worldGen").GetComponent<WorldGenerator>();
	}

    private void Update()
    {
        //test for building placement
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            if(hit.collider != null && hit.collider.tag.Equals("Tile"))
            {
                worldTile hitTile = hit.collider.GetComponent<worldTile>();
                placeUnit(testObject, hitTile.x, hitTile.y, true);
            }
        }

        
    }

    //places a unit at x, y in the generated world, returns true if successful, returns false if
    //that x, y was already occupied
    public bool placeUnit(GameObject unit, int x, int y, bool occupy)
    {
        if(occupied[x, y])
        {
            return false;
        }

        if (occupy)
        {
            occupied[x, y] = true;
        }
        Instantiate(unit, new Vector3(x, 1, y), Quaternion.identity);
        return true;

    }

    //places a unit at xPlace, yPlace, occupying from xStart, yStart to xTo, yTo (inclusive)
    //returns true if placed succesfully, returns false otherwise
    public bool placeUnit(GameObject unit, int xPlace, int yPlace, 
                            int xStart, int yStart, int xTo, int yTo, bool occupy)
    {
        if(xPlace < xStart || yPlace < yStart || xPlace > xTo || yPlace > yTo)
        {
            throw new System.ArgumentException(
                "xPlace and yPlace must be within xStart, yStart and xTo, yTo");
        }

        if(!checkTiles(xStart, yStart, xTo, yTo))
        {
            return false;
        }

        if(occupy)
        {
            for (int x = xStart; x <= xTo; x++)
            {
                for (int y = yStart; y <= yTo; y++)
                {
                    occupied[x, y] = true;
                }
            }
        }
       

        Instantiate(unit, new Vector3(xPlace, 1, yPlace), Quaternion.identity);
        return true;

    }

    //returns true if tile at x, y is open, returns false otherwise
    public bool checkTile(int x, int y)
    {
        return occupied[x, y];
    }

    //returns true if tiles between x,y and xTo, yTo are open (inclusive)
    //returns false otherwise
    public bool checkTiles(int xStart, int yStart, int xTo, int yTo)
    {
        for(int x = xStart; x <= xTo; x++)
        {
            for(int y = yStart; y <= yTo; y++)
            {
                if(occupied[x, y])
                {
                    return false;
                }
            }
        }

        return true;
    }

    //start late so that worldGen can generate the world first
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.1f);
        occupied = new bool[worldGen.width, worldGen.height];
        for(int x = 0; x < worldGen.width; x++)
        {
            for(int y = 0; y < worldGen.height; y++)
            {
                occupied[x, y] = false;
            }
        }

    }
}
