using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderScript : MonoBehaviour
{
    // Start is called before the first frame update
    static public bool wasInterfaceClicked = false;
    static public bool isBuildingMode = false;
    static public GameObject buildingObject = null;

    // Update is called once per frame

    public void setBuildingObject(GameObject go)
    {
        buildingObject = go;
        isBuildingMode = true;
        wasInterfaceClicked = true;
    }
}
