using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Forward, Back, Left, Right }

public class PlayerMovement : MonoBehaviour
{
    public LayerMask layerWall;
    public LayerMask layerBouncingCorner;
    public float speed;

    private Vector3 downPoint, upPoint;
    private float distanceX, distanceY;
    public Direction dir;

    private Ray ray = new Ray();
    private RaycastHit hit;
    private Vector3 start;
    private Vector3 stopPoint;

    private bool isStop = true;

    private Vector3 temp;

    private void Awake()
    {
        stopPoint = transform.position;
    }

    void Update()
    {
        Move();
        CheckPlayerStop();
    }

    private void SwipeDirection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            downPoint = upPoint = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            upPoint = Input.mousePosition;
        }

        distanceX = Mathf.Abs(downPoint.x - upPoint.x);
        distanceY = Mathf.Abs(downPoint.y - upPoint.y);

        if (distanceX > 10f || distanceY > 10f)
        {
            if (distanceX > distanceY)
            {
                if (downPoint.x > upPoint.x)
                {
                    dir = Direction.Left;
                }
                else
                {
                    dir = Direction.Right;
                }
            }
            else
            {
                if (downPoint.y > upPoint.y)
                {
                    dir = Direction.Back;
                }
                else
                {
                    dir = Direction.Forward;
                }
            }

            FindStopPoint();
            downPoint = upPoint = Vector3.zero;
        }
    }

    private void FindStopPoint()
    {
        start = transform.position;
        start.y = 2.7f;
        temp = stopPoint;

        switch (dir)
        {
            case Direction.Forward:
                ray = new Ray(start, transform.forward);
                if (Physics.Raycast(ray, out hit, 1000, layerWall))
                {
                    temp.x = hit.point.x;
                    temp.z = hit.point.z - 0.5f;
                    stopPoint = temp;
                }
                break;

            case Direction.Back:
                ray = new Ray(start, -transform.forward);
                if (Physics.Raycast(ray, out hit, 1000, layerWall))
                {
                    temp.x = hit.point.x;
                    temp.z = hit.point.z + 0.5f;
                    stopPoint = temp;
                }
                break;

            case Direction.Right:
                ray = new Ray(start, transform.right);
                if (Physics.Raycast(ray, out hit, 1000, layerWall))
                {
                    temp.z = hit.point.z;
                    temp.x = hit.point.x - 0.5f;
                    stopPoint = temp;
                }
                break;

            case Direction.Left:
                ray = new Ray(start, -transform.right);
                if (Physics.Raycast(ray, out hit, 1000, layerWall))
                {
                    temp.z = hit.point.z;
                    temp.x = hit.point.x + 0.5f;
                    stopPoint = temp;
                }
                break;
        }
    }

    private void Move()
    {
        if (isStop)
        {
            CheckBouncing();
            SwipeDirection();
        }

        stopPoint.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, stopPoint, Time.deltaTime * speed);
    }

    private void CheckBouncing()
    {
        start = transform.position;
        ray = new Ray(start, -transform.up);

        if (Physics.Raycast(ray, out hit, 10000, layerBouncingCorner))
        {
            if (hit.transform.CompareTag("TopLeft"))
            {
                if (dir == Direction.Forward)
                {
                    dir = Direction.Right;
                }
                else
                {
                    dir = Direction.Back;
                }
            }

            if (hit.transform.CompareTag("TopRight"))
            {
                if (dir == Direction.Forward)
                {
                    dir = Direction.Left;
                }
                else
                {
                    dir = Direction.Back;
                }
            }

            if (hit.transform.CompareTag("BottomLeft"))
            {
                if (dir == Direction.Back)
                {
                    dir = Direction.Right;
                }
                else
                {
                    dir = Direction.Forward;
                }
            }

            if (hit.transform.CompareTag("BottomRight"))
            {
                if (dir == Direction.Back)
                {
                    dir = Direction.Left;
                }
                else
                {
                    dir = Direction.Forward;
                }
            }

            FindStopPoint();
        }
    }

    private void CheckPlayerStop()
    {
        if ((transform.position - stopPoint).magnitude < 0.01f)
        {
            isStop = true;
        }
        else
        {
            isStop = false;
        }
    }

    public void ResetStopPoint()
    {
        stopPoint = new Vector3(8.5f, 2.95f, -8.5f);
    }
}
