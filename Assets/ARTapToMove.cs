using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class ARTapToMove : MonoBehaviour
{
    public GameObject deleteButton;
    private GameObject objectToPlace;
    public FurnitureInit furnitureInit;
    private FurnitureCol furnitureCol;
    private GameObject ghostObject;
    public GameObject movementIndicator;
    private Pose placementPose;
    private ARRaycastManager arraycastmanager;
    private bool placementPoseIsValid = false;
    private bool movingFurniture = false;
    public DetectFurniture detectFurniture;
    private string furnitureName;

    // Start is called before the first frame update
    void Start()
    {
        arraycastmanager = FindObjectOfType<ARRaycastManager>();
        //test
        
    }

    private void PlaceObject()
    {
        objectToPlace = furnitureInit.furnitureReal[furnitureName];
        Debug.Log("Object name: " + objectToPlace.name);
        Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
        DeleteFurniture();
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit furnitureHit;
        arraycastmanager.Raycast(screenCenter, hits, TrackableType.Planes);
        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
        }
    }

    private void UpdateMovementIndicator()
    {
        if (placementPoseIsValid)
        {
            movementIndicator.SetActive(true);
            movementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            
        }
        else
        {
            movementIndicator.SetActive(false);
        }
    }

    private void ShowMovePreview()
    {
        
        if (placementPoseIsValid && movingFurniture)
        {
            ghostObject.SetActive(true);
            ghostObject.transform.SetPositionAndRotation(placementPose.position,placementPose.rotation);
            deleteButton.SetActive(true);
        }
        else
        {
            ghostObject.SetActive(false);
        }
    }

    public void SwapGhost(string ghostName)
    {
        if(ghostObject != null)
        {
            ghostObject.SetActive(false);
        }
        ghostObject = furnitureInit.furnitureGhosts[ghostName + "1"];
        furnitureCol = ghostObject.GetComponent<FurnitureCol>();
        furnitureCol.canPlace = true;
    }

    private void MoveFurniture()
    {
        if(detectFurniture.furnitureDetected != null)
        {
            furnitureName = detectFurniture.furnitureDetected.name.Replace("(Clone)", "");
            Debug.Log("name: "+furnitureName);
            SwapGhost(furnitureName);
            movingFurniture = true;
            Destroy(detectFurniture.furnitureDetected);
        }
        else
        {
            Debug.Log("No furniture detected");
        }
    }

    public void SetGhostInactive()
    {
        if (movingFurniture)
        {
            ghostObject.SetActive(false);
            deleteButton.SetActive(false);
        }
    }

    public void SetGhostActive()
    {
        if (movingFurniture)
        {
            ghostObject.SetActive(true);
            deleteButton.SetActive(true);
        }
    }
    

    public void DeleteFurniture()
    {
        movingFurniture = false;
        deleteButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdateMovementIndicator();
        if(ghostObject != null)
        {
            ShowMovePreview();
        }
        if(placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            if (movingFurniture && furnitureCol.canPlace)
            {
                PlaceObject();
            }
            else
            {
                MoveFurniture();
            }
            //helo
        }
        
    }
}
