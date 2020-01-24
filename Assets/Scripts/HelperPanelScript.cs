using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperPanelScript : MonoBehaviour
{
    private bool isToggled = false;
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void toggle()
    {
        isToggled = !isToggled;
        this.gameObject.SetActive(isToggled);

    }
}
