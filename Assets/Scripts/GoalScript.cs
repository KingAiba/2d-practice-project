using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public delegate void OnGoalTriggerDelegate(bool flag);
    public OnGoalTriggerDelegate OnGoalTrigger;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            OnGoalTrigger?.Invoke(true);
        }
    }
}
