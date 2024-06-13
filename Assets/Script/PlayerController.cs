using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float rotationSpeed = 100f;

    private float horizontalInput;
    private float verticalInput;

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * verticalInput * playerSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
