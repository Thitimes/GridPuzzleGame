using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapColider : MonoBehaviour
{
    [SerializeField]
    private Tilemap doorTilemap;
    [SerializeField]
    private Tilemap groundTilemap;
    [SerializeField]
    private Tilemap birdTilemap;
    [SerializeField]
    private Tilemap collisionTilemap;
    [SerializeField]
    private SpineAnimationController spineAnimation;

    private InterpolatedMovement interpolatedMovement;

    private RaycastHit2D hit2D;

    private bool isMoving = false;
    private void Awake()
    {
        interpolatedMovement = gameObject.GetComponent<InterpolatedMovement>();
    }
    public bool Move(Vector2 direction)
    {
       
        if (CanMove(direction) && isMoving == false)
        {
            isMoving = true;
            if (birdTilemap.HasTile(getGridPosition(direction)))
            {
                birdTilemap.SetTile(getGridPosition(direction), null);
            }
            spineAnimation.playWalkAnimation(direction);
            interpolatedMovement.MoveToTarget(transform.position + (Vector3)direction, () => { isMoving = false;});

            return true;
        }
        return false;

    }
    public bool CanMove(Vector2 direction)
    {
        if (hit2D = Physics2D.Raycast(transform.position, direction, 1f))
        {
            if (hit2D.collider.tag == "box")
            {
                return false;
            }
        }

            if (!groundTilemap.HasTile(getGridPosition(direction)) || collisionTilemap.HasTile(getGridPosition(direction)) || doorTilemap.HasTile(getGridPosition(direction)))
            {
                return false;
            }
            return true;
        
      
    }
    private Vector3Int getGridPosition(Vector2 direction)
    {
        return groundTilemap.WorldToCell(transform.position + (Vector3)direction);
    }
}
