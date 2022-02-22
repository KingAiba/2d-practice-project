using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBarrels : MonoBehaviour
{
    public GameObject barrelPrefab;
    public float initialForce = 5f;

    public float spawnRate = 2f;
    void Start()
    {
        StartCoroutine(BarrelSpawnProcedure());
    }

    void Update()
    {
        
    }


    IEnumerator BarrelSpawnProcedure()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnRate);
            GameObject newBarrel = Instantiate(barrelPrefab, transform.position, barrelPrefab.transform.rotation);
            newBarrel.GetComponent<Rigidbody2D>().velocity = Vector2.right * initialForce * 10;
        }

    }
}
