using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicCamera : MonoBehaviour
{
    public Camera CameraToMimic;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.position = CameraToMimic.transform.position;
        GetComponent<Camera>().fieldOfView = CameraToMimic.fieldOfView;
        
    }
}
