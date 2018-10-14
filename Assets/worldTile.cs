using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldTile : MonoBehaviour
{

    public float maxHeight;

}

public class WorldTileComp : IComparer<worldTile>
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
