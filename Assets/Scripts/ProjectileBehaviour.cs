using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    void Start()
    {
        //This will destroy the object after 3 seconds
        Invoke("DestroyProjectile", 3f);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
