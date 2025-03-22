using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeoControler : MonoBehaviour
{
    string String = "hello ";  
    int var1 = 3;
    Rigidbody2D rb;
    public string nextlevel = "Scene_2";
    public string Secretlevel = "Secretlevel";
    private int coinCounter = 0;
       float speed = 7;
    private void OnTriggerEnter2D(Collider2D collision)
     {
        Debug.Log(collision.tag);
        switch (collision.tag) {
            case "Death":
                {
                    string thislevel=SceneManager.GetActiveScene().name;
                    SceneManager.LoadScene(thislevel);
                    break;
            }
           case "Finish":
                {
                    SceneManager.LoadScene(nextlevel);
                    break;
                }
           case "secret_level":
                        {
                    SceneManager.LoadScene(Secretlevel);
                    break;
                        }
            case "Coin":
                {
                    coinCounter++;
                    Destroy(collision.gameObject);
                    break;
                }
        }
           


    
  
       
        }
        // Start is called before the first frame update
        void Start()
    {
    /*    Debug.Log("Hello World");
        string String2 = "world";
        Debug.Log(String + String2);
    */
        rb = GetComponent<Rigidbody2D>();
    }

    //8 Update is called once per frame
    void Update()
    {
        //console based interaction//
       // var1++;
        //Debug.Log(var1);
        //Movement based action//
        //transform.position += new Vector3(0.0005f,0,0);
        /*
         if (Input.GetKeyDown(KeyCode.W))
         {
             transform.position += new Vector3(0, 1, 0);
         }
         if (Input.GetKeyDown(KeyCode.S)) 
         { 
             transform.position += new Vector3(0, -1, 0); 
         }
         if (Input.GetKeyDown(KeyCode.D))
         {
             transform.position += new Vector3(1, 0, 0);
         }
         if(Input.GetKeyDown(KeyCode.A))
         {
             transform.position += new Vector3(-1, 0, 0);
         }
         if (Input.GetKeyDown(KeyCode.UpArrow))
         {
             transform.position += new Vector3(0, 1, 0);
         }
         if (Input.GetKeyDown(KeyCode.DownArrow))
         {
             transform.position += new Vector3(0, -1, 0);
         }
         if (Input.GetKeyDown(KeyCode.RightArrow))
         {
             transform.position += new Vector3(1, 0, 0);
         }
         if (Input.GetKeyDown(KeyCode.LeftArrow))
         {
             transform.position += new Vector3(-1, 0, 0);
         }
        

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-1, rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(1, -rb.velocity.y);
        }
        */
        float xImput = Input.GetAxis("Horizontal");
       // Debug.Log(xImput);
        rb.velocity = new Vector2(xImput * speed, rb.velocity.y);
    }


}