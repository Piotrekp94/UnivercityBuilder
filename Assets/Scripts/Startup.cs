using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class Startup : MonoBehaviour
{
    public GameObject road;
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
    private HashSet<GameObject> roads = new HashSet<GameObject>();
    private HashSet<GameObject> crossroads = new HashSet<GameObject>();
    private HashSet<GameObject> buildings = new HashSet<GameObject>();
    private HashSet<GameObject> bigBuildings = new HashSet<GameObject>();



    private Random random = new Random();


    void OnEnable()
    {
        loadAssets(11, "road",ref roads);
        loadAssets(3, "crossroad", ref crossroads);
        loadAssets(13, "building11", ref buildings);
        loadAssets(3, "building22", ref bigBuildings);


        for (int i = 0; i < 3; i++)
        {
            crossroads.Add(Resources.Load("crossroad" + i.ToString()) as GameObject);
        }

        GameObject[,] gameObjects = new GameObject[mapSizeX, mapSizeY];
        HashSet<MyObject> set = new HashSet<MyObject>();
        var buildingScript = FindObjectOfType<MapToObjects>();

        array = new MyObject[mapSizeX, mapSizeY];
        prepareMap(ref array);
        for (int i = 0; i < mapSizeX; i++)
        {
            for (int j = 0; j < mapSizeY; j++)
            {
                MyObject m = array[i, j];
                if (m != null)
                {
                    if (!set.Contains(m))
                    {

                        GameObject createdObject = buildingScript.createObject(m.gameObject, i, j, m.xsize, m.ysize, m.rotation);
                        fillMap(ref gameObjects, i, j, ref createdObject);
                        set.Add(m);
                    }
                }
            }
        }
        MapToObjects.mapOfObjects = gameObjects;
        Debug.Log("StartupDone");
    }

    private void loadAssets(int amount, string assetName, ref HashSet<GameObject> collection)
    {
        for (int i = 0; i < amount; i++)
        {
            collection.Add(Resources.Load(assetName + i.ToString()) as GameObject);
        }
    }

    private void fillMap(ref GameObject[,] gameObjects, int i, int j, ref GameObject gameObject)
    {
        for (int x = 0; x < gameObject.GetComponent<SizeScript>().sizeX; x++)
        {
            for (int y = 0; y < gameObject.GetComponent<SizeScript>().sizeY; y++)
            {
                gameObjects[i + x, j + y] = gameObject;
            }
        }
    }

    private void prepareMap(ref MyObject[,] array)
    {
        addCorners(ref array);
        addUnivercityTerritory(ref array);
        addCornerRoads(ref array);
        addSideRoads(ref array);
        fillRest(ref array);
    }

    private void addSideRoads(ref MyObject[,] array)
    {
        for (int y = distanceBetweenXRoads; y < mapSizeY - 1; y += distanceBetweenXRoads)
        {
            for (int i = 1; i < mapSizeX - 1; i++)
            {
                if(i % distanceBetweenYRoads == 0)
                {
                    array[i, y] = new MyObject(pickRandomCrossRoad());
                }
                else
                {
                    array[i, y] = new MyObject(pickRandomRoad());
                }
            }
        }
        for (int x = distanceBetweenYRoads; x < mapSizeX - 1; x += distanceBetweenYRoads)
        {
            for (int i = 1; i < mapSizeY - 1; i++)
            {
                if (i % distanceBetweenXRoads == 0)
                {
                    array[x, i] = new MyObject(pickRandomCrossRoad());
                }
                else
                {
                    array[x, i] = new MyObject(pickRandomRoad(), 1, 1, 90);
                }
            }
        }
    }

    private GameObject pickRandomCrossRoad()
    {
        return crossroads.ElementAt(random.Next(crossroads.Count));
    }

    private GameObject pickRandomRoad()
    {
        if (random.Next(0, 4) == 0)
            return roads.ElementAt(random.Next(roads.Count));
        return road;
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
            array[univercityPosX + 1, univercityPosY + 1] = new MyObject(univercityCenter);

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
        for (int i = 1; i < mapSizeX - 1; i++)
        {
            for (int j = 1; j < mapSizeY - 1; j++)
            {
                if (array[i, j] == null)
                {

                    if (random.Next(0, 2) != 0 || (i < univercityStartingPointX + univercityStartingSize && i > univercityStartingPointX) && (j < univercityStartingPointX + univercityStartingSize && j > univercityStartingPointX))
                    {
                        array[i, j] = new MyObject(empty); ;
                    }
                    else
                    if (random.Next(0, 18) == 0 && isPlaceBig(ref array[i, j + 1], ref array[i + 1, j], ref array[i + 1, j + 1]))
                    {
                        MyObject myObject = pickFromAll(random.Next(0,2));
                        array[i, j] = myObject;
                        array[i + 1, j] = myObject;
                        array[i, j + 1] = myObject;
                        array[i + 1, j + 1] = myObject;
                    }
                    else
                    {
                        array[i, j] = pickFromSmall(random.Next(0, 9));
                    }

                }
            }
        }

    }

    private MyObject pickFromSmall(int buildingNumber)
    {
        return new MyObject(buildings.ElementAt(random.Next(buildings.Count)),random.Next(0,5)*90);
    }

    private MyObject pickFromAll(int buildingNumber)
    {
        if (buildingNumber == 0)
            return new MyObject(bigBuildings.ElementAt(random.Next(bigBuildings.Count)), 2, 2, random.Next(0, 5) * 90);
        if (buildingNumber == 1)
            return new MyObject(bigBuildings.ElementAt(random.Next(bigBuildings.Count)), 2, 2, random.Next(0, 5) * 90);
        return new MyObject(bigBuildings.ElementAt(random.Next(bigBuildings.Count)), 2, 2, random.Next(0, 5) * 90);

    }

    private bool isPlaceBig(ref MyObject myObject, ref MyObject myObject1, ref MyObject myObject2)
    {
        return myObject == null && myObject1 == null && myObject2 == null;
    }

    private void addCornerRoads(ref MyObject[,] array)
    {
        for (int i = 1; i < mapSizeX - 1; i++)
        {
            array[i, 0] = new MyObject(pickRandomRoad());
        }
        for (int i = 1; i < mapSizeX - 1; i++)
        {
            array[i, mapSizeY - 1] = new MyObject(pickRandomRoad());
        }
        for (int i = 1; i < mapSizeY - 1; i++)
        {
            array[0, i] = new MyObject(road, 1, 1, 90);
        }
        for (int i = 1; i < mapSizeY - 1; i++)
        {
            array[mapSizeX - 1, i] = new MyObject(pickRandomRoad(), 1, 1, 90);
        }
    }

    private void addCorners(ref MyObject[,] array)
    {
        array[0, 0] = new MyObject(pickRandomCrossRoad());
        array[0, mapSizeY - 1] = new MyObject(pickRandomCrossRoad());
        array[mapSizeX - 1, 0] = new MyObject(pickRandomCrossRoad());
        array[mapSizeX - 1, mapSizeY - 1] = new MyObject(pickRandomCrossRoad());
    }
}
