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
    [SerializeField]
    private Color[] counterColor;
    [SerializeField]
    private SpineAnimationController spine;



    public bool isMoving = false;
    private bool isPushing = false;
    private Transform transformScale;
    private InterpolatedMovement interpolatedMovement;

    private int minusCounter = 1;

    private PlayerMovement inputActions;

    private RaycastHit2D hit2D;

    private void Awake()
    {
        inputActions = new PlayerMovement();
        Counter.text = moveCounter.ToString();
        interpolatedMovement = gameObject.GetComponent<InterpolatedMovement>();
        transformScale = GetComponent<Transform>();
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
        Counter.color = counterColor[0];
    }

    public void RestartScene()
    {
        levelLoader.LoadScene(0);
    }


    public void Move(Vector2 direction, bool IsOneWay = false)
    {

        changeCounterColor();
        if (CanMove(direction) && isMoving == false)
        {
            isMoving = true;
            if (isPushing == false)
            {
                playMoveAnim(direction);
            }
            interpolatedMovement.MoveToTarget(transform.position + (Vector3)direction, () => { isMoving = false; });
            if (IsOneWay == false)
            {
                moveCounter--;
                Counter.text = moveCounter.ToString();
            }
            
        }
        isPushing = false;
        if (moveCounter < 0)
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
            if (hit2D.collider.tag == "box" && isMoving == false)
            {
                TilemapColider Box = hit2D.collider.GetComponent<TilemapColider>();
                if (!Box.Move(direction))
                {
                    return false;
                }
                else
                {
                    isPushing = true;
                    playPushAnim(direction);
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
    

  private void changeCounterColor()
    {
        if(moveCounter == 20)
        {
            Counter.color = counterColor[1];
        }
        else if(moveCounter == 10)
        {
            Counter.color = counterColor[2];
        }
    }

    void playMoveAnim(Vector2 direction)
    {
        if (direction == new Vector2(-1, 0))
        {
            transform.localScale = new Vector3(1, 1, 1);      
            
        }
        if (direction == new Vector2(1, 0))
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        spine.playWalkAnimation(direction);
    }
    void playPushAnim(Vector2 direction)
    {
        if (direction == new Vector2(-1, 0))
        {
            transform.localScale = new Vector3(1, 1, 1);

        }
        if (direction == new Vector2(1, 0))
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        spine.playPushAnimation(direction);
    }
}
