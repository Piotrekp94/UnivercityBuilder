using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class Startup : MonoBehaviour
{
    public GameObject tCrossRoad;
    public GameObject crossroad;
    public GameObject road;
    public GameObject widerBuilding;
    public GameObject bigBuilding;
    public GameObject smallBuilding;
    public GameObject empty;
    public GameObject univercityCenter;


    public MyObject[,] array;
    public int mapSizeX = 30;
    public int mapSizeY = 10;

    public int distanceBetweenXRoads = 30;
    public int distanceBetweenYRoads = 30;
    public int univercityStartingPointX = 30;
    public int univercityStartingPointY = 30;
    public int univercityStartingSize = 10;
    // called first

    void OnEnable()
    {
        HashSet<MyObject> set = new HashSet<MyObject>();
        var a = new MyObject(widerBuilding, 1, 2);
        var b = new MyObject(widerBuilding, 1, 2);
        var c = new MyObject(bigBuilding, 2, 2);
        array = new MyObject[mapSizeX, mapSizeY];
        prepareMap(ref array);
        for(int i = 0; i < mapSizeX; i++)
        {
            for (int j = 0; j < mapSizeY; j++)
            {
                MyObject m = array[i, j];
                if (m != null)
                {

                    if (!set.Contains(m))
                    {
                        Instantiate(m.gameObject, new Vector3(i * 10 + 5 * m.xsize, 0, j * 10 + 5 * m.ysize), Quaternion.Euler(0, m.rotation, 0));
                        set.Add(m);
                    }
                }
                
            }
        }

    }

    private void prepareMap(ref MyObject[,] array)
    {
        addCorners(ref array);
        addUnivercityTerritory(ref array);
        addCornerRoads(ref array);
        fillRest(ref array);
    }

    private void addUnivercityTerritory(ref MyObject[,] array)
    {
        int univercityPosX = univercityStartingPointX + univercityStartingSize / 2;
        int univercityPosY = univercityStartingPointY + univercityStartingSize / 2;
        if (array[univercityPosX, univercityPosY] == null)
        {
            array[univercityPosX, univercityPosY] = new MyObject(univercityCenter);
        }
        else
        {
            array[univercityPosX+1, univercityPosY+1] = new MyObject(univercityCenter);

        }
        for (int i = 1; i < mapSizeX - 1; i++)
        {
            for (int j = 1; j < mapSizeY - 1; j++)
            
                if (array[i, j] == null)
                {
                    if ((i < univercityStartingPointX + univercityStartingSize && i > univercityStartingPointX) && (j < univercityStartingPointY + univercityStartingSize && j > univercityStartingPointY))
                    {
                        array[i, j] = new MyObject(empty);
                    }
                }
            }
        }

    private void fillRest(ref MyObject[,] array)
    {
        Random random = new Random();

        for (int i = 1; i < mapSizeX -1; i++)
        {
            for (int j = 1; j < mapSizeY -1; j++)
            {
               if(array[i,j] == null)
               {
                    
                    if (random.Next(0, 2) == 0 || (i < univercityStartingPointX + univercityStartingSize && i > univercityStartingPointX) && (j < univercityStartingPointX + univercityStartingSize && j > univercityStartingPointX) )
                    {
                        array[i, j] = new MyObject(empty); ;
                    }
                    else
                    if (random.Next(0,5) == 0 && isPlaceBig(ref array[i, j + 1], ref array[i + 1, j], ref array[i + 1, j + 1]))
                    {
                        MyObject myObject = pickFromAll();
                        array[i, j] = myObject;
                        array[i+1, j] = myObject;
                        array[i, j+1] = myObject;
                        array[i+1, j+1] = myObject;
                    }
                    else if (random.Next(0, 3) == 0 && isPlaceWide(ref array[i+1,j]))
                    {
                        MyObject myObject = pickFromWide();
                        array[i, j] = myObject;
                        array[i + 1, j] = myObject;
                    }
                    else
                    {
                        array[i, j] = pickFromSmall();
                    }
                    
               }
            }
        }

    }

    private MyObject pickFromSmall()
    {
        return new MyObject(smallBuilding);
    }

    private MyObject pickFromWide()
    {
        return new MyObject(widerBuilding, 2, 1);
    }
    private MyObject pickFromAll()
    {
        return new MyObject(bigBuilding, 2, 2);
    }

    private bool isPlaceWide(ref MyObject myObject)
    {
        return myObject == null;
    }


    private bool isPlaceBig(ref MyObject myObject, ref MyObject myObject1, ref MyObject myObject2)
    {
        return myObject == null && myObject1 == null && myObject2 == null;
    }

    private void addCornerRoads(ref MyObject[,] array)
    {
        for (int i = 1; i < mapSizeX - 1; i++)
        {
            array[i, 0] = new MyObject(road);
        }
        for (int i = 1; i < mapSizeX - 1; i++)
        {
            array[i, mapSizeY - 1] = new MyObject(road);
        }
        for (int i = 1; i < mapSizeY - 1; i++)
        {
            array[0, i] = new MyObject(road,1,1,90);
        }
        for (int i = 1; i < mapSizeY - 1; i++)
        {
            array[mapSizeX-1, i] = new MyObject(road,1,1,90);
        }
        for(int y = distanceBetweenXRoads; y < mapSizeY -1; y+= distanceBetweenXRoads)
        {
            for (int i = 1; i < mapSizeX -1 ; i++)
            {
                array[i, y] = new MyObject(road);
            }
        }
        for (int x = distanceBetweenYRoads; x < mapSizeX - 1; x += distanceBetweenYRoads)
        {
            for (int i = 1; i < mapSizeY - 1; i++)
            {
                array[x, i] = new MyObject(road, 1, 1, 90);
            }


        }
    }

    private void addCorners(ref MyObject[,] array)
    {
        array[0, mapSizeY-1] = new MyObject(crossroad);
        array[mapSizeX-1, 0] = new MyObject(crossroad);
        array[mapSizeX-1, mapSizeY-1] = new MyObject(crossroad);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
