using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propGen : MonoBehaviour {

    private WorldGenerator worldGen;

    private int width;
    private int height;

    public GameObject tree;
    public float minVal;

    private float[,] values;
    private float seed;

    private void Start()
    {
        worldGen = GameObject.Find("worldGen").GetComponent<WorldGenerator>();
        width = worldGen.width;
        height = worldGen.height;
        values = new float[width, height];
        seed = Random.value;
        generateNoise();
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.1f);
        noiseToProp();
    
    }

    private void generateNoise()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                values[x, y] = Mathf.PerlinNoise(x * seed, y * seed);
            }
        }
    }

    private void noiseToProp()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(values[x,y] > minVal && worldGen.getTileInfo(x,y).tileName.Equals("ground"))
                {
                    Instantiate(tree, new Vector3(x, 1, y), Quaternion.identity);
                }
            }
        }
    }
}
