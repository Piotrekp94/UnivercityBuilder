using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateStudentsValue : MonoBehaviour
{
    Text studentsText;
    // Start is called before the first frame update
    void Start()
    {
        studentsText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        studentsText.text = PersistentUIManager.Instance.Students.ToString();
    }
}
