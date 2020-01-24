using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animatedPanel;
    private bool isOpen = false;
    public void callSlide()
    {
        isOpen = !isOpen;
        animatedPanel.SetBool("slideIn", isOpen);
    }


}
