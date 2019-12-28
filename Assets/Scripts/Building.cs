using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ObjectToSpawn;

    void Start()
    {
        Vector3 v = new Vector3(0,5,0);
        Instantiate(ObjectToSpawn, transform.position + v, Quaternion.identity);
        Debug.Log(transform.position + v);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
