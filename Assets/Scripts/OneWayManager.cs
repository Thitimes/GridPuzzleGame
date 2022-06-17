using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayManager : MonoBehaviour
{

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
    public Vector2 GetOneWayDirection(OneWayDirection direction)
    {
        return vectorDirection[(int)direction];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {
            Debug.Log("yeah");
           StartCoroutine(PushPlayer());
        }
    }

    IEnumerator PushPlayer()
    {
        
        yield return new WaitUntil(() => playerController.isMoving == false);
        playerController.Move(GetOneWayDirection(direction));

    }
}
