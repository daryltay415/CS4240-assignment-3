using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureInit : MonoBehaviour
{
    public GameObject[] furnitureGhostList;
    public GameObject[] furnitureRealList;
    public Dictionary<string, GameObject> furnitureGhosts = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> furnitureReal = new Dictionary<string, GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        if (furnitureGhostList != null)
        {
            foreach (var item in furnitureGhostList)
            {
                furnitureGhosts.Add(item.name,item);
            }
        }

        if (furnitureRealList != null)
        {
            foreach(var item in furnitureRealList)
            {
                furnitureReal.Add(item.name,item);
            }
        }
    }
}
