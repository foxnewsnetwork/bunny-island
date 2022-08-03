using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCellMap : MonoBehaviour
{
    public GameObject ghostRoadPrefab;

    private Dictionary<CellType, GameObject> cellGhostPairs;

    private void Start()
    {
        cellGhostPairs = new Dictionary<CellType, GameObject>()
        {
            { CellType.Road, ghostRoadPrefab },
            // TODO: declare others
        };
    }

    public GameObject GetGhost(CellType type)
    {
        return cellGhostPairs[type];
    }
}
