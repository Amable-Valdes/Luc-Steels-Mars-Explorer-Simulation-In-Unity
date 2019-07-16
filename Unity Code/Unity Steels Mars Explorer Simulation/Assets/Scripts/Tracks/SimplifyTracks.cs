using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplifyTracks : MonoBehaviour
{
    private GameObject track;

    void Awake()
    {
        track = this.transform.parent.gameObject;
        this.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag == "Track")
        {
            track.GetComponent<TrackBehaviour>().Asimilate_Track(collisionInfo.GetComponent<Collider>().gameObject);
        }
    }

    void OnTriggerStay(Collider collisionInfo)
    {
        if (collisionInfo.tag == "Track")
        {
            track.GetComponent<TrackBehaviour>().Asimilate_Track(collisionInfo.GetComponent<Collider>().gameObject);
        }
    }
}
