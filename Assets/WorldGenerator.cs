using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{


    public Mode mode = Mode.generate;
    public bool normalize = true;
    public int octaves = 5;
    public float freq = 100.0f;
    public float amp = 1;

    public int width = 100;
    public int height = 100;
    public float scale = 1f;

    public float[,] values;

    public float waterLevel = 0.65f;
    public float beachLevel = 0.7f;
    public float groundLevel = 0.82f;
    public float mountainLevel = 0.85f;
    public float snowLevel = 0.95f;

    public GameObject waterTile;
    public GameObject beachTile;
    public GameObject groundTile;
    public GameObject mountainTile;
    public GameObject snowTile;

    public string savedPath;

    private float seed;

    public enum Mode
    {
        generate,
        read
    };

    private void Start()
    {
        if(mode == Mode.generate)
        {
            seed = Random.value;
            values = new float[width, height];
            generateNoiseMap();
            noiseToTile();
            writeToFile();
        }
        else if(mode == Mode.read)
        {
            string[] lines = System.IO.File.ReadAllLines(savedPath);
            string[] dim = lines[lines.Length - 1].Split('x');
            values = new float[int.Parse(dim[0]), int.Parse(dim[1])];
            width = int.Parse(dim[0]); height = int.Parse(dim[1]);
            for(int row = 0; row < lines.Length - 1; row++)
            {

                string[] split = lines[row].Split(' ');
                //-1 because i didn't account for fencepost when adding spaces
                for(int col = 0; col < split.Length - 1; col++)
                {
                    values[row, col] = float.Parse(split[col]);
                    
                }
                
            }

            noiseToTile();
           

        }
        

    }

    public void generateNoiseMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float noise = 0.0f;
                float gain = 1.0f;

                for (int o = 0; o < octaves; o++)
                {
                    noise += Mathf.PerlinNoise(seed + x * gain / freq, seed + y * gain / freq) * amp / gain;
                    gain *= 2.0f;

                    values[x, y] = noise;

                }

            }
        }

        if (normalize)
        {
            float max = 0f;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (values[x, y] > max)
                    {
                        max = values[x, y];
                    }
                }
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    values[x, y] /= max;
                }
            }
        }
    }

    public void writeToFile()
    {
       string[] lines = new string[height + 1];
       for(int y = 0; y < height; y++)
       {
            string line = "";
            for(int x = 0; x < width; x++)
            {
                line += values[x, y] + " ";
            }
            lines[y] = line;
       }
       lines[lines.Length - 1] = width + "x" + height;

       System.IO.File.WriteAllLines(@"" + savedPath, lines);

    }

    public void noiseToTile()
    {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x * scale, 0, y * scale);
                float val = values[x, y];

                if (val < waterLevel)
                {
                    Instantiate(waterTile, pos, Quaternion.identity, transform);
                }
                else if(val < beachLevel)
                {
                    Instantiate(beachTile, pos, Quaternion.identity, transform);

                } 
                else if(val < groundLevel)
                {
                    Instantiate(groundTile, pos, Quaternion.identity, transform);

                }
                else if(val < mountainLevel)
                {
                    Instantiate(mountainTile, pos, Quaternion.identity, transform);

                }
                else
                {
                    Instantiate(snowTile, pos, Quaternion.identity, transform);

                }



            }

        }
    }

    

    
}
