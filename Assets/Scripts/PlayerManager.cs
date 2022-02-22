using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public int maxlives;
    public int curlives;

    public int score;

    public bool isDead;

    public LayerMask barrelLayer;

    private ScoreTriggerScript scoreTrigger;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    void Start()
    {
        scoreTrigger = transform.Find("ScoreTrigger").GetComponent<ScoreTriggerScript>();
        scoreTrigger.OnBarrelJump += AddScore;

        curlives = maxlives;
        isDead = false;
    }

    
    void Update()
    {
        //UpdateScore();
        UpdatePlayerUI();
    }

    private void OnDestroy()
    {
        scoreTrigger.OnBarrelJump -= AddScore;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Barrel"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        if(!isDead)
        {
            curlives -= damage;
        }

        if(curlives <= 0)
        {
            isDead = true;
        }

    }

    public bool CheckForBarrelJump()
    {
        float castDistance = 1f;
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, Vector3.down, transform.localScale.y + castDistance, barrelLayer);
        //hit = Physics2D.BoxCast(transform.position, transform,, 0f, Vector2.down, castDistance, groundLayer);

        return hit.collider != null;
    }

    public void UpdateScore()
    {
        if(CheckForBarrelJump())
        {
            AddScore(10);
        }
    }

    public void AddScore(int value)
    {
        if (!isDead)
        {
            score += value;
        }
        
    }

    public void UpdatePlayerUI()
    {  
        livesText.SetText("Lives:"+curlives);
        scoreText.SetText("Score:" + score);     
    }
}
