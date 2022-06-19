using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private Tilemap groundTilemap;
    [SerializeField]
    private Tilemap doorTilemap;
    [SerializeField]
    private Tilemap collisionTilemap;
    [SerializeField]
    private Tilemap birdTilemap;
    [SerializeField]
    private TextMeshProUGUI Counter;
    [SerializeField]
    private int moveCounter;
    [SerializeField]
    private LevelLoader levelLoader;



    public bool isMoving = false;

    private InterpolatedMovement interpolatedMovement;

    private int minusCounter = 1;

    private PlayerMovement inputActions;

    private RaycastHit2D hit2D;

    private void Awake()
    {
        inputActions = new PlayerMovement();
        Counter.text = moveCounter.ToString();
        interpolatedMovement = gameObject.GetComponent<InterpolatedMovement>();

        OneWayMovement(new Vector2(-1,0));
        
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }


    void Start()
    {
        inputActions.Main.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    public void RestartScene()
    {
        levelLoader.LoadScene(0);
    }


    public void Move(Vector2 direction, bool IsOneWay = false)
    {

        if (CanMove(direction) && isMoving == false)
        {
            isMoving = true;
            interpolatedMovement.MoveToTarget(transform.position + (Vector3)direction, () => { isMoving = false; });
            if (IsOneWay == false)
            {
                moveCounter--;
                Counter.text = moveCounter.ToString();
            }

        }
        if (moveCounter == 0)
        {
            //endgamehere
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + (Vector3)direction);
        
        if (hit2D = Physics2D.Raycast(transform.position, direction, 1f))
        {
            if (hit2D.collider.tag == "box")
            {
                TilemapColider Box = hit2D.collider.GetComponent<TilemapColider>();
                if (!Box.Move(direction))
                {
                    return false;
                }
            }


        }
        if (birdTilemap.HasTile(gridPosition) && isMoving == false)
        {
            birdTilemap.SetTile(gridPosition, null);
            moveCounter -= minusCounter;
        }
        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition) || doorTilemap.HasTile(gridPosition))
        {
            return false;
        }
        return true;
    }
    

    private void OneWayMovement(Vector2 direction)
    {

    }

}
