using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConroller : MonoBehaviour
{

    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerController.instense.transform;
    }

    // LateUpdate is called once per frame after Update
    void LateUpdate() 
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
