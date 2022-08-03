using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadFixer : MonoBehaviour
{
    public GameObject roadEnd, roadStraight, roadCorner, roadThreeWay, roadFourWay;

    public void FixRoadAtPosition(PlacementManager placementManager, Vector3Int pos) {
        // [left, top, right, down]
        var neighborTypes = placementManager.GetNeighborTypesAtPosition(pos);
        int roadCount = neighborTypes.Where(t => t == CellType.Road).Count();

        switch (roadCount) {
            case 0:
            case 1:
                CreateRoadEnd(placementManager, neighborTypes, pos);
                break;
            case 2:
                CreateRoadTwoWay(placementManager, neighborTypes, pos);
                break;
            case 3:
                CreateRoadThreeWay(placementManager, neighborTypes, pos);
                break;
            case 4:
                CreateRoadFourWay(placementManager, neighborTypes, pos);
                break;
        }   
    }

    private void CreateRoadEnd(PlacementManager placementManager, CellType[] neighborTypes, Vector3Int pos) {
       // Default 0 case
       placementManager.ModifyStructureModel(pos, roadEnd, Quaternion.identity);

       for (int checkStart=2;  checkStart<=5; checkStart++) {
            if (isRoadType(neighborTypes[checkStart%4])) {
                placementManager.ModifyStructureModel(pos, roadEnd, Quaternion.Euler(0, 90*(checkStart-2), 0));
                break;
            }
        }
    }

    private void CreateRoadTwoWay(PlacementManager placementManager, CellType[] neighborTypes, Vector3Int pos) {
        // Straight checks
        if (isRoadType(neighborTypes[0]) && isRoadType(neighborTypes[2])) {
            placementManager.ModifyStructureModel(pos, roadStraight, Quaternion.identity);
        } else if (isRoadType(neighborTypes[1]) && isRoadType(neighborTypes[3])) {
            placementManager.ModifyStructureModel(pos, roadStraight, Quaternion.Euler(0, 90, 0));
        } else {
            // Corner checks
            for (int checkStart=0; checkStart<=3; checkStart++) {
                if (isRoadType(neighborTypes[checkStart % 4]) &&
                    isRoadType(neighborTypes[(checkStart + 1) % 4])) {
                    placementManager.ModifyStructureModel(pos, roadCorner, Quaternion.Euler(0, 90 * (checkStart), 0));
                    break;
                }
            }
        }        
    }

    private void CreateRoadThreeWay(PlacementManager placementManager, CellType[] neighborTypes, Vector3Int pos) {
        // Check consecutive tuples
        for (int checkStart=1;  checkStart<=4; checkStart++) {
            if (isRoadType(neighborTypes[checkStart%4]) && 
                isRoadType(neighborTypes[(checkStart+1)%4]) &&
                isRoadType(neighborTypes[(checkStart+2)%4])) {
                placementManager.ModifyStructureModel(pos, roadThreeWay, Quaternion.Euler(0, 90*(checkStart-1), 0));
                break;
            }
        }
    }

    private void CreateRoadFourWay(PlacementManager placementManager, CellType[] neighborTypes, Vector3Int pos) {
        placementManager.ModifyStructureModel(pos, roadFourWay, Quaternion.identity);
    }

    private bool isRoadType(CellType type) {
        return type == CellType.Road;
    }

}
