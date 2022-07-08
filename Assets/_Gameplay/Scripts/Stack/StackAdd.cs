using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackAdd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerStack"))
        {
            Destroy(gameObject);
        }
    }
}
