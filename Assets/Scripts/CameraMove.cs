using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed;
    public Transform player;
    Vector2 dir;
    float h;
    float v;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKey(KeyCode.W))|| (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.D)))
        {

        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = player.position+new Vector3(0,0,-10);
        }
        else
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
            dir = new Vector2(h, v) * speed;
            transform.Translate(dir);
        }

    }
}
