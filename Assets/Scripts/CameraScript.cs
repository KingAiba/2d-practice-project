using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float followSpeed;

    public GameObject target;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector2 newPos = Vector2.Lerp(transform.position, target.transform.position, Time.deltaTime * followSpeed);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }
}
