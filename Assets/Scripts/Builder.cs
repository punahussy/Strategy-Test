using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class Builder : MonoBehaviour
{
    [SerializeField]
    private ShowPointer mousePointer;

    [SerializeField] 
    private GameObject polePrefab;
    [SerializeField] 
    private GameObject wallPrefab;
    
    private BuilderMode _mode = BuilderMode.None;

    private bool buildingWall = false;
    private bool wallSegmentStarted = false;
    private GameObject lastPole;

    void Update()
    {
        switch (_mode)
        {
            case BuilderMode.Wall:
                mousePointer.TogglePointer(true);
                mousePointer.TrackMouse();
                break;
            case BuilderMode.None:
                mousePointer.TogglePointer(false);
                break;
        }
    }
    
    //Заглушка для тестов, потом будет вызываться через UI
    public void ToggleBuildMode(InputAction.CallbackContext context)
    {
        _mode = _mode == BuilderMode.None ? BuilderMode.Wall : BuilderMode.None;
        buildingWall = false;
        wallSegmentStarted = false;
    }

    public void Build(InputAction.CallbackContext context)
    {
        switch (_mode)
        {
            case BuilderMode.Wall:
                BuildWall();
                break;
        }
    }

    private void BuildWall()
    {
        if (!wallSegmentStarted && !buildingWall)
        {
            buildingWall = true;
            wallSegmentStarted = true;
            StartWall();
        }
        else if (buildingWall)
        {
            updateWall();
        }
        else
        {
            buildingWall = false;
            setWall();
        }
    }

    private void StartWall()
    {
        wallSegmentStarted = true;
        Vector3 startPoint = mousePointer.Point;
        GameObject startPole = Instantiate(polePrefab, startPoint, Quaternion.identity);
        startPole.transform.position = new Vector3(startPoint.x, startPoint.y + 0.3f, startPoint.z);
        lastPole = startPole;
    }
    
    private void setWall()
    {
        wallSegmentStarted = false;
    }

    private void updateWall()
    {
        Vector3 current = mousePointer.Point;
        if (!current.Equals(lastPole.transform.position))
            CreateWallSegment(current);
    }

    private void CreateWallSegment(Vector3 current)
    {
        GameObject newPole = Instantiate(polePrefab, current, Quaternion.identity);
        Vector3 middle = Vector3.Lerp(newPole.transform.position, lastPole.transform.position, 0.5f);
        
        GameObject newWall = Instantiate(wallPrefab, middle, Quaternion.identity);
        
        float wallLength = Vector3.Distance(lastPole.transform.position, newPole.transform.position);
        newWall.transform.localScale = new Vector3(newWall.transform.localScale.x, newWall.transform.localScale.y, wallLength);
        newWall.transform.LookAt(lastPole.transform);
        lastPole = newPole;
    }
}
