using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputBlock : Block
{
    public KeyCode ActivationKey;
 
    protected override void Start() {
        base.Start();
    
        SetColor(Random.ColorHSV());
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(ActivationKey)) {
            
            Fire(1,true);
            
        }
    }
}
