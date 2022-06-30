using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayManager : MonoBehaviour
{

    private Animator animator;
    public enum OneWayDirection {Left,Right,Up,Down };
    static readonly Vector2[] vectorDirection = new Vector2[]
    {
        Vector2.left,
        Vector2.right,
        Vector2.up,
        Vector2.down
        
    };
    public OneWayDirection direction;
    public PlayerController playerController;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        setAnimation(direction);
    }
    public Vector2 GetOneWayDirection(OneWayDirection direction)
    {
        return vectorDirection[(int)direction];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {
           StartCoroutine(PushPlayer());
        }
    }

    IEnumerator PushPlayer()
    {
        
        yield return new WaitUntil(() => playerController.isMoving == false);
        playerController.Move(GetOneWayDirection(direction),true);

    }

    private void setAnimation(OneWayDirection x)
    {
        if(x == OneWayDirection.Left)
        {
            animator.SetTrigger("Left");
        }
        if (x == OneWayDirection.Right)
        {
            animator.SetTrigger("Right");
        }
        if (x == OneWayDirection.Down)
        {
            animator.SetTrigger("Down");
        }
        if (x == OneWayDirection.Up)
        {
            animator.SetTrigger("Up");
        }
    }
}
