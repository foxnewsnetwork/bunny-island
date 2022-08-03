using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public CameraMovement cameraMovement;
    public InputManager inputManager;
    public RoadBuilder roadBuilder;

    void Start() {
        inputManager.OnLeftMouseDown += BeginRoadBuild;
        inputManager.OnLeftMouseUp += FinalizeRoadBuild;
        inputManager.OnLeftMouseHold += Callback.DebounceWhen<Vector3Int>(AdjustRoadDestination, ArePositionsDifferent);
    }

    private void AdjustRoadDestination(Vector3Int position)
    {
        roadBuilder.AppendGhostRoad(position);
    }

    private void BeginRoadBuild(Vector3Int position)  {
        // TODO: handle clicking off-map
        roadBuilder.BeginBuild(position);
    }

    private bool ArePositionsDifferent(Vector3Int a, Vector3Int b)
    {
        return a !s= b;
    }

    private void FinalizeRoadBuild(Vector3Int position)
    {
        // TODO: handle clicking off-map
        roadBuilder.FinalizeBuild(position);
    }

    // Update is called once per frame
    void Update()
    {   
        Vector3 nextVec = new Vector3(inputManager.CameraMovementVector.x, 0, inputManager.CameraMovementVector.y);
        cameraMovement.MoveCamera(nextVec);        
    }
}
