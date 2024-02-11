using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek_Script : MonoBehaviour
{
    Base_Behavior bb;
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        bb = gameObject.GetComponent<Base_Behavior>();
        target = bb.target;

        
        if(bb.seekScript == null)
        {
            bb.seekScript = gameObject.AddComponent<Seek>();
            bb.seekScript.target = target;
            bb.seekScript.weight = 0.7f;
            bb.seekScript.enabled = true;

            //bb.fleeScript = gameObject.AddComponent<Flee>();
            //bb.fleeScript.target = target;
            //bb.fleeScript.weight = 1.0f;
            //bb.fleeScript.enabled = true;

            //bb.boidCoh = gameObject.AddComponent<BoidCohesion>();
            //bb.boidCoh.targets = bb.target.GetComponent<Squad_Parent_Script>().children;
            //bb.boidCoh.weight = 0.4f;
            //bb.boidCoh.enabled = true;

            //bb.boidSep = gameObject.AddComponent<BoidSeparation>();
            //bb.boidSep.targets = bb.target.GetComponent<Squad_Parent_Script>().children;
            //bb.boidSep.weight = 70.0f;
            //bb.boidSep.enabled = true;

        }

    }
    private void OnDestroy()
    {
        DestroyImmediate(bb.seekScript);
    }
    

    private void OnDrawGizmos()
    {
        UnityEditor.Handles.Label(transform.position + Vector3.up * 3, "Seek");
    }

}
