using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxCamera : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Camera SkyCamera;
    [SerializeField] private Vector3 SkyBoxRotation;

    //Makes the rotation
    public void SetSkyBoxRotation(Vector3 rotation)
    {
        this.SkyBoxRotation += rotation * 0.2f;
    }

    void FixedUpdate()
    {
        //Set the right values for X and Y for the right effect
        Vector2 invertXandY = new Vector2(-player.velocity.y, player.velocity.x);

        SkyCamera.transform.position = MainCamera.transform.position;
        SkyCamera.transform.rotation = MainCamera.transform.rotation;
        SkyCamera.transform.Rotate(SkyBoxRotation);
        SetSkyBoxRotation(invertXandY);
        
    }

}
