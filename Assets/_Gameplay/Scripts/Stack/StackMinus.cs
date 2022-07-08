using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackMinus : MonoBehaviour
{
    public GameObject crossedStack;

    private Vector3 tempPos;
    private Quaternion tempRot;

    private GameObject createdStack;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerStack"))
        {
            tempPos = transform.position;
            tempRot = transform.rotation;

            createdStack = Instantiate(crossedStack, tempPos, tempRot);
            createdStack.transform.SetParent(transform.parent);

            Destroy(gameObject);

        }
    }
}
