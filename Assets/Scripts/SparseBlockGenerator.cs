using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparseBlockGenerator : MonoBehaviour
{
    public bool On = true;
    public GameObject BlockPrefab;
    public float Min = 0;
    public float Max = 1;
    public int Count;
    public Vector3 Direction;
    public float DistBetweenBlocks = 1;

    // how much overlap in receptive fields. (0 is 0%, 1 is 50%)
    public float OverlapFactor = 1.0f;

    // Start is called before the first frame update
    void Awake()
    {

        if (On == false) return;
        
        Vector3 pos = transform.position;

        // the total range of value
        float diff = Max - Min;

        // the different between receptive field centers
        float centerDist = diff / (float)Count;
        
       
       float fieldCenter = Min;

        for (int i=0; i < Count; i++) {

            Debug.Log("got here");
            pos += Direction * DistBetweenBlocks;
            GameObject block = GameObject.Instantiate(BlockPrefab,pos,Quaternion.identity);
            
            fieldCenter += centerDist;

            // half of the receptive field
            float halfCenterDist = (centerDist * .5f);
            float fieldMin = fieldCenter - (halfCenterDist + (halfCenterDist * OverlapFactor));
            float fieldMax = fieldCenter + (halfCenterDist + (halfCenterDist * OverlapFactor));
            fieldMin = Mathf.Clamp(fieldMin,Min,fieldMin);
            fieldMax = Mathf.Clamp(fieldMax,fieldMax,Max);
            
            block.SendMessage("SetFieldMin",fieldMin);
            block.SendMessage("SetFieldMax",fieldMax);
            GetComponent<Block>().Observers.Add(block);

        }
    }

}
