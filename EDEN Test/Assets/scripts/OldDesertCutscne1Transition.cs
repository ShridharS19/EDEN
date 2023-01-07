using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Timeline;

public class OldDesertCutscne1Transition : MonoBehaviour
{

    public GameObject barrier;
    public GameObject block;

    // Start is called before the first frame update
    void Start()
    {
        //trans();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void trans()
    {
        
        Instantiate(barrier, new Vector3((float)42.1, (float)-77.5, 0.1f), Quaternion.identity);
        Instantiate(barrier, new Vector3((float)42.1, (float)-75.5, 0.1f), Quaternion.identity);
        Instantiate(barrier, new Vector3((float)42.1, (float)-78.5, 0.1f), Quaternion.identity);
        Instantiate(barrier, new Vector3((float)42.1, (float)-79.5, 0.1f), Quaternion.identity);
        Instantiate(barrier, new Vector3((float)42.1, (float)-74.5, 0.1f), Quaternion.identity);
        Instantiate(barrier, new Vector3((float)42.1, (float)-73.5, 0.1f), Quaternion.identity);
        //GameObject.Find("Main Camera").GetComponent<VirtualCameraManager>().blackOut(2f);
        GameObject.Find("dialoguemanager").GetComponent<dialogue_manager>().OndialogueEnd += OldDesertCutscne1Transition_OndialogueEnd;
    }

    private void OldDesertCutscne1Transition_OndialogueEnd(object sender, GameObject e)
    {
        GameObject.Find("dialoguemanager").GetComponent<dialogue_manager>().OndialogueEnd -= OldDesertCutscne1Transition_OndialogueEnd;
        GameObject.Find("dialoguemanager").GetComponent<dialogue_manager>().OndialogueEnd += OldDesertCutscne1Transition_OndialogueEnd2;
        burackout();
        //start sandstorm
        Dialogue[] Array = new Dialogue[3];
        Array[0] = new Dialogue("Sandstorm is coming lol", "Cernard");
        Array[1] = new Dialogue("Lemme go check on Bossu", "Dernard");
        Array[2] = new Dialogue("Let's go save villagers Bernard", "Ce0rnard");


        GameObject.Find("Dernard").GetComponent<dialogue_trigger>().inputdialogue(Array);
        GameObject.Find("Dernard").GetComponent<dialogue_trigger>().StartDialogue();
    }

    [System.Obsolete]
    private void OldDesertCutscne1Transition_OndialogueEnd2(object sender, GameObject e)
    {
        Dialogue[] Array2 = new Dialogue[3];
        Array2[0] = new Dialogue("Sandstorm is coming lol", "Cernard");
        Array2[1] = new Dialogue("Lemme go check on Bossu", "Dernard");
        Array2[2] = new Dialogue("Let's go save villagers Bernard", "Ce0rnard");
        GameObject.Find("dialoguemanager").GetComponent<dialogue_manager>().OndialogueEnd -= OldDesertCutscne1Transition_OndialogueEnd2;
        GameObject.Find("Cernard").GetComponent<Transform>().position= new Vector3(101f, -91f);
        GameObject.Find("Dernard").SetActive(false);
        GameObject.Find("Cernard").AddComponent<CernardInPuzzleScript>();
        GameObject.Find("Cernard").AddComponent<dialogue_trigger>();
        GameObject.Find("Cernard").AddComponent<localDialogueTrigger>();
        GameObject.Find("Cernard").GetComponent<dialogue_trigger>().Canvas_dialogue=GameObject.Find("UI").transform.FindChild("dialogue").gameObject;
        Debug.Log("Transision script");
        GameObject.Find("player").GetComponent<SandBlockDevice>().puzzlestart=true;
        GameObject.Find("player").GetComponent<SandBlockDevice>().sword_range = GameObject.Find("player").GetComponent<swordcombat>().sword_range;
        //GameObject.Find("player").GetComponent<SandBlockDevice>().enemy_layer = LayerMask.GetMask("enemy_layer");
        GameObject.Find("Cernard").GetComponent<dialogue_trigger>().inputdialogue(Array2);
    }

    public void burackout()
    {
        GameObject.Find("Main Camera").GetComponent<VirtualCameraManager>().blackOut(1f);
        GameObject.Find("MadMax1").SetActive(false);
        GameObject.Find("MadMax2").SetActive(false);
    }

    public void puzzlesetup()
    {

    }
}
