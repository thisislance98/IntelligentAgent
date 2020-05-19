using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
 
    public float Speed;
    public float FOVSpeed = 1;

    Camera _camera;

    void Start() {
        _camera = Camera.main;
    }
     // Update is called once per frame
    // void Update()
    // {

    //     float h = Input.GetAxis("Horizontal");
    //     float v = Input.GetAxis("Vertical");

    //     transform.position += (h * Speed * Time.deltaTime * transform.right) + (v * Speed * Time.deltaTime * transform.up);
        
    //     if (Input.GetKey(KeyCode.E)) {
    //         _camera.fieldOfView -= FOVSpeed * Time.deltaTime;
    //     }

    //     if (Input.GetKey(KeyCode.Q)) {
    //         _camera.fieldOfView += FOVSpeed * Time.deltaTime;
    //     }

        
    // }
}
