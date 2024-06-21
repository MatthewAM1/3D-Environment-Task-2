using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    [SerializeField] private bool moveLeft = true;
    [SerializeField] private float enemySpeed = 5f;

    [SerializeField] private float leftEdge = -7.5f;

    [SerializeField] private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveLeft == true)
        {
            transform.Translate(Vector3.left * enemySpeed * Time.deltaTime);
        }
        if (moveLeft == false)
        {
            transform.Translate(Vector3.right * enemySpeed * Time.deltaTime);
        }
        if (transform.position.x <= leftEdge)
        {
            moveLeft = false;
        }
        if (transform.position.x >= -leftEdge)
        {
            moveLeft = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            gameManager.BallCaptured();
        }
    }
}
