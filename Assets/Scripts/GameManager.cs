using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: GameManager really should be promoted to
// GameDirector, who in turn is responsible for coordinating
// the behavior of his staff and switching modes between them
public class GameManager : MonoBehaviour
{

    public CameraMovement cameraMovement;
    public InputManager inputManager;

    // TODO: underlying staff are:
    // - ConstructionManager: who handles laying ghosts onto the map and
    //   rendering buildings andsuch
    // - MenuManager: who handles clicking on menus and such
    // - QueryManager: basically the "default" guy
    // and potentially  others like a "UnitManager" in the event we care
    // to have microing units as a part of the game
    public RoadBuilder roadBuilder;
    public UIController uiController;

    void Start() {
        uiController.OnRoadPlacement += RoadPlacementHandler;
        uiController.onWallPlacement += WallPlacementHandler;
        uiController.OnHousePlacement += HousePlacementHandler;
        uiController.OnSpecialPlacement += SpecialPlacementHandler;
    }

    private void WallPlacementHandler()
    {
        ClearInputActions();
    }

    private void SpecialPlacementHandler()
    {
        ClearInputActions();
    }

    private void HousePlacementHandler()
    {
        ClearInputActions();
    }

    private void RoadPlacementHandler()
    {
        ClearInputActions();
        inputManager.OnLeftMouseDown += BeginRoadBuild;
        inputManager.OnLeftMouseUp += FinalizeRoadBuild;
        inputManager.OnLeftMouseHold += Callback.DebounceWhen<Vector3Int>(AdjustRoadDestination, ArePositionsDifferent);
    }



    private void ClearInputActions()
    {
        inputManager.OnLeftMouseDown = null;
        inputManager.OnLeftMouseUp = null;
        inputManager.OnLeftMouseHold = null;
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
        return a != b;
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
