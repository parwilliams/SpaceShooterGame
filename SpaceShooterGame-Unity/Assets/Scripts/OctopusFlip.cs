using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class OctopusFlip : MonoBehaviour
{
    //A* Implementation help from Brackey's: https://www.youtube.com/watch?v=jvtFUfJ6CP8

    public AIPath aiPath;

    // Update is called once per frame
    void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-5.5f, 5.5f, 1f);
        }
        else if(aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(5.5f, 5.5f, 1f);
        }
    }
}
