using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RodScript : MonoBehaviour
{
    public event Action CollisionEnter;
    public event Action CollisionExit;

    private Rigidbody2D body;

    private float rodSpeed = 0.1f;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            body.AddForce(Vector3.up * Time.deltaTime * rodSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish"))
        {
            Debug.Log("COLLISION ENTER");
            CollisionEnter?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish"))
        {
            Debug.Log("COLLISION EXIT");
            CollisionExit?.Invoke();
        }
    }
}
