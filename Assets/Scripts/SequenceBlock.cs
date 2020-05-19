using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceBlock : Block
{

    List<Block> _blocks = new List<Block>();
    int _currentBlockIndex = 0;

    public void Initialize(params Block[] blocks) {
        foreach (Block block in blocks) {
            block.AddObserver(gameObject);
        }
        BlockColor = Color.red;
    }

    // Fire if all observed blocks fire in a sequence
    void OnObservedBlockFire(Block block) {
        if (block == _blocks[_currentBlockIndex]) {

            if (BlockIndex == _blocks.Count-1) {
                Fire();
                BlockIndex = 0;
            }
            else {
                BlockIndex++;
            }
        }
    }

    public void Strengthen(float strength=.1f) {
        foreach (Connection c in Connections) {
            c.AddStrength(strength);
        }
    }

}
