﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidCohesion : AgentBehavior
{
    //응집

    public float neighborDist = 15.0f;
    public List<GameObject> targets;

    public override Steering GetSteering()
    {
        Steering steer = new Steering();
        int count = 0;

        foreach(GameObject other in targets)
        {
            if(other != null)
            {
                float d = (transform.position - other.transform.position).magnitude;
                if((d > 0) && (d < neighborDist))
                {
                    steer.linear += other.transform.position;
                    count++;
                }
            }
        }//endfor

        if (count > 0)
        {
            steer.linear /= count;
            steer.linear = steer.linear - transform.position;
        }

        return steer;
    }
}
