using System;
using Managers;
using UnityEngine;

namespace Systems.Visualizer
{
    public class SM_GridVisualizer : MonoBehaviour
    {
        public GameObject tilePrefab;
        public Transform tileParent;

        public void GenerateTiles()
        {
            SM_GridManager gridManager = (SM_GridManager)SM_GameManager.Instance.GetManager(ESM_Manager.GridManager);
            Vector2Int gridSize = gridManager.gridSize;
            for (int x = 0; x < gridSize.x; ++x)
            {
                for (int y = 0; y < gridSize.y; ++y)
                {
                    Vector2Int gridPos = new Vector2Int(x, y);
                    Vector3 worldPos = gridManager.GridToWorld(gridPos);
                    GameObject tile = Instantiate(tilePrefab, worldPos, Quaternion.identity, tileParent);
                    tile.name = $"Tile_{x}_{y}";
                }
            }
        }
    }
}