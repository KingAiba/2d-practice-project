using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTriggerScript : MonoBehaviour
{
    public bool JumpedBarrel = false;
    public LayerMask barrelLayer;

    public delegate void OnBarrelJumpDelegate(int val);
    public OnBarrelJumpDelegate OnBarrelJump;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Barrel"))
        {      
            JumpedBarrel = true;
            OnBarrelJump?.Invoke(10);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        JumpedBarrel = false;
    }
}
