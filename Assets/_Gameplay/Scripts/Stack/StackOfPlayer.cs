using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackOfPlayer : MonoBehaviour
{
    private GameObject newStack;
    private Vector3 temp;

    private int numberOfStack = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StackAdd"))
        {
            AddStack();
        }

        if (other.CompareTag("StackMinus"))
        {
            MinusStack();
        }

        if (other.CompareTag("Finish"))
        {
            LevelManager.Instance.EndLevel();
            ClearStack();
        }

        if (other.CompareTag("BouncingCorner"))
        {
            //TODO: bounce player
            //LevelManager.Instance.playerMovement.Bouncing();
        }
    }

    private void AddStack()
    {
        temp = transform.localPosition;
        temp.y -= 0.3f;

        newStack = Instantiate(gameObject, transform.parent);
        newStack.transform.localPosition = temp;
        newStack.name = newStack.name.Replace("(Clone)", "");

        LevelManager.Instance.player.AddStack();
    }

    private void MinusStack()
    {
        Destroy(gameObject);

        LevelManager.Instance.player.MinusStack();
    }

    private void ClearStack()
    {
        numberOfStack = LevelManager.Instance.score;

        for (int i = numberOfStack; i > 0; i--)
        {
            Destroy(LevelManager.Instance.player.transform.GetChild(i).gameObject);
            LevelManager.Instance.player.MinusStack();
        }
    }
}
