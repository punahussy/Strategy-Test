﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed;
    public float ZoomStrenght;
    public float MinCameraHeight;

    private Vector2 _moveDirection;

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
        Move(_moveDirection);
    }

    public void OnZoom(InputAction.CallbackContext context)
    {
        float zoomDistance = context.ReadValue<float>();
        Zoom(zoomDistance);
    }
    
    private void Move(Vector2 direction)
    {
        float scaledMoveSpeed = MoveSpeed * Time.deltaTime;
        
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
        transform.position += moveDirection * scaledMoveSpeed;
    }

    private void Zoom(float distance)
    {
        float finalZoomAmount = distance * ZoomStrenght;
        Vector3 zoomVector = new Vector3(0, finalZoomAmount, 0);
        
        var nextCameraPos = transform.position + zoomVector;
        if (nextCameraPos.y > MinCameraHeight || finalZoomAmount > 0)
            transform.position += zoomVector;
        else
            transform.position = new Vector3(transform.position.x, MinCameraHeight,transform.position.z);
    }

    private void Update()
    {
        Move(_moveDirection);
    }
    
}
