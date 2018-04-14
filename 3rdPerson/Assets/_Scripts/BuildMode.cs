﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMode : MonoBehaviour {

    public Transform normalSquare;
    public Transform transparentSquare;
    public Transform map;
    public Grid grid;
    public float maxRay;
    public float gridSize;

    private Vector3 position;
    private bool isBuildModeActive;
    private Transform square;
    private Vector3 testPoint;

    protected vThirdPersonCamera tpCamera;
    // Use this for initialization
    void Start () {
        isBuildModeActive = false;
        tpCamera = FindObjectOfType<vThirdPersonCamera>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            isBuildModeActive = true;
        }
        if (isBuildModeActive)
        {
            RaycastHit hit;
            Ray ray = new Ray(tpCamera.transform.position, tpCamera.transform.forward);
            if (Physics.Raycast(ray, out hit, maxRay))
            {
                Vector3 finalPos;
                if (hit.transform.tag.Equals("BuildSquare"))
                {
                    finalPos = AlignAndGenerateSquare(hit.point);
                }
                else
                {
                     finalPos = AlignAndGenerateSquare(hit.point);
                    Debug.Log("hit: " + hit.point);
                }
                if(finalPos != position)
                {
                    if (square != null)
                    {
                        Destroy(square.gameObject);
                    }
                    position = finalPos;
                    square = Instantiate(transparentSquare, finalPos, Quaternion.identity);
                }
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Instantiate(normalSquare, position, Quaternion.identity);
            }
        }      
	}

    Vector3 AlignAndGenerateSquare(Vector3 hitPoint)
    {

        float snapPointX = hitPoint.x + ((gridSize - hitPoint.x) % gridSize);
        float snapPointY = hitPoint.y + 0.05f;
        float snapPointZ = hitPoint.z + ((gridSize - hitPoint.z) % gridSize);

        Vector3 gridPosition = new Vector3(snapPointX, snapPointY, snapPointZ);
        Debug.Log("grid position:" + gridPosition);
        return gridPosition;
            
        
    }

    Vector3Int GetNearestCell(Vector3 hitPoint)
    {
        int xPoint = (int)hitPoint.x +2 ;
        int yPoint = (int)(hitPoint.y);
        int zPoint = (int)hitPoint.z;

        Vector3Int cellPoint = new Vector3Int(xPoint, yPoint, zPoint);
        Vector3 cellCenterWorldPoint = grid.GetCellCenterWorld(cellPoint);
        Vector3Int finalPosition = grid.WorldToCell(cellCenterWorldPoint);
        return finalPosition;
    }
}
