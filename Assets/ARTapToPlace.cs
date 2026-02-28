using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class ARTapToPlace : MonoBehaviour
{
    public GameObject objectToPlace;
    public FurnitureInit furnitureInit;
    private FurnitureCol furnitureCol;
    private GameObject ghostObject;
    public GameObject placementIndicator;
    public GameObject movementIndicator;
    private GameObject lastGhostObjForPlacingMode;
    public string ghost;
    private Pose placementPose;
    private ARRaycastManager arraycastmanager;
    private bool placementPoseIsValid = false;
    private bool isMoveModeOn = false;
    private bool movingFurniture = false;
    private DetectFurniture detectFurniture;

    // Start is called before the first frame update
    void Start()
    {
        arraycastmanager = FindObjectOfType<ARRaycastManager>();
        ghostObject = furnitureInit.furnitureGhosts["Testrack1"];
        objectToPlace = furnitureInit.furnitureReal["Testrack"];
        furnitureCol = ghostObject.GetComponent<FurnitureCol>();
        detectFurniture = movementIndicator.GetComponent<DetectFurniture>();
        //test
        
    }

    private void PlaceObject()
    {
        Debug.Log("Object name: " + objectToPlace.name);
        Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
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

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    //private void UpdateMovementIndicator()
    //{
    //    if (placementPoseIsValid)
    //    {
    //        movementIndicator.SetActive(true);
    //        movementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
    //        
    //    }
    //    else
    //    {
    //        movementIndicator.SetActive(false);
    //    }
    //}

    public void setGhostInactive()
    {
        ghostObject.SetActive(false);
    }

    private void ShowPreview()
    {
        
        if (placementPoseIsValid)
        {
            ghostObject.SetActive(true);
            ghostObject.transform.SetPositionAndRotation(placementPose.position,placementPose.rotation);
        }
        else
        {
            ghostObject.SetActive(false);
        }
    }

    //private void ShowMovePreview()
    //{
    //    if (placementPoseIsValid && movingFurniture)
    //    {
    //        ghostObject.SetActive(true);
    //        ghostObject.transform.SetPositionAndRotation(placementPose.position,placementPose.rotation);
    //    }
    //    else
    //    {
    //        ghostObject.SetActive(false);
    //    }
    //}

    public void SwapGhost(string ghostName)
    {
        ghostObject.SetActive(false);
        ghostObject = furnitureInit.furnitureGhosts[ghostName + "1"];
        furnitureCol = ghostObject.GetComponent<FurnitureCol>();
    }

    //public void ChangeToMoveMode()
    //{
    //    lastGhostObjForPlacingMode = ghostObject;
    //    isMoveModeOn = true;
    //    movingFurniture = false;
    //}
//
    //public void ChangeToPlaceMode()
    //{
    //    ghostObject.SetActive(false);
    //    ghostObject = lastGhostObjForPlacingMode;
    //    furnitureCol = ghostObject.GetComponent<FurnitureCol>();
    //    isMoveModeOn = false;
    //}

    //private void MoveFurniture()
    //{
    //    if(detectFurniture.furnitureDetected != null)
    //    {
    //        string name = detectFurniture.furnitureDetected.name.Replace("(Clone)", "");
    //        SwapGhost(name);
    //        movingFurniture = true;
    //        Destroy(detectFurniture.furnitureDetected);
    //    }
    //    else
    //    {
    //        Debug.Log("No furniture detected");
    //    }
    //}
//
    //public void DeleteFurniture()
    //{
    //    movingFurniture = false;
    //}

    // Update is called once per frame
    void Update()
    {
        //if(isMoveModeOn == false)
        //{
        //    movementIndicator.SetActive(false);
        //    UpdatePlacementPose();
        //    UpdatePlacementIndicator();
        //    ShowPreview();
        //    
        //    Debug.Log(furnitureCol.canPlace);
        //    if(placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && furnitureCol.canPlace)
        //    {
        //        PlaceObject();
        //    }
        //}
        //else
        //{
        //    placementIndicator.SetActive(false);
        //    ghostObject.SetActive(false);
        //    UpdatePlacementPose();
        //    UpdateMovementIndicator();
        //    ShowMovePreview();
        //    if(placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //    {
        //        if (movingFurniture && furnitureCol.canPlace)
        //        {
        //            PlaceObject();
        //            movingFurniture = false;
        //        }
        //        else
        //        {
        //            MoveFurniture();
        //        }
        //    }
        //}
        movementIndicator.SetActive(false);
        UpdatePlacementPose();
        UpdatePlacementIndicator();
        ShowPreview();
        
        Debug.Log(furnitureCol.canPlace);
        if(placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && furnitureCol.canPlace && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            PlaceObject();
        }
        
    }
}
