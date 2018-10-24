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
                multiEdit((int)pos.x, (int)pos.y, drawHeight, radius, Mode.Circle);
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

    public void multiEdit(int x, int y, float newHeight, int radius, Mode mode)
    {

        List<Vector3> toEdit = new List<Vector3>();
        int xStart = radius - 1;
        int yStart = 0;
        int dx = 0;
        int dy = 0;
        int err = dx - (radius << 1);

        while (xStart > yStart)
        {
            toEdit.Add(new Vector3(x + xStart, y + yStart));
            toEdit.Add(new Vector3(x + yStart, y + xStart));
            toEdit.Add(new Vector3(x - yStart, y + xStart));
            toEdit.Add(new Vector3(x - xStart, y + yStart));
            toEdit.Add(new Vector3(x - xStart, y - yStart));
            toEdit.Add(new Vector3(x - yStart, y - xStart));
            toEdit.Add(new Vector3(x + yStart, y - xStart));
            toEdit.Add(new Vector3(x + xStart, y - yStart));

            if (err <= 0)
            {
                yStart++;
                err += dy;
                dy += 2;
            }

            if (err > 0)
            {
                xStart--;
                dx += 2;
                err += dx - (radius << 1);
            }

        }

        foreach(Vector3 v3 in toEdit)
        {
            editTile((int)v3.x, (int)v3.y, drawHeight);
        }
    }
}
