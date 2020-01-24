using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            var statsPanelScript = FindObjectsOfTypeAll<StatsPanelScript>.findObject();
            var result = statsPanelScript.First();
            result.setString("");
            result.toggle(false);
        }
        //if (BuilderScript.activeSelling)
        //{
        //    RaycastHit hit;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out hit, range))
        //    {
        //        GameObject selectedObject = GameObject.Find(hit.transform.gameObject.name);
        //        if (selectedObject.name.StartsWith("blank") || selectedObject.name.StartsWith("uniwersytet"))
        //        {
        //            var statsPanelScript = FindObjectsOfTypeAll<StatsPanelScript>.findObject();
        //            var result = statsPanelScript.First();
        //            result.toggle(false);
        //        } 
        //        else
        //        if (currentX != selectedObject.GetComponent<SizeScript>().x || currentY != selectedObject.GetComponent<SizeScript>().y || rotation != oldRotation)
        //        {
        //            var statsPanelScript = FindObjectsOfTypeAll<StatsPanelScript>.findObject();
        //            var sizeScript = selectedObject.GetComponent<SizeScript>();
        //            currentX = sizeScript.x;
        //            currentY = sizeScript.y;
        //            int price = getPrice(sizeScript);
        //            if (Input.GetMouseButtonDown(0))
        //            {
        //                if (sizeScript.isOwned)
        //                {
        //                    mapToObjects.killObject(currentX, currentY);
        //                    ManagerStatistic.Instance.Money += price;
        //                } else
        //                {

        //                        mapToObjects.killObject(currentX, currentY);
        //                        ManagerStatistic.Instance.Money -= price;

        //                }
        //            }
        //            else
        //            {
        //                var result = statsPanelScript.First();
        //                result.toggle(true);
        //                result.setString(getStringForBuilding(price, sizeScript));
        //            }
        //        }
        //    }
            //else
            //{
            //    var statsPanelScript = FindObjectsOfTypeAll<StatsPanelScript>.findObject();
            //    var result = statsPanelScript.First();
            //    result.toggle(false);
            //}
        //}
        else
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
            mapToObjects.clear();
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, range))
            {
                markClickedObject(ref hit);
            }
        }
    }

    private string getStringForBuilding(int price, SizeScript sizeScript)
    {
        return sizeScript.isOwned ? "Funds returned: " + price.ToString() : "Destroying cost: " + price.ToString();
    }

    private int getPrice(SizeScript sizeScript)
    {
        return  sizeScript.isOwned ? (int) (sizeScript.prize * 0.5) : sizeScript.prize * 2;
    }

    private void softReset()
    {
        rotation = 0;
        oldRotation = 0;
        BuilderScript.isBuildingMode = false;
        BuilderScript.activeSelling = false;
        currentY = 999;
        currentX = 999;
        clearSelected();
        mapToObjects.clear();
    }

    private void markClickedObject(ref RaycastHit hit)
    {
        clearSelected();
        so = hit.transform.gameObject.name;
        selectedObject = GameObject.Find(CastingToObject.so);

        color = selectedObject.GetComponent<Renderer>().material.color;
        selectedObject.GetComponent<Renderer>().material.color = new Color32((byte)red, (byte)green, (byte)blue, 255);
        var sizeScript = selectedObject.GetComponent<SizeScript>();
        mapToObjects.markAtDistance(sizeScript.x, sizeScript.y, sizeScript.range);

        Debug.Log("You selected the " + so); // ensure you picked right object


    }
    private void clearSelected()
    {
        if (selectedObject != null)
        {
            selectedObject.GetComponent<Renderer>().material.color = color;
        }
    }
}
