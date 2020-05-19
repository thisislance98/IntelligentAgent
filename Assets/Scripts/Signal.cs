using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : MonoBehaviour
{
    public float MinSize = .3f;

    float _currentTime;
    float _timeToDest;

    Vector3 _start;
    Vector3 _end;

    float _strength;

    Block _toBlock;
    
    public void Init(Block toBlock, Vector3 start, Vector3 end, float timeToDest, float strength)
    {
        transform.localScale = Vector3.one * (MinSize);
        _currentTime = 0;
        transform.position = start;
        _start = start;
        _end = end;
        _timeToDest = timeToDest;
        _toBlock = toBlock;
        _strength = strength;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        float percent = _currentTime / _timeToDest;

        transform.position = Vector3.Lerp(_start,_end,percent);

        if (percent >= 1) {
            _toBlock.OnReceivedConnectionActivation(_strength);
            Destroy(gameObject);
        }
        
    }
}
