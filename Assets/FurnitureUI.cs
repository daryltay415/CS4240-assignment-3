using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureUI : MonoBehaviour
{
    public ARTapToPlace tapToPlace;
    public GameObject furniturePanel;

    public void SelectFurniture(string baseName)
    {
        tapToPlace.ApplySelection(baseName);
        if (furniturePanel != null) furniturePanel.SetActive(false);
    }
}