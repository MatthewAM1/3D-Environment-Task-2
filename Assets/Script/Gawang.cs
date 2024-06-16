using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gawang : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            gameManager.Goal();
        }
    }
}
