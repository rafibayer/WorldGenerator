using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldEditor : MonoBehaviour
{

    public float drawHeight = 0.5f;//the height to set edited tiles to
    private WorldGenerator worldGen;//refrence to the worldGenerator

    public int size = 3;//radius to edit in for circle and square
    public Mode mode = Mode.Circle;//the current edit mode

    //edit mode
    public enum Mode
    {
        Single,
        Circle,
        Square
    };

    private void Update()
    {
        //testing, set tileHeight to 1
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            if (hit.collider != null && hit.collider.tag.Equals("Tile"))
            {
                worldTile hitTile = hit.collider.GetComponent<worldTile>();
                Vector3 pos = new Vector3(hitTile.x, hitTile.y);

                switch(mode)
                {
                    case Mode.Single:
                        editTile((int)pos.x, (int)pos.y, drawHeight);
                        break;
                    case Mode.Circle:
                        multEdit((int)pos.x, (int)pos.y, drawHeight, size);
                        break;
                    default:
                        editTile((int)pos.x, (int)pos.y, drawHeight);
                        break;


                }
                
            }
        }
    }

    private void Start()
    {
        worldGen = GameObject.Find("worldGen").GetComponent<WorldGenerator>();
    }

    //set the tile at x, y to a new height
    public void editTile(int x, int y, float newHeight)
    {
        worldGen.setValue(x, y, newHeight);
        worldGen.tiles[x, y].GetComponent<worldTile>().replaceTile(newHeight);
    }

    //set all tiles in a radius of x,y to a new height
    public void multEdit(int x, int y, float newHeight, int radius)
    {
        Vector3 pos = new Vector3(x, 0, y);
        Collider[] toEdit = Physics.OverlapSphere(pos, radius, LayerMask.GetMask("Tile"));

        
        foreach(Collider col in toEdit)
        {
            worldTile tile = col.gameObject.GetComponent<worldTile>();
            worldGen.setValue(tile.x, tile.y, newHeight);
            worldGen.tiles[tile.x, tile.y].GetComponent<worldTile>().replaceTile(newHeight);

        }

    }

    public void squareEdit(int x, int y, float newHeight, int sideLength)
    {

    }
   
}
