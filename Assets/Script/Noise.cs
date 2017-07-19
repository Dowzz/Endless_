using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise {
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int Octaves, float persistance, float lacunarity, Vector2 Offset)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];
        System.Random prng = new System.Random(seed);
        Vector2[] OctaveOffsets = new Vector2[Octaves];
        for (int i = 0; i < Octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + Offset.x;
            float offsetY = prng.Next(-100000, 100000) + Offset.y;
            OctaveOffsets[i] = new Vector2(offsetX, offsetY);

        }

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxNoiseHeiht = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfWeight = mapHeight / 2f;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;
                for (int i = 0; i < Octaves; i++)
                {
                    float sampleX = (x-halfWidth) / scale * frequency + OctaveOffsets[i].x;
                    float sampleY = (y-halfWeight) / scale * frequency + OctaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) *2 -1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight >maxNoiseHeiht)
                {
                    maxNoiseHeiht = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
                
            }
        }
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeiht, noiseMap[x, y]);
            }
        }
                return noiseMap;
    }
}

