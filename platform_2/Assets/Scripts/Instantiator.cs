using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    public Transform objPrefab; 
    public Transform objPos; 
    public float spawnTimer = 1f; 
    

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            Instantiate(objPrefab, objPos.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
