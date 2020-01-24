using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleView : MonoBehaviour
{
    public void toggle()
    {
        FindObjectOfType<MapToObjects>().toggleView();
    }
}
