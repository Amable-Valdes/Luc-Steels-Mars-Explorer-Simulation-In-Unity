using UnityEngine;

public class StoreMineral : MonoBehaviour
{

    private SimulationManager simulationManager;
    void Awake()
    {
        simulationManager = GameObject.Find("SimulationManager").GetComponent<SimulationManager>();
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.GetComponent<Collider>().tag == "Mineral")
        {
            Mineral_In_Base(collisionInfo.GetComponent<Collider>().gameObject);
        }

        if (collisionInfo.GetComponent<Collider>().tag == "Robot")
        {
            collisionInfo.GetComponent<Collider>().GetComponent<RobotControl>().inWarehouse = true;
            if (collisionInfo.GetComponent<Collider>().GetComponent<RobotControl>().mineral_Loaded != null)
            {
                Mineral_In_Base(collisionInfo.GetComponent<Collider>().GetComponent<RobotControl>().mineral_Loaded);
            }
        }
    }

    void OnTriggerStay(Collider collisionInfo)
    {
        if (collisionInfo.GetComponent<Collider>().tag == "Mineral")
        {
            Mineral_In_Base(collisionInfo.GetComponent<Collider>().gameObject);
        }

        if (collisionInfo.GetComponent<Collider>().tag == "Robot")
        {
            collisionInfo.GetComponent<Collider>().GetComponent<RobotControl>().inWarehouse = true;
            if (collisionInfo.GetComponent<Collider>().GetComponent<RobotControl>().mineral_Loaded != null)
            {
                Mineral_In_Base(collisionInfo.GetComponent<Collider>().GetComponent<RobotControl>().mineral_Loaded);
            }
        }
    }

    private void Mineral_In_Base(GameObject mineral)
    {
        if (mineral != null && mineral.GetComponent<MineralBehaviour>().inWarehouse != true) {
            mineral.GetComponent<MineralBehaviour>().inWarehouse = true;
            simulationManager.Mineral_In_Base(mineral);
        }
    }
}
