using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackBouncing : MonoBehaviour
{
    public LayerMask layerWall;
    public Vector3 stopPoint { get; private set; }

    private Ray ray = new Ray();
    private RaycastHit hit;

    private Vector3[] dirs;

    private void Awake()
    {
        dirs = new Vector3[] { transform.forward, -transform.forward, transform.right, -transform.right };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerStack"))
        {
            FindStopPoint();
        }
    }

    private void FindStopPoint()
    {
        //TODO: bounce player - not working
        for (int i = 0; i < dirs.Length; i++)
        {
            ray = new Ray(transform.position, dirs[i]);
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (!hit.transform.CompareTag("CrossedStack"))
                {
                    if (Physics.Raycast(ray, out hit, 1000, layerWall))
                    {
                        if ((transform.position - hit.point).magnitude > 1f)
                        {
                            stopPoint = hit.point; 
                        }
                    }
                }
            }
        }
    }
}
