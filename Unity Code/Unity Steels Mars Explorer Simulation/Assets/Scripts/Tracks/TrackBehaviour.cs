using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBehaviour : MonoBehaviour
{
    public int number;

    public void Create()
    {
        number = 2;
    }

    public void Add()
    {
        number = number + 2;
    }

    public void Remove()
    {
        number = number - 1;
        if (number < 1)
        {
            Destroy(this.gameObject);
        }
    }
}
