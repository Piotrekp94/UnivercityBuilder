using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsActiveDolarScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite on;
    public Sprite off;
    private Button b; 
    void Start()
    {
        b = this.gameObject.GetComponent<Button>();
    }

    public void toggle(bool isActive)
    {
        if(isActive)
        {
            b.image.sprite = on;
        }
        else
        {
            b.image.sprite = off;
        }
    }
}
