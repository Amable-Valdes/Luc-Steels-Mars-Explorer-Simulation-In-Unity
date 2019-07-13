using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 0.05f;

    private float h;
    private float v;

    private float up;
    private float right;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        // Get the mouse delta. This is not in the range -1...1
        h = 2.0f * Input.GetAxis("Mouse X");
        v = 2.0f * Input.GetAxis("Mouse Y");

        up = 0;
        right = 0;

        if (Input.GetKey("s"))
        {
            up = -1f;
        }
        if (Input.GetKey("w"))
        {
            up = 1f;
        }

        if (Input.GetKey("a"))
        {
            right = - 1f;
        }
        if(Input.GetKey("d"))
        {
            right = 1f;
        }

        transform.Rotate(0, h, 0);
        transform.position = transform.position + new Vector3(right, 0, up);
    }
}
