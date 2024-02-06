using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSeparation : Flee
{
    //분리

    public float desiredSeparation = 15.0f;
    public List<GameObject> targets;

    public override Steering GetSteering()
    {
        Steering steer = new Steering();
        int count = 0;

        //for each boid in the system, check if it is too close
        foreach(GameObject other in targets)
        {
            if (other != null)
            {
                float d = (transform.position - other.transform.position).magnitude;

                if((d > 0) && (d < desiredSeparation))
                {
                    //calculate vector pointing away from neighbor
                    Vector3 diff = transform.position - other.transform.position;
                    diff.Normalize();
                    diff /= d;
                    steer.linear += diff;
                    count++;
                }
            }
        }//end for

        if(count > 0) //객체간의 간격 조정
        {
            steer.linear /= (float)count;
        }

        return steer;

    }
}
