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
                placeUnit(testObject, hitTile.x, hitTile.y);
            }
        }
    }

    //places a unit at x, y in the generated world, returns true if successful, returns false if
    //that x, y was already occupied
    public bool placeUnit(GameObject unit, int x, int y)
    {
        if(occupied[x, y])
        {
            return false;
        }
        occupied[x, y] = true;
        Instantiate(unit, new Vector3(x, 1, y), Quaternion.identity);
        return true;

    }

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
