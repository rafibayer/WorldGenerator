using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldEditor : MonoBehaviour
{

    private WorldGenerator worldGen;

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
                editTile((int)pos.x, (int)pos.y, 1f);
            }
        }
    }

    private void Start()
    {
        worldGen = GameObject.Find("worldGen").GetComponent<WorldGenerator>();
    }

    public void editTile(int x, int y, float newHeight)
    {
        worldGen.setValue(x, y, newHeight);
        worldGen.getTile(x, y).rebuildTile();
    }
}
