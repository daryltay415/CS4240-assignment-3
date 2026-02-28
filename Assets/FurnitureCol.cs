using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureCol : MonoBehaviour
{
    public Color redColor;
    public Color ghostColor;
    public bool canPlace = true;
    public Renderer[] renderer;
    
    private void OnEnable() {
        canPlace = true;
        foreach(var rend in renderer)
        {
            rend.material.color = ghostColor;
        }
    }

    private void OnTriggerStay(Collider other) {
        Debug.Log("change to red");
        if (other.GetComponent<Collider>().CompareTag("Furniture"))
        {
            foreach(var rend in renderer)
            {
                rend.material.color = redColor;
            }
            canPlace = false;
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Furniture"))
        {
            foreach(var rend in renderer)
            {
                rend.material.color = ghostColor;
            }
            canPlace = true;
        }
    }
}
