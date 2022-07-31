using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Action<Vector3Int> OnLeftMouseDown, OnLeftMouseHold;
    public Action OnLeftMouseUp;

    [SerializeField]
    Camera mainCamera;

    public LayerMask groundMask;

    private Vector2 cameraMovementVector;
    public Vector2 CameraMovementVector {
        get { return cameraMovementVector; }
    }

    private Vector3Int? RaycastGround() {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Boolean isCollide = Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask);
        if (isCollide) {
            Vector3Int hitPosition = Vector3Int.RoundToInt(hit.point);
            return hitPosition;
        }
        return null;
    }

    private void Update() {
        CheckDirectionInput();
        CheckLeftMouseButtonUp();
        CheckLeftMouseButtonHold();
        CheckLeftMouseButtonDown();
    }

    private void CheckDirectionInput() {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        cameraMovementVector = new Vector2(hor, ver);
    }

    private void CheckLeftMouseButtonHold() {
        Boolean isLeftMouseButtonDown = Input.GetMouseButton(0);

        if (isLeftMouseButtonDown) {
            Vector3Int? hitPosition = RaycastGround();
            if (hitPosition is Vector3Int pos) {
                OnLeftMouseHold?.Invoke(pos);
            }
        }
    }

    private void CheckLeftMouseButtonUp() {
        Boolean isLeftMouseUp = Input.GetMouseButtonUp(0);
        if (isLeftMouseUp) {
            OnLeftMouseUp?.Invoke();
        }
    }

    private void CheckLeftMouseButtonDown() {
        Boolean isLeftMouseButtonDown = Input.GetMouseButtonDown(0);
        if (isLeftMouseButtonDown) {
            Vector3Int? hitPosition = RaycastGround();
            if (hitPosition is Vector3Int pos) {
                OnLeftMouseDown?.Invoke(pos);
            }
        }
    }
}
