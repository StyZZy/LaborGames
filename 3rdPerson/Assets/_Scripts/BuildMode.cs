using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMode : MonoBehaviour {

    public Transform normalSquare;
    public Transform transparentSquare;
    public Transform map;
    public Grid grid;

    private Vector3Int position;
    private bool isBuildModeActive;
    private Transform square;
    private Vector3 testPoint;
	// Use this for initialization
	void Start () {
        isBuildModeActive = false;
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Vector3Int finalPos;
                if (hit.transform.tag.Equals("BuildSquare"))
                {
                    finalPos = GetNearestCell(hit.point);
                }
                else
                {
                     finalPos = AlignAndGenerateSquare(hit.point);
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

    Vector3Int AlignAndGenerateSquare(Vector3 hitPoint)
    {
        Vector3Int hitP = new Vector3Int((int) hitPoint.x, (int) hitPoint.y, (int) hitPoint.z);        
        Vector3 cellPoint = grid.GetCellCenterWorld(hitP);
        Vector3Int finalPosition = grid.WorldToCell(cellPoint);

            
            
        return finalPosition;
    }

    Vector3Int GetNearestCell(Vector3 hitPoint)
    {
        int xPoint = (int)(hitPoint.x + 0.5);
        int yPoint = (int)(hitPoint.y + 0.5);
        int zPoint = (int)(hitPoint.z + 0.5);

        Vector3Int cellPoint = new Vector3Int(xPoint, yPoint, zPoint);
        Vector3 cellCenterWorldPoint = grid.GetCellCenterWorld(cellPoint);
        Vector3Int finalPosition = grid.WorldToCell(cellCenterWorldPoint);
        return finalPosition;
    }
}
