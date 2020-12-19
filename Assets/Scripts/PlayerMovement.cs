using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed;
    public float ZoomStrenght;
    public float MinCameraHeight;
    public float ZoomStep;

    private Vector2 _moveDirection;

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
        Move(_moveDirection);
    }

    public void OnMouseZoom(InputAction.CallbackContext context)
    {
        float zoomDistance = context.ReadValue<float>();
        Zoom(zoomDistance * ZoomStrenght);
    }

    public void OnKeyboardZoom(InputAction.CallbackContext context)
    {
        var zoomModifier = context.ReadValue<float>();
        Zoom(zoomModifier * ZoomStep);
    }
    
    private void Move(Vector2 direction)
    {
        float scaledMoveSpeed = MoveSpeed * Time.deltaTime;
        
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
        transform.Translate(moveDirection * scaledMoveSpeed);
    }

    private void Zoom(float distance)
    {
        Vector3 zoomVector = new Vector3(0, distance, 0);
        
        var nextCameraPos = transform.position + zoomVector;
        if (nextCameraPos.y > MinCameraHeight || distance > 0)
            transform.position = nextCameraPos;
        else
            transform.position = new Vector3(transform.position.x, MinCameraHeight,transform.position.z);
    }

    private void Update()
    {
        Move(_moveDirection);
    }
    
}
