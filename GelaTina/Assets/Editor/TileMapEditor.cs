using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileMap))]
public class TileMapEditor : Editor
{
    public TileMap map;

    TileBrush brush;
    Vector3 mouseHitPos;

    bool mouseOnMap
    {
        get { return mouseHitPos.x > 0 && mouseHitPos.x < map.gridSize.x && mouseHitPos.y < 0 && mouseHitPos.y > -map.gridSize.y; }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        var oldSolidState = map.isSolid;

        map.isSolid = EditorGUILayout.Toggle("Solid Tiles", map.isSolid);

        if(oldSolidState != map.isSolid)
        {
            UpdateSolidState();
        }

        var oldDepth = map.layerDepth;

        map.layerDepth = EditorGUILayout.IntField("Layer Depth: ", map.layerDepth);

        if(map.layerDepth != oldDepth)
        {
            UpdateDepth();
        }

        var oldSize = map.mapSize;

        map.mapSize = EditorGUILayout.Vector2Field("Map Size:", map.mapSize);

        if(map.mapSize != oldSize)
        {
            UpdateCalculations();
        }

        var oldTexture = map.texture2D;
        map.texture2D = (Texture2D)EditorGUILayout.ObjectField("Texture2D:", map.texture2D, typeof(Texture2D), false);

        if(oldTexture != map.texture2D)
        {
            UpdateCalculations();
            map.tileID = 1;
            CreateBrush();
        }

        if(map.texture2D == null)
        {
            EditorGUILayout.HelpBox("You have not selected a texture 2D yet.", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.LabelField("Tile Size:", map.tileSize.x + "x" + map.tileSize.y);

            map.tilePadding = EditorGUILayout.Vector2Field("Tile Padding", map.tilePadding);

            EditorGUILayout.LabelField("Grid Size In Units:", map.gridSize.x + "x" + map.gridSize.y);
            EditorGUILayout.LabelField("Pixels To Units:", map.pixelsToUnits.ToString());
            UpdateBrush(map.currentTileBrush);

            if(GUILayout.Button("Clear Tiles"))
            {
                if (EditorUtility.DisplayDialog("Clear map's tiles?", "Are you sure?", "Clear", "Cancel"))
                {
                    ClearMap();
                }
            }
        }

        EditorGUILayout.EndVertical();
    }

    void OnEnable()
    {
        map = target as TileMap;
        Tools.current = Tool.View;

        if(map.tiles == null)
        {
            var go = new GameObject();
            go.transform.SetParent(map.transform);
            go.transform.position = Vector3.zero;
            go.name = "Layer_" + map.layerDepth;

            map.tiles = go;
        }

        if(map.texture2D != null)
        {
            UpdateCalculations();
            NewBrush();
        }
    }

    void OnDisable()
    {
        DestroyBrush();
    }

    void OnSceneGUI()
    {
        if(brush != null)
        {
            UpdateHitPosition();
            MoveBrush();

            if(map.texture2D != null && mouseOnMap)
            {
                Event current = Event.current;

                if(current.shift)
                {
                    Draw();
                }

                if (current.alt)
                {
                    RemoveTile();
                }
            }
        }
    }

    void UpdateCalculations()
    {
        var path = AssetDatabase.GetAssetPath(map.texture2D);
        map.spriteReferences = AssetDatabase.LoadAllAssetsAtPath(path);

        // Make sure that you don't pick the parent that is of type Texture2D
        // The parent appears to always be on index 0 at the time of writing
        var sprite = (Sprite)map.spriteReferences[1];
        var width = sprite.textureRect.width;
        var height = sprite.textureRect.height;

        map.tileSize = new Vector2(width, height);

        map.pixelsToUnits = (int)(sprite.rect.width / sprite.bounds.size.x);

        map.gridSize = new Vector2((width / map.pixelsToUnits) * map.mapSize.x, (height / map.pixelsToUnits) * map.mapSize.y);
    }

    void UpdateDepth()
    {
        map.tiles.name = "Layer_" + map.layerDepth;

        for (int i = 0; i < map.tiles.transform.childCount; i++)
        {
            var tile = map.tiles.transform.GetChild(i);

            tile.position = new Vector3(tile.position.x, tile.position.y, map.layerDepth);
        }
    }

    void UpdateSolidState()
    {
        for (int i = 0; i < map.tiles.transform.childCount; i++)
        {
            var tile = map.tiles.transform.GetChild(i);

            if (map.isSolid)
            {
                tile.gameObject.layer = LayerMask.NameToLayer("Solid");
                tile.gameObject.AddComponent<BoxCollider2D>();
            }
            else
            {
                tile.gameObject.layer = LayerMask.NameToLayer("Parallax");
                if (tile.gameObject.GetComponent<BoxCollider2D>())
                {
                    DestroyImmediate(tile.gameObject.GetComponent<BoxCollider2D>());
                }
            }
        }
    }

    void CreateBrush()
    {
        var sprite = map.currentTileBrush;

        if(sprite != null)
        {
            GameObject go = new GameObject("Brush");
            go.transform.SetParent(map.transform);

            brush = go.AddComponent<TileBrush>();
            brush.renderer2D = go.AddComponent<SpriteRenderer>();
            brush.renderer2D.sortingOrder = 1000;

            var pixelsToUnits = map.pixelsToUnits;
            brush.brushSize = new Vector2(sprite.textureRect.width / pixelsToUnits, sprite.textureRect.height / pixelsToUnits);

            brush.UpdateBrush(sprite);
        }
    }

    void NewBrush()
    {
        if (map.GetComponentInChildren<TileBrush>())
        {
            DestroyImmediate(map.GetComponentInChildren<TileBrush>().gameObject);
        }

        if(brush == null)
        {
            CreateBrush();
        }
    }

    void DestroyBrush()
    {
        if(brush != null)
        {
            DestroyImmediate(brush.gameObject);
        }
    }

    public void UpdateBrush(Sprite sprite)
    {
        if(brush != null)
        {
            brush.UpdateBrush(sprite);
        }
    }

    void UpdateHitPosition()
    {
        var p = new Plane(map.transform.TransformDirection(Vector3.forward), Vector3.zero);
        var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        var hit = Vector3.zero;
        var dist = 0f;

        if(p.Raycast(ray, out dist))
        {
            hit = ray.origin + ray.direction.normalized * dist;
            mouseHitPos = map.transform.InverseTransformPoint(hit);
        }
    }

    void MoveBrush()
    {
        var tileSize = map.tileSize.x / map.pixelsToUnits;

        var x = Mathf.Floor(mouseHitPos.x / tileSize) * tileSize;
        var y = Mathf.Floor(mouseHitPos.y / tileSize) * tileSize;

        var row = x / tileSize;
        var column = Mathf.Abs(y / tileSize) - 1;

        if (!mouseOnMap)
        {
            return;
        }

        var id = (int)((column * map.mapSize.x) + row);

        brush.tileID = id;

        x += map.transform.position.x + tileSize / 2;
        y += map.transform.position.y + tileSize / 2;

        brush.transform.position = new Vector3(x, y, map.layerDepth);
    }

    void Draw()
    {
        var id = brush.tileID.ToString();

        var posX = brush.transform.position.x;
        var posY = brush.transform.position.y;
        var posZ = map.layerDepth;

        GameObject tile = GameObject.Find(map.name + "/Layer_" + map.layerDepth + "/tile_" + id);
        
        if(tile == null)
        {
            tile = new GameObject("tile_" + id);
            tile.transform.SetParent(map.tiles.transform);
            tile.transform.position = new Vector3(posX, posY, posZ);
            tile.AddComponent<SpriteRenderer>();
        }

        tile.GetComponent<SpriteRenderer>().sprite = brush.renderer2D.sprite;

        if (map.isSolid)
        {
            tile.gameObject.layer = LayerMask.NameToLayer("Solid");

            if (!tile.gameObject.GetComponent<BoxCollider2D>())
            {
                tile.gameObject.AddComponent<BoxCollider2D>();
            }
        }
        else
        {
            tile.gameObject.layer = LayerMask.NameToLayer("Parallax");
        }
    }

    void RemoveTile()
    {
        var id = brush.tileID.ToString();

        GameObject tile = GameObject.Find(map.name + "/Layer_" + map.layerDepth + "/tile_" + id);

        if (tile != null)
        {
            DestroyImmediate(tile);
        }
    }

    void ClearMap()
    {
        for (int i = 0; i < map.tiles.transform.childCount; i++)
        {
            Transform t = map.tiles.transform.GetChild(i);
            DestroyImmediate(t.gameObject);
            i--;
        }
    }
}
