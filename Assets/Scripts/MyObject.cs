using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyObject
{
    public GameObject gameObject;
    public int xsize = 1;
    public int rotation = 0;
    public int ysize = 1;


    public MyObject(GameObject pole, int xsize, int ysize)
    {
        this.gameObject = pole;
        this.xsize = xsize;
        this.ysize = ysize;
    }
    public MyObject(GameObject pole)
    {
        this.gameObject = pole;
    }
    public MyObject(GameObject pole,int rotation)
    {
        this.gameObject = pole;
        this.rotation = rotation;

    }
    public MyObject(GameObject pole, int xsize, int ysize, int rotation)
    {
        this.gameObject = pole;
        this.xsize = xsize;
        this.rotation = rotation;
        this.ysize = ysize;
    }
}