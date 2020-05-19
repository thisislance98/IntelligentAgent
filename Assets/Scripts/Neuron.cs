using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Neuron : MonoBehaviour
{
    
    public float Rate = 1;
    public float Force = 1000;
    public Vector3 ForceDir;
    Rigidbody _body;
    float _timeSinceFire = 0;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceFire += Time.deltaTime;
        if (_timeSinceFire > Rate) {
            ForceDir = new Vector3(Random.RandomRange(-1.0f,1.0f),0,Random.RandomRange(-1.0f,1.0f));
            _body.AddForce(ForceDir,ForceMode.VelocityChange);
        }
        
    }
}
