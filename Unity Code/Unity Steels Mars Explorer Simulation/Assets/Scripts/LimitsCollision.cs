using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitsCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider collisionInfo)
    {
        Collider robot = collisionInfo.GetComponent<Collider>();
        if (robot.tag == "Robot") {
            robot.GetComponent<RobotControl>().obstacles.Add(this.gameObject);
        }
    }
}
