using System;
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

    private int rotation = 0;
    private int oldRotation = 0;
    private MapToObjects mapToObjects;
    Color color;
    // Update is called once per frame
    private void Start()
    {
        mapToObjects = FindObjectOfType<MapToObjects>();
    }
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            rotation += 90;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            softReset();
            mapToObjects.softReset();
        }
        if (BuilderScript.isBuildingMode)
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, range))
            {
                GameObject selectedObject = GameObject.Find(hit.transform.gameObject.name);
                if (currentX != selectedObject.GetComponent<SizeScript>().x || currentY != selectedObject.GetComponent<SizeScript>().y || rotation != oldRotation)
                {
                    currentX = selectedObject.GetComponent<SizeScript>().x;
                    currentY = selectedObject.GetComponent<SizeScript>().y;
                    mapToObjects.paintTempObject(currentX, currentY, BuilderScript.buildingObject, rotation);
                    oldRotation = rotation;
                }
                
                if (Input.GetMouseButtonDown(0))
                {
                    if (mapToObjects.isGreen)
                    {
                        mapToObjects.constructPaintedObject(currentX, currentY, rotation);
                        rotation = 0;
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            clearSelected();

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, range))
            {
                markClickedObject(ref hit);
            }
        }
    }

    private void softReset()
    {
        rotation = 0;
        oldRotation = 0;
        BuilderScript.isBuildingMode = false;
        currentY = 999;
        currentX = 999;
        clearSelected();
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
