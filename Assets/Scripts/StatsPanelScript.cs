using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsPanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI script;
    public bool isToggled = false;
    MeshRenderer panel;
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void toggle()
    {
        isToggled = !isToggled;
        this.gameObject.SetActive(isToggled);

    }
    public void setString(string message)
    {
        script.SetText(message);
    }
}
