using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    private Grid grid;
    public int height, width;
    public RoadBuilder roadBuilder;

    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid(width, height);
    }

    public bool IsLegal(Vector3Int position, CellType cellType) {
        return IsInBounds(position) && IsEmpty(position);
    }

    public bool IsInBounds(Vector3Int position) {
        return 0 <= position.x && position.x <= width &&
            0 <= position.z && position.z <= height;
    }

    public bool IsEmpty(Vector3Int position) {
        return grid[position.x, position.z] == CellType.Empty;
    }

    public void Build(Vector3Int position, CellType cell) {
        grid[position.x, position.z] = cell;
        if (cell == CellType.Road) {
            roadBuilder.Build(position);
        }
    }
}
