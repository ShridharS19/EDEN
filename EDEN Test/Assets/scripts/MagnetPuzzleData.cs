using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPuzzleData : MonoBehaviour
{
    int treesUsed;
    int numberOfTests;
    bool isPickaxeOwned;
    bool MagnetsHarvested;
    bool MagnetsBroken;

    public MagnetPuzzleData()
    {
        treesUsed = GameObject.Find("PuzzleNPC").GetComponent<PuzzleNPCScript>().getTreesUsed();
        numberOfTests =GameObject.Find("player").GetComponent<DetectBlocks>().getTests();
        isPickaxeOwned = GameObject.Find("player").GetComponent<PickaxeBlockBreaking>().getPickaxe();
        MagnetsHarvested = GameObject.Find("player").GetComponent<PickaxeBlockBreaking>().AreMagnetsHarvested();
        MagnetsBroken= GameObject.Find("Main magnetic block").GetComponent<MainMagBlock>().AreMagnetsBroken();
    }
}
