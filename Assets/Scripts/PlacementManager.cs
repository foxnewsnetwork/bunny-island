using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    private Grid grid;
    public int height, width;
    public BuildingCellMap buildingCell;

    private Dictionary<Vector3Int, StructureModel> tempRoadObjects = new Dictionary<Vector3Int,  StructureModel>();
    private CellType? currentGhostType;
    private Dictionary<Vector3Int, GameObject> tempGhostObjects = new Dictionary<Vector3Int, GameObject>();
    private Vector3Int ghostStartPosition;


    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid(width, height);
    }

    internal CellType[] GetNeighborTypesAtPosition(Vector3Int pos) {
        return grid.GetAllAdjacentCellTypes(pos.x, pos.z);
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

    public void StartGhostStructure(Vector3Int position, CellType type)
    {
        Debug.Log("StartGhostStructure");
        ClearGhosts();
        Debug.Log("StartGhostStructure:KillGhost");
        if (IsLegal(position, type))
        {
            currentGhostType = type;
            GameObject ghostPrefab = buildingCell.GetGhost(type);
            GameObject ghostClone = Instantiate(ghostPrefab, position, Quaternion.identity);
            ghostStartPosition = position;
            tempGhostObjects.Add(position, ghostClone);
        }
    }

    private void ClearGhosts()
    {
        foreach (KeyValuePair<Vector3Int, GameObject> kvp in tempGhostObjects)
        {
            DestroyImmediate(kvp.Value);
        }
        tempGhostObjects.Clear();
    }

    public void FinishGhostStructure(Vector3Int position)
    {
        Debug.Log("FinishGhostStructure");
        ClearGhosts();
        Debug.Log("FinishGhostStructure:KillGhost");
        if (currentGhostType is CellType type && IsLegal(position, type))
        {
            Point startPoint = new Point(ghostStartPosition.x, ghostStartPosition.z);
            Point finishPoint = new Point(position.x, position.z);
            List<Point> path = GridSearch.AStarSearch(grid, startPoint, finishPoint);
            GameObject ghostPrefab = buildingCell.GetGhost(type);
            path.ForEach(x =>
            {
                Vector3Int vec = PointToVector(x);
                GameObject ghostClone = Instantiate(ghostPrefab, vec, Quaternion.identity);
                tempGhostObjects.Add(vec, ghostClone);
            });
        }
    }

    public List<Vector3Int> ConvertGhostToRealBuilding()
    {
        List<Vector3Int> builtRoadPositions = new List<Vector3Int>();
        if (currentGhostType is CellType type) {
            foreach (KeyValuePair<Vector3Int, GameObject> kvp in tempGhostObjects)
            {
                if (IsLegal(kvp.Key, type))
                {
                    PlaceTemporaryStructure(kvp.Key, kvp.Value, type);
                }
                builtRoadPositions.Add(kvp.Key);
            }
            ClearGhosts();
        }
        return builtRoadPositions;
    }

    private Vector3Int PointToVector(Point obj)
    {
        return new Vector3Int(obj.X, 0, obj.Y);
    }

    internal void PlaceTemporaryStructure(Vector3Int position, GameObject structurePrefab, CellType type) {
        grid[position.x, position.z] = type;
        StructureModel structure = CreateNewStructureModel(position, structurePrefab, type);
        tempRoadObjects.Add(position, structure);
    }

    private StructureModel CreateNewStructureModel(Vector3Int position, GameObject prefab, CellType type) {
        GameObject structure = new GameObject(type.ToString());
        structure.transform.SetParent(transform);
        structure.transform.localPosition = position;
        var structureModel = structure.AddComponent<StructureModel>();
        structureModel.CreateModel(prefab);
        return structureModel;
    }

    public void ModifyStructureModel(Vector3Int position, GameObject newModel, Quaternion rotation) {
        if  (tempRoadObjects.ContainsKey(position)) {
            tempRoadObjects[position].SwapModel(newModel, rotation);
        }
    }
}
