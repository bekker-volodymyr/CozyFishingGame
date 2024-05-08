using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RodScript : MonoBehaviour
{
    public event Action CollisionEnter;
    public event Action CollisionExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("COLLISION ENTER");
        CollisionEnter?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("COLLISION EXIT");
        CollisionExit?.Invoke();
    }
}
