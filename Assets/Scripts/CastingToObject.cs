using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CastingToObject : MonoBehaviour
{
    public static string so;
    private GameObject selectedObject;
    public int red;
    public int green;
    public int blue;
    public int range = 1000;
    public int interfaceHeight = 200;

    private int currentX;
    private int currentY;
    Color color;
    // Update is called once per frame
    void Update()
    {
        if (BuilderScript.isBuildingMode)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, range))
                {
                    var mapToObject = FindObjectOfType<MapToObjects>();
                    GameObject selectedObject = GameObject.Find(hit.transform.gameObject.name);
                    if (currentX != selectedObject.GetComponent<SizeScript>().x || currentY != selectedObject.GetComponent<SizeScript>().y)
                    {
                        currentX = selectedObject.GetComponent<SizeScript>().x;
                        currentY = selectedObject.GetComponent<SizeScript>().y;
                        mapToObject.paintTempObject(currentX, currentY, BuilderScript.buildingObject);
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (mapToObject.isGreen)
                        {
                            BuilderScript.isBuildingMode = false;
                            mapToObject.constructPaintedObject(currentX, currentY, BuilderScript.buildingObject);

                        }
                    }
                }
            }

        }
        else
        if (Input.GetMouseButtonDown(0))
        {
            clearSelected();

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                BuilderScript.isBuildingMode = false;

                if (Physics.Raycast(ray, out hit, range))
                {
                    if (BuilderScript.isBuildingMode)
                    {

                    }
                    else
                    {
                        markClickedObject(ref hit);
                    }

                }
            }
        }
    }
    private void markClickedObject(ref RaycastHit hit)
    {
        clearSelected();
        so = hit.transform.gameObject.name;
        selectedObject = GameObject.Find(CastingToObject.so);

        color = selectedObject.GetComponent<Renderer>().material.color;
        selectedObject.GetComponent<Renderer>().material.color = new Color32((byte)red, (byte)green, (byte)blue, 255);
        Debug.Log("You selected the " + so); // ensure you picked right object
        Debug.Log("Size " + selectedObject.GetComponent<SizeScript>().sizeX); // ensure you picked right object

        Debug.Log("X: " + hit.transform.gameObject.transform.position.x + ", Y:" + hit.transform.gameObject.transform.position.y + ", Z: " + hit.transform.gameObject.transform.position.z); // ensure you picked right object

    }
    private void clearSelected()
    {
        if (selectedObject != null)
        {
            selectedObject.GetComponent<Renderer>().material.color = color;
        }
    }
}
