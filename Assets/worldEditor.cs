using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldEditor : MonoBehaviour
{

    public float drawHeight = 0.5f;
    private WorldGenerator worldGen;

    public int radius = 3;
    public Mode mode = Mode.Circle;

    public enum Mode
    {
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
                multEdit((int)pos.x, (int)pos.y, drawHeight, radius);
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
        worldGen.getTile(x, y).rebuildTile();
    }

    public void multEdit(int x, int y, float newHeight, int radius)
    {
        Vector3 pos = new Vector3(x, 0, y);
        Collider[] toEdit = Physics.OverlapSphere(pos, radius, LayerMask.GetMask("Tile"));

        Debug.Log(toEdit.Length);

        foreach(Collider col in toEdit)
        {
            worldTile tile = col.gameObject.GetComponent<worldTile>();
            worldGen.setValue(tile.x, tile.y, newHeight);
            worldGen.getTile(tile.x, tile.y).rebuildTile();
        }

    }
   
}
