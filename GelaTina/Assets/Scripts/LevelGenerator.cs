using System;
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class LevelGenerator : MonoBehaviour
{
    public int TileSize = 16;
    public int LayerIndex = 0;
    public Texture2D LevelMap;
    public ColorToPrefab[] ColorsToPrefabs;

    private GameObject _layer;

    public void EmptyMap()
    {
        if (_layer != null)
        {
            while (_layer.transform.childCount > 0)
            {
                Transform c = _layer.transform.GetChild(0);
                c.SetParent(null);
                DestroyImmediate(c.gameObject);
            }
        }
    }

    public void LoadMap()
    {
        Color32[] allPixels = LevelMap.GetPixels32();
        int width = LevelMap.width;
        int height = LevelMap.height;

        _layer = GameObject.Find("Layer_" + LayerIndex);

        if (_layer == null)
        {
            GameObject layer = new GameObject();
            layer.name = "Layer_" + LayerIndex;
            layer.transform.parent = this.transform;
            _layer = layer;
        }

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
                GameObject go = (GameObject) Instantiate(ctp.prefab, new Vector3(x, y, LayerIndex), ctp.prefab.transform.rotation);
                go.transform.parent = _layer.transform;
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