using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapToObjects : MonoBehaviour
{
    private static int sx = 90;
    private static int sy = 60;
    public GameObject blank;
    public static GameObject[,] mapOfObjects = new GameObject[sx, sy];

    public bool isGreen = false;

    private GameObject tempObject;
    private GameObject prefab;
    private bool toggledView = false;

    public void killObject(int x, int y)
    {
        x = mapOfObjects[x, y].GetComponent<SizeScript>().x;
        y = mapOfObjects[x, y].GetComponent<SizeScript>().y;

        int sizeX = mapOfObjects[x, y].gameObject.GetComponent<SizeScript>().sizeX;
        int sizeY = mapOfObjects[x, y].gameObject.GetComponent<SizeScript>().sizeY;
        Destroy(mapOfObjects[x, y].gameObject);
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                mapOfObjects[x + i, y + j] = createBlankAtPosition(x + i, y + j);
            }
        }
    }
    public void paintTempObject(int x, int y, GameObject p, int rotation)
    {
        this.prefab = p;
        if (tempObject != null)
        {
            Destroy(tempObject);
        }

        int sizeX = prefab.GetComponent<SizeScript>().sizeX;
        int sizeY = prefab.GetComponent<SizeScript>().sizeY;
        if (!ManagerStatistic.Instance.canBuy(prefab.GetComponent<SizeScript>().prize) || isPlaceTaken(x, y, prefab))
        {
            tempObject = createObject(prefab, x, y, sizeX, sizeY, rotation);
            tempObject.GetComponent<Renderer>().material.color = new Color32((byte)255, (byte)0, (byte)0, 255);
            isGreen = false;

        }
        else
        {
            tempObject = createObject(prefab, x, y, sizeX, sizeY, rotation);
            tempObject.GetComponent<Renderer>().material.color = new Color32((byte)0, (byte)255, (byte)0, 255);
            isGreen = true;
        }
    }

    internal void softReset()
    {
        isGreen = false;
        if (tempObject != null)
        {
            Destroy(tempObject);
        }
        tempObject = null;
        prefab = null;
    }

    public GameObject createObject(GameObject prefab, int x, int y, int sizeX, int sizeY, int v)
    {
        var constructedObject = Instantiate(prefab, new Vector3(x * 10 + 5 * sizeX, 0, y * 10 + 5 * sizeY), Quaternion.Euler(0, v, 0));
        constructedObject.GetComponent<SizeScript>().x = x;
        constructedObject.GetComponent<SizeScript>().y = y;
        constructedObject.name = constructedObject.name + "x" + x.ToString() + "y" + y.ToString();

        return constructedObject;
    }



    internal void constructPaintedObject(int currentX, int currentY, int rotation)
    {
        var stats = ManagerStatistic.Instance;
        int prefabPrize = prefab.GetComponent<SizeScript>().prize;
        if (!stats.canBuy(prefabPrize))
        {
            return;
        }
        BuilderScript.isBuildingMode = false;
        stats.buy(prefabPrize);
        stats.updateMoneyPerSecond(prefab.GetComponent<SizeScript>().maintenanceCost * -1);
        isGreen = false;
        if (tempObject != null)
        {
            Destroy(tempObject);
        }
        Destroy(mapOfObjects[currentX, currentY]);
        var newObject = createObject(prefab, currentX, currentY, 1, 1, rotation);
        newObject.GetComponent<SizeScript>().isOwned = true;
        mapOfObjects[currentX, currentY] = newObject;
        prefab = null;


    }

    private bool isPlaceTaken(int x, int y, GameObject prefab)
    {
        int sizeX = prefab.GetComponent<SizeScript>().sizeX;
        int sizeY = prefab.GetComponent<SizeScript>().sizeY;
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                if (!mapOfObjects[x + i, y + j].transform.name.StartsWith("blank"))
                    return true;
            }
        }
        return false;
    }

    private GameObject createBlankAtPosition(int x, int y)
    {
        GameObject g = Instantiate(blank, new Vector3(x * 10 + 5 * 1, 0, y * 10 + 5 * 1), Quaternion.Euler(0, 0, 0));
        g.GetComponent<SizeScript>().x = x;
        g.GetComponent<SizeScript>().y = y;
        return g;
    }
    public void toggleView()
    {
        if (toggledView)
        {
            for (int i = 0; i < sx; i++)
            {
                for (int j = 0; j < sy; j++)
                {
                    var building = mapOfObjects[i, j];
                    var buildingScript = building.GetComponent<SizeScript>();
                    if (building.name.StartsWith("road") || building.name.StartsWith("cross"))
                        continue;
                    building.GetComponent<Renderer>().material.color = new Color32((byte)buildingScript.red, (byte)buildingScript.green, (byte)buildingScript.blue, 255);
                }
            }
            toggledView = !toggledView;

        }
        else
        {
            var blue = new Color32((byte)255, (byte)170, (byte)150, 255);
            var grey = new Color32((byte)55, (byte)55, (byte)55, 255);
            for (int i = 0; i < sx; i++)
            {
                for (int j = 0; j < sy; j++)
                {
                    var building = mapOfObjects[i, j];
                    if (building.name.StartsWith("road") || building.name.StartsWith("cross"))
                        continue;
                    if (building.GetComponent<SizeScript>().isOwned)
                    {
                        building.GetComponent<Renderer>().material.color = blue;
                    }
                    else
                    {
                        building.GetComponent<Renderer>().material.color = grey;
                    }
                }
            }
            toggledView = !toggledView;
        }
    }
}
