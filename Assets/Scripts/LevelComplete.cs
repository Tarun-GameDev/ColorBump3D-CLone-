using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    Collider col;
    
    private void Start()
    {
        if (col == null)
            col = GetComponent<Collider>();


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(GameManager.instance.LevelCompleted());
        }
            
    }
}
