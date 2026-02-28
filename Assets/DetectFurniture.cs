using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectFurniture : MonoBehaviour
{
    public GameObject furnitureDetected;
    private void OnTriggerStay(Collider other) {
        if (other.GetComponent<Collider>().CompareTag("Furniture"))
        {
            furnitureDetected = other.gameObject;
            Debug.Log("Detected furniture " + furnitureDetected.name);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<Collider>().CompareTag("Furniture"))
        {
            furnitureDetected = null;
        }
    }
}
