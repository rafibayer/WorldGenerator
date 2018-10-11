using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public int octaves = 5;
    public float freq = 100.0f;
    public float amp = 1;

    public int width = 100;
    public int height = 100;

    public float[,] values;

    public GameObject waterTile;
    public GameObject groundTile;

    private float seed;

    private void Start()
    {
        seed = Random.value;

        values = new float[width, height];
        generateNoiseMap();
        noiseToTile();

    }

    public void generateNoiseMap()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                float noise = 0.0f;
                float gain = 1.0f;

                for(int o = 0; o < octaves; o++)
                {
                    noise += Mathf.PerlinNoise(seed + x * gain / freq, seed + y * gain / freq) * amp / gain;
                    gain *= 2.0f;

                    values[x, y] = noise;

                }

            }
        }

       
    }

    public void noiseToTile()
    {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x, 0, y);

                if (values[x,y] < 0.9f)
                {
                    Instantiate(waterTile, pos, Quaternion.identity, transform);
                }
                else
                {
                    Instantiate(groundTile, pos, Quaternion.identity, transform);

                }

            }

        }
    }

    public Texture2D getNoiseTexture()
    {
        generateNoiseMap();
        Texture2D tex = new Texture2D(width, height);

        Color[] pix = new Color[width * height];
        int index = 0;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                pix[index] = new Color(values[x, y] * 255, values[x, y] * 255, values[x, y] * 255);
                index++;
            }
        }

        tex.SetPixels(pix);
        tex.Apply();
        return tex;
    }
}
