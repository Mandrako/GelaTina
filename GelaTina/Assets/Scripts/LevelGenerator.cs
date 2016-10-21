using System;
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class LevelGenerator : MonoBehaviour
{
    public int TileSize = 16;
    public Texture2D LevelMap;
    public ColorToPrefab[] ColorsToPrefabs;

    public void EmptyMap()
    {
        while (transform.childCount > 0)
        {
            Transform c = transform.GetChild(0);
            c.SetParent(null);
            DestroyImmediate(c.gameObject);
        }
    }

    public void LoadMap()
    {
        Color32[] allPixels = LevelMap.GetPixels32();
        int width = LevelMap.width;
        int height = LevelMap.height;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                SpawnPrefabAt(allPixels[(y * width) + x], x * TileSize, y * TileSize);
            }
        }
    }

    void SpawnPrefabAt(Color32 color, int x, int y)
    {
        if (color.a <= 0)
        {
            return;;
        }

        foreach (ColorToPrefab ctp in ColorsToPrefabs)
        {
            if (ctp.color.Equals(color))
            {
                GameObject go = (GameObject) Instantiate(ctp.prefab, new Vector3(x, y, ctp.prefab.transform.position.z), ctp.prefab.transform.rotation);
                go.transform.parent = this.transform;
                return;
            }
        }

        Debug.LogError("No prefab matches the color: " + color.ToString());
    }
}

[Serializable]
public class ColorToPrefab
{
    public Color32 color;
    public GameObject prefab;
}