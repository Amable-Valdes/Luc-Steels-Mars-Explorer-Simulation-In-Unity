using UnityEngine;

public class StoreMineral : MonoBehaviour
{
    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.GetComponent<Collider>().tag == "Mineral")
        {
            collisionInfo.GetComponent<Collider>().GetComponent<MineralBehaviour>().inWarehouse = true;
        }

        if (collisionInfo.GetComponent<Collider>().tag == "Robot")
        {
            collisionInfo.GetComponent<Collider>().GetComponent<RobotControl>().inWarehouse = true;
            if (collisionInfo.GetComponent<Collider>().GetComponent<RobotControl>().mineral_Loaded != null)
            {
                collisionInfo.GetComponent<Collider>().GetComponent<RobotControl>().mineral_Loaded.GetComponent<MineralBehaviour>().inWarehouse = true;
            }
        }
    }

    void OnTriggerStay(Collider collisionInfo)
    {
        if (collisionInfo.GetComponent<Collider>().tag == "Mineral")
        {
            collisionInfo.GetComponent<Collider>().GetComponent<MineralBehaviour>().inWarehouse = true;
        }

        if (collisionInfo.GetComponent<Collider>().tag == "Robot")
        {
            collisionInfo.GetComponent<Collider>().GetComponent<RobotControl>().inWarehouse = true;
            if (collisionInfo.GetComponent<Collider>().GetComponent<RobotControl>().mineral_Loaded != null)
            {
                collisionInfo.GetComponent<Collider>().GetComponent<RobotControl>().mineral_Loaded.GetComponent<MineralBehaviour>().inWarehouse = true;
            }
        }
    }
}
