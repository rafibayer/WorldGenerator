using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldTile : MonoBehaviour
{
    public string tileName = "";
    public float maxHeight;//max height this tile can occur at
    public List<Material> mats;//list of possible materials for this tile

    public WorldGenerator worldGen;

    public int x = 0;
    public int y = 0;

    private void Start()
    {
        
        if(mats.Count > 0)
        {
            //pick a random possbile material if there are multiple options
            gameObject.GetComponent<Renderer>().material = mats[Random.Range(0, mats.Count)];//set the material to a random value in mats

        }
    }

    //rebuild this tile based on height value
    public void rebuildTile()
    {
        worldTile newTile = worldGen.getTileInfo(x, y);
        newTile = Instantiate(newTile, transform.position, transform.rotation, worldGen.transform);
        newTile.worldGen = worldGen;
        worldGen.setTile(newTile.x, newTile.y, newTile);
        Destroy(gameObject);
    }

}

//comparator to allow sorting of tiles by maxHeight
public class WorldTileComp : IComparer<worldTile>//IComparer<worldTile> for tiles, sorted by max height
{
    public int Compare(worldTile myTile, worldTile otherTile)
    {
        if(myTile.maxHeight > otherTile.maxHeight)
        {
            return 1;
        }
        else if(myTile.maxHeight < otherTile.maxHeight)
        {
            return -1;
        }

        return 0;
    }
}
