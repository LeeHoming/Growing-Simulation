using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFormer : MonoBehaviour
{
    public static int

            width = 41,
            height = 41;

    public static int

            insideWidht = 31,
            insideHeight = 31;

    public float scale = 10;

    public float

            offsetX,
            offsetY,
            octaves,
            persistance,
            lacunarity;

    public float threshold;

    public TerrainUnit[,] grid = new TerrainUnit[width, height];

    private void Start()
    {
        PlaceTerrain();
    }

    void Update()
    {
        //Renderer renderer = GetComponent<Renderer>();
        //renderer.material.mainTexture = GenerateTexture();
        UnitHeight();
    }

    void UnitHeight()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y].offsetValue = CalculateColor(x, y);
                //texture.SetPixel (x, y, color);
            }
        }
    }

    // Texture2D GenerateTexture()
    // {
    //     Texture2D texture = new Texture2D(width, height);
    //     for (int x = 0; x < width; x++)
    //     {
    //         for (int y = 0; y < height; y++)
    //         {
    //             Color color = CalculateColor(x, y);
    //             texture.SetPixel (x, y, color);
    //         }
    //     }
    //     texture.Apply();
    //     return texture;
    // }
    float CalculateColor(int x, int y)
    {
        float amplitude = 1;
        float frequency = 1;
        float noiseHeight = 0;

        for (int i = 0; i < octaves; i++)
        {
            // float xCoord = (float) x / width * scale + offsetX;
            // float yCoord = (float) y / height * scale + offsetY;
            float xCoord = ((float) x / scale + offsetX) * frequency;
            float yCoord = ((float) y / scale + offsetY) * frequency;

            float sample = Mathf.PerlinNoise(xCoord, yCoord) * 2 - 1;
            noiseHeight += sample * amplitude;

            amplitude *= persistance;
            frequency *= lacunarity;
        }

        return noiseHeight;
    }

    //adjustHeight
    void PlaceTerrain()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                TerrainUnit TerrainUnit =
                    Instantiate(Resources
                        .Load("TerrainBox", typeof (TerrainUnit)),
                    new Vector3(x - (float) width / 2,
                        0,
                        y - (float) height / 2),
                    Quaternion.identity) as
                    TerrainUnit;
                grid[x, y] = TerrainUnit;

                grid[x, y].transform.SetParent(transform, true);

                if (
                    x >= (width - insideWidht) / 2 &&
                    x < (width - insideWidht) / 2 + insideWidht &&
                    y >= (height - insideHeight) / 2 &&
                    y < (height - insideHeight) / 2 + insideHeight
                )
                {
                    grid[x, y].colorUnit = true;
                }
            }
        }
    }
}

public static class ExtensionMethods
{
    public static float
    Remap(this float value, float from1, float to1, float from2, float to2)
    {
        if (value <= from1)
        {
            value = from1;
        }
        if (value >= to1)
        {
            value = to1;
        }
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
