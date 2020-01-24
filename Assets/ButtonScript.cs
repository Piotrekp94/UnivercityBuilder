using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    public Sprite on;
    public Sprite off;
    public GameObject prefab;
    

    private SizeScript sizeScript;
    private ManagerStatistic manager;
    public StatsPanelScript statsPanel;

    void Start()
    {
        button = GetComponent<Button>();
        button.image.sprite = off;
        sizeScript = prefab.GetComponent<SizeScript>();
        manager = ManagerStatistic.Instance;
    }

    void Update()
    {
        if (manager.canBuy(sizeScript.prize))
        {
            button.image.sprite = on;
            if (button.GetComponent<Button>().IsInteractable() == false)
            {
                button.GetComponent<Button>().interactable = true;
            }
        }
        else 
        {
            button.image.sprite = off;
            if (button.GetComponent<Button>().IsInteractable() == true)
            {
                button.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        statsPanel.toggle();
        statsPanel.setString("Cost: " + sizeScript.prize.ToString() + "\n" + "Maintenance: " + sizeScript.maintenanceCost.ToString());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        statsPanel.toggle();
    }
}
