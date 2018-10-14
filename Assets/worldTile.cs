using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldTile : MonoBehaviour
{
    public string tileName = "";
    public float maxHeight;//max height this tile can occur at
    public List<Material> mats;//list of possible materials for this tile

    private void Start()
    {
        if(mats.Count > 0)
        {
            gameObject.GetComponent<Renderer>().material = mats[Random.Range(0, mats.Count)];//set the material to a random value in mats

        }
    }

}

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
