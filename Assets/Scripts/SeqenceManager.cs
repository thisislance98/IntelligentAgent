using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeqenceManager : MonoBehaviour
{
 
    public int TimeWindow = 2;

    List<Block> AllBlocks;

    List<Block> _blocksInWindow = new List<Block>();

    public Color InWindowColor = Color.red;
    public static SeqenceManager Instance;
    void Awake() {
        Instance = this;
    }

    void Start()
    {
        AllBlocks = new List<Block>(GameObject.FindObjectsOfType<Block>());

        foreach (Block block in AllBlocks) {
            block.AddObserver(gameObject);
        }
    }

    void Update() {
        // first remove any blocks that are outside the window
        while(_blocksInWindow.Count > 0 && Time.time - _blocksInWindow[0].FireTime > TimeWindow) {
            _blocksInWindow[0].RevertColor();
            _blocksInWindow.RemoveAt(0);
        }

        if (Input.GetKeyDown(KeyCode.C)) {

            SceneManager.LoadScene(0);
        }
    }

    void OnObservedBlockFire(Block block) {
        
        // now create connections or add strength to connections from the blocks in the window
        // to our current firing block
        foreach (Block fromBlock in _blocksInWindow) {
            float travelTime = block.FireTime - fromBlock.FireTime;
            Connection c = fromBlock.GetConnectionTo(block,travelTime);
            if (c) {
                c.AddStrength();
            }
            else {
                fromBlock.ConnectTo(block,travelTime);
            }
        }

        if (block.FiredFromSensor) {
            block.SetColor(InWindowColor,false);
            _blocksInWindow.Add(block);
        }
  
    }

}
