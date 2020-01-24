using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderScript : MonoBehaviour
{
    // Start is called before the first frame update
    static public bool wasInterfaceClicked = false;
    static public bool isBuildingMode = false;
    static public GameObject buildingObject = null;
    static public bool activeSelling = false;
    public GameObject g;

    // Update is called once per frame

    public void setBuildingObject(GameObject go)
    {
        buildingObject = go;
        isBuildingMode = true;
        wasInterfaceClicked = true;
        activeSelling = false;
        //g.GetComponent<IsActiveDolarScript>().toggle(false);

    }
    public void cancelBuildingObject()
    {
        buildingObject = null;
        isBuildingMode = false;
        wasInterfaceClicked = false;
    }

    public void setActiveSelling()
    {
        cancelBuildingObject();
        activeSelling = true;
        //g.GetComponent<IsActiveDolarScript>().toggle(true);

    }
}
