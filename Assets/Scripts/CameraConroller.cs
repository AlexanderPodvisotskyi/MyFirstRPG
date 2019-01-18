using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraConroller : MonoBehaviour
{

    public Transform target;

    public Tilemap theMap;

    private Vector3 bottomLeftLimit;
    private Vector3 topRigthLimit;

    private float halfHeigth;
    private float halfWidth;


    // Start is called before the first frame update
    void Start()
    {
        target = PlayerController.instense.transform;

        halfHeigth = Camera.main.orthographicSize;
        halfWidth = halfHeigth * Camera.main.aspect;

        bottomLeftLimit = theMap.localBounds.min + new Vector3(halfWidth, halfHeigth, 0f);
        topRigthLimit = theMap.localBounds.max + new Vector3(-halfWidth, -halfHeigth, 0f);
    }

    // LateUpdate is called once per frame after Update
    void LateUpdate() 
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        // Keep camera inside the bounds
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRigthLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRigthLimit.y), transform.position.z);
    }
}
