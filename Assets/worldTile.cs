using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldTile : MonoBehaviour
{
    public string tileName = "";//name of the this tile
    public float maxHeight;//max height this tile can occur at
    public List<Material> mats;//list of possible materials for this tile

    public WorldGenerator worldGen;//refrence to worldGenerator

    public int x = 0;//x position
    public int y = 0;//y position

    private void Start()
    {
        
        if(mats.Count > 0)
        {
            //pick a random possbile material if there are multiple options
            gameObject.GetComponent<Renderer>().material = mats[Random.Range(0, mats.Count)];//set the material to a random value in mats

        }
    }

    //replaces this tile with the appropriate tile for a new height
    public void replaceTile(float newHeight)
    {
        worldTile tileForHeight = worldGen.getTileForHeight(worldGen.getValue(x, y));
        GameObject replacement = Instantiate(tileForHeight, transform.position, transform.rotation, worldGen.transform).gameObject;
        worldTile tile = replacement.GetComponent<worldTile>();
        tile.worldGen = worldGen;
        tile.x = x;
        tile.y = y;

        Debug.Log(tile.x + "," + tile.y);
        worldGen.tiles[x, y] = replacement;


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
