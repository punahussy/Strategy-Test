using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowPointer : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject mousePointer;

    public Vector3 Point => SnapPosition(GetWorldPoint());

    public void TrackMouse()
    {
        mousePointer.transform.position = Point;
    }

    public void TogglePointer(bool enable)
    {
        mousePointer.SetActive(enable);
    }

    private Vector3 GetWorldPoint()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = _camera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out var hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    private Vector3 SnapPosition(Vector3 original)
    {
        Vector3 snapped;
        snapped.x = Mathf.Floor(original.x + 0.5f);
        snapped.y = Mathf.Floor(original.y + 0.5f);
        snapped.z = Mathf.Floor(original.z + 0.5f);
        return snapped;

    }
}
