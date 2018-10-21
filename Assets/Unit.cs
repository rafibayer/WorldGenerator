using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public float moveDelay = 1f;//delay between moves
    private float moveCountdown = 0f;

    public bool canMoveDiagonal = true;
    private WorldGenerator world;

    public List<string> legalTiles;

	void Start ()
    {
        world = GameObject.Find("worldGen").GetComponent<WorldGenerator>();
	}
	
	void Update ()
    {
		if(moveCountdown > 0f)
        {
            moveCountdown -= Time.deltaTime;
        }
        else
        {
            List<Vector3> moves = new List<Vector3>();
            moves = getValidMoves(canMoveDiagonal);
            //move
            if(moves.Count > 0)
            {
                Vector3 choice = moves[Random.Range(0, moves.Count - 1)];
                transform.position += new Vector3(choice.x, 0f, choice.y);
                moveCountdown += moveDelay;
            }
            

        }


	}
    
    private List<Vector3> getValidMoves(bool diag)
    {
        List<Vector3> possible = new List<Vector3>
        {
            new Vector3(1, 0),
            new Vector3(-1, 0),
            new Vector3(0, 1),
            new Vector3(0, -1)
        };
        if (diag)
        {
            possible.Add(new Vector3(1, 1));
            possible.Add(new Vector3(-1, 1));
            possible.Add(new Vector3(-1, 1));
            possible.Add(new Vector3(-1, -1));
        }
        
        List<Vector3> result = new List<Vector3>();
        Vector3 pos = transform.position;
        foreach (Vector3 v3 in possible)
        {
            worldTile tile = world.getTileInfo((int)(v3.x + pos.x), (int)(v3.y + pos.z));
            if (tile != null && legalTiles.Contains(tile.tileName))
            {
                result.Add(v3);
            }
            
        }

        return result;
    }
}
