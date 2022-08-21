using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBuilder : MonoBehaviour
{
    public PlacementManager placementManager;

    public RoadFixer roadFixer;

    private void Start() {
        roadFixer = GetComponents<RoadFixer>()[0];
    }

    internal void AppendGhostRoad(Vector3Int position)
    {
        placementManager.FinishGhostStructure(position);
    }

    public void BeginBuild(Vector3Int position)
    {
        if (placementManager.IsLegal(position, CellType.Road))
        {
            placementManager.StartGhostStructure(position, CellType.Road);
        }
    }

    public void FinalizeBuild(Vector3Int position)
    {
        List<Vector3Int> tempRoadPosition = placementManager.ConvertGhostToRealBuilding();
        FixRoadPrefabs(tempRoadPosition);
    }

    
    private void FixRoadPrefabs(List<Vector3Int> tempRoadPositions) {
        List<Vector3Int> allPointsToCheck = new List<Vector3Int>();
        foreach (var tempPos in tempRoadPositions) {
            allPointsToCheck.Add(tempPos);
            List<Vector3Int> neighbors = placementManager.GetNeighborsOfType(tempPos, CellType.Road);
            allPointsToCheck.AddRange(neighbors);
        }
        foreach  (var posToFix in  allPointsToCheck) {
            roadFixer.FixRoadAtPosition(placementManager, posToFix);
        }
    }

}
