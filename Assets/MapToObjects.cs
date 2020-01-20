using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapToObjects : MonoBehaviour
{
    public GameObject blank;
    public static GameObject[,] mapOfObjects = new GameObject[120,90];
    private GameObject tempObject;

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
    public void paintTempObject(int x, int y, GameObject prefab)
    {
        if(tempObject != null)
        {
            Destroy(tempObject);
        }

        int sizeX = prefab.GetComponent<SizeScript>().sizeX;
        int sizeY = prefab.GetComponent<SizeScript>().sizeY;
        if (isPlaceTaken(x,y, prefab))
        {
            tempObject = Instantiate(prefab, new Vector3(x * 10 + 5 * 1, 0, y * 10 + 5 * 1), Quaternion.Euler(0, 0, 0));
            tempObject.GetComponent<Renderer>().material.color = new Color32((byte)255, (byte)0, (byte)0, 255);
        }
        else
        {
            tempObject = Instantiate(prefab, new Vector3(x * 10 + 5 * 1, 0, y * 10 + 5 * 1), Quaternion.Euler(0, 0, 0));
            tempObject.GetComponent<Renderer>().material.color = new Color32((byte)0, (byte)255, (byte)0, 255);
        }
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
}
