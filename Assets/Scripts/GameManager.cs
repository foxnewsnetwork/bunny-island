using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public CameraMovement cameraMovement;
    public InputManager inputManager;
    public PlacementManager placementManager;

    void Start() {
        inputManager.OnLeftMouseDown += HandleMouseClick;
    }

    private void HandleMouseClick(Vector3Int position)  {
        if (placementManager.IsLegal(position, CellType.Road)) {
            placementManager.Build(position, CellType.Road);
        }
    }

    // Update is called once per frame
    void Update()
    {   
        Vector3 nextVec = new Vector3(inputManager.CameraMovementVector.x, 0, inputManager.CameraMovementVector.y);
        cameraMovement.MoveCamera(nextVec);        
    }
}
