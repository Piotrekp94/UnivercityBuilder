using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    bool tutorialDormitory = false;
    bool tutorialDepartment = false;

    public GameObject TutorialDormPanel;
    public GameObject TutorialDepaPanel;

    public void dormActive()
    {
        if(! tutorialDormitory)
        {
            tutorialDormitory = true;
            TutorialDormPanel.SetActive(true);
        }
    }
    public void depaActive()
    {
        if (!tutorialDepartment)
        {
            tutorialDepartment = true;
            TutorialDepaPanel.SetActive(true);
        }
    }

}
