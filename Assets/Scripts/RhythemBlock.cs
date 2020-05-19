using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythemBlock : Block
{

    void OnObservedBlockFire(Block block) {
        float observedActivation = block.Activation;

        if (observedActivation > FieldMin && observedActivation < FieldMax) {
            // create a linear attenuation based on how close in time we are to the center of this block's
            // receptive field
            float distFromCenter = (Mathf.Abs(observedActivation - _fieldCenter) /_fieldRadius);
        
            float activation = observedActivation * (1 - distFromCenter);
            Fire(activation);
        }
    }

}
