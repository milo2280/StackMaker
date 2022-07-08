using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 temp;
    public GameObject playerStack;

    private GameObject resetFirstStack;

    public void AddStack()
    {
        temp = transform.position;
        temp.y += 0.3f;
        transform.position = temp;
        LevelManager.Instance.score++;
    }

    public void MinusStack()
    {
        temp = transform.position;
        temp.y -= 0.3f;
        transform.position = temp;
        LevelManager.Instance.score--;
    }

    public void ResetPlayer()
    {
        LevelManager.Instance.playerMovement.ResetStopPoint();
        transform.position = new Vector3(8.5f, 2.95f, -8.5f);

        resetFirstStack = Instantiate(playerStack, transform);
        temp = resetFirstStack.transform.localPosition;
        temp.y -= 0.3f;
        resetFirstStack.transform.localPosition = temp;
    }
}
