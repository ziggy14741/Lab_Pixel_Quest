using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoControler : MonoBehaviour
{
    string String = "hello ";  
    int var1 = 3;
        
       
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World");
        string String2 = "world";
        Debug.Log(String + String2);
        
    }

    //8 Update is called once per frame
    void Update()
    {
        var1++;
        Debug.Log(var1);
    }
}