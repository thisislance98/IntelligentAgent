using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour
{
    public Transform SpherePrefab;
    public float Strength;
    public Block ToBlock;
    public Block FromBlock;

    bool _isActivive;

    public static float TimeWindow = .2f;

    bool _showLines = true;

    // the time it takes to from the activation to go from the from block to the to block
    public float TravelTime;
    float _currentTravelTime = 0;

    float _fromBlockActivation;

    float _increase;


    Vector3 _offset;

    public static int MaxActiveConnections = 10;
    static int numActiveConnections = 0;
    public void Init(Block fromBlock, Block toBlock, float travelTime=1, float strength=.1f) {
        Strength = strength;
        FromBlock = fromBlock;
        ToBlock = toBlock;
        TravelTime = travelTime; 
        _offset = transform.forward * travelTime;
        
        if (_showLines)
            Utils.Instance.DrawLine(FromBlock.transform.position + _offset,ToBlock.transform.position + _offset,Color.green,.2f);
    }

    public void AddStrength(float strength=.1f) {
        Strength += strength;

        // Debug.Log("increasing strength: " + Strength);
    }

    public void OnFromBlockFire() {

        if (true) { // numActiveConnections < MaxActiveConnections) {
            _isActivive = true;
            Vector3 from = FromBlock.transform.position + _offset;
            Vector3 to = ToBlock.transform.position + _offset;

            float increase = FromBlock.Activation * Strength;

            numActiveConnections++;
            Transform sphere = (Transform)Instantiate(SpherePrefab,FromBlock.transform.position,Quaternion.identity);
            
            float hitTime = Time.time + TravelTime;
            float roundedTravelTime = Utils.Instance.RoundTo(hitTime) - Time.time;

            sphere.GetComponent<Signal>().Init(ToBlock,from,to,roundedTravelTime,increase);
   
            
            if (_showLines)
                Utils.Instance.DrawLine(from,to,Color.white,.02f,roundedTravelTime);   
        }
        
    }
}
