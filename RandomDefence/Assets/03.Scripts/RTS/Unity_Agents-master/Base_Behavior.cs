using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Behavior : MonoBehaviour
{

    public int team;

    //links to the different behavior components
    public Idle_Script idle;
    public Seek_Script seek;
    public Attack_Script attack;

    //gps is our general pathfinding script
    //public general_pathfinding gps;

    //intelligent movement scripts
    public Agent agentScript;

    public Seek seekScript;
    public BoidCohesion boidCoh;
    public BoidSeparation boidSep;
    public Flee fleeScript;

    public float maxSpeed;

    public GameObject target;
    public UnitFSM state;

    public enum UnitFSM //states
    {
        Attack,
        Seek,
        Idle
    }

    // Start is called before the first frame update
    void Start()
    {
        agentScript = gameObject.AddComponent<Agent>(); //add agent
        agentScript.maxSpeed = maxSpeed;

        changeState(UnitFSM.Seek);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if(state == UnitFSM.Idle)
            {
                changeState(UnitFSM.Seek);
            }
            else
            {
                changeState(UnitFSM.Idle);
            }
        }
    }

    public void changeState(UnitFSM new_state)
    {

        state = new_state;

        switch (new_state)
        {
            case UnitFSM.Idle:

                if(gameObject.GetComponent<Idle_Script>() == null)
                {
                    idle = gameObject.AddComponent<Idle_Script>();
                }
                DestroyImmediate(seek);
                DestroyImmediate(attack);

                break;

            case UnitFSM.Seek:

                if (gameObject.GetComponent<Seek_Script>() == null)
                {
                    seek = gameObject.AddComponent<Seek_Script>();
                }
                DestroyImmediate(idle);
                DestroyImmediate(attack);

                break;

            case UnitFSM.Attack:

                if (gameObject.GetComponent<Attack_Script>() == null)
                {
                    attack = gameObject.AddComponent<Attack_Script>();
                }
                DestroyImmediate(seek);
                DestroyImmediate(idle);

                break;



        }
    }
}
