using UnityEngine;

namespace Grid
{
    public class SM_GridCell
    {
        public Vector2Int GridPosition { get; private set; }
        public bool IsBlocked { get; set; }
        public GameObject OccupiedObject { get; set; }

        public SM_GridCell(Vector2Int pos)
        {
            GridPosition = pos;
            IsBlocked = false;
        }
    }
}