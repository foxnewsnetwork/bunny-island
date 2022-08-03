using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Action<Vector3Int> OnLeftMouseDown, OnLeftMouseHold, OnLeftMouseUp;
    private bool isLeftMouseButtonDown;

    public bool IsLeftMouseButtonDown
    {
        get { return isLeftMouseButtonDown; }
    }

    public bool IsLeftMouseButtonUp
    {
        get { return !isLeftMouseButtonDown; }
    }

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
        bool isCollide = Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask);
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
        isLeftMouseButtonDown = Input.GetMouseButton(0);

        if (isLeftMouseButtonDown) {
            Vector3Int? hitPosition = RaycastGround();
            if (hitPosition is Vector3Int pos) {
                OnLeftMouseHold?.Invoke(pos);
            }
        }
    }

    private void CheckLeftMouseButtonUp() {
        bool isLeftMouseButtonUp = Input.GetMouseButtonUp(0);
        isLeftMouseButtonDown = !isLeftMouseButtonUp;
        if (isLeftMouseButtonUp) {
            Vector3Int? hitPosition = RaycastGround();
            if (hitPosition is Vector3Int pos)
            {
                OnLeftMouseUp?.Invoke(pos);
            }
        }
    }

    private void CheckLeftMouseButtonDown() {
        isLeftMouseButtonDown = Input.GetMouseButtonDown(0);
        if (isLeftMouseButtonDown) {
            Vector3Int? hitPosition = RaycastGround();
            if (hitPosition is Vector3Int pos) {
                OnLeftMouseDown?.Invoke(pos);
            }
        }
    }
}
