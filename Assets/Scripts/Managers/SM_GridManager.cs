using System.Collections.Generic;
using Grid;
using Systems;
using Systems.EventHub;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class SM_GridManager : MonoBehaviour, ISM_ManagerBase
    {
        [Inject] private SM_ManagerEventHub _eventHub;
        public Vector2 cellSize = Vector2.one;
        public Vector2Int gridSize = new Vector2Int(100, 20);
        public Vector3 origin = Vector3.zero;

        private Dictionary<Vector2Int, SM_GridCell> _gridMap = new();
        
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<Signal_InitializeManagers>(x => InitManager(x.EventHub));
            SM_GameManager.Instance.RegisterManager(ESM_Manager.GridManager, this);
        }

        public void InitManager(SM_ManagerEventHub eventHub)
        {
            _eventHub = eventHub;
            
            _gridMap.Clear();
            for (int x = 0; x < gridSize.x; ++x)
            {
                for (int y = 0; y < gridSize.y; ++y)
                {
                    var pos = new Vector2Int(x, y);
                    _gridMap[pos] = new SM_GridCell(pos);
                }
            }
        }

        public void DestroyManager()
        {
        }

        public Vector2Int WorldToGrid(Vector3 worldPos)
        {
            Vector3 localPos = worldPos - origin;
            int x = Mathf.FloorToInt(localPos.x / cellSize.x);
            int y = Mathf.FloorToInt(localPos.y / cellSize.y);
            return new Vector2Int(x, y);
        }

        public Vector3 GridToWorld(Vector2Int gridPos)
        {
            return origin + new Vector3(gridPos.x * cellSize.x, gridPos.y * cellSize.y, 0f);
        }

        public SM_GridCell GetCell(Vector2Int pos)
        {
            return _gridMap.GetValueOrDefault(pos);
        }
    }
}