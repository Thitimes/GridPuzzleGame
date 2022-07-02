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
    [SerializeField]
    private ParticleSystem particle;



    public bool isMoving = false;
    private bool isPushing = false;
    private bool isDead = false;
    private bool isShock = false;

    private CorrectAnswer correctAnswer;
    private InterpolatedMovement interpolatedMovement;

    private int minusCounter = 2;

    private PlayerMovement inputActions;

    private RaycastHit2D hit2D;

    private void Awake()
    {
        inputActions = new PlayerMovement();
        Counter.text = moveCounter.ToString();
        interpolatedMovement = gameObject.GetComponent<InterpolatedMovement>();
        if (GameObject.Find("CorrectAnswer"))
        {
            correctAnswer = GameObject.Find("CorrectAnswer").GetComponent<CorrectAnswer>();
        }
        if(correctAnswer != null && correctAnswer.getBool() == true)
        {
            moveCounter += 2;
            Counter.text = moveCounter.ToString();
        }
        isMoving = false;
        isPushing = false;
        isDead = false;
        isShock = false;
        if (TryGetComponent(out CorrectAnswer answer))
        {
            if (answer.getBool() == true)
            {
                moveCounter += 2;
            }
        }
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
        if (isMoving == false)
        {
            isDead = true;
            isMoving = true;
            PlayDeadAnim();
        }
    }


    public void Move(Vector2 direction, bool IsOneWay = false)
    {

        changeCounterColor();
       
        if (CanMove(direction) && isMoving == false)
        {
            isMoving = true;
            if (moveCounter <= 0 && isDead ==false)
            {
                RestartScene();

                return;
            }

            if (isPushing == false && isShock == false && isDead == false)
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
            isShock = true;
            changeScale(direction);
            moveCounter -= minusCounter;
            particle.Clear();
            Counter.text = moveCounter.ToString();
            particle.transform.position = this.transform.position + (Vector3)direction;
            particle.Play();
            interpolatedMovement.MoveToTarget(transform.position + (Vector3)direction);
            playShockAnim();
            birdTilemap.SetTile(gridPosition, null);
            
            
        }
        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition) || doorTilemap.HasTile(gridPosition))
        {
            return false;
        }
        return true;
    }
    
    public void playCrowParticle( Vector2 direction)
    {
        particle.Clear();
        particle.transform.position = this.transform.position + (Vector3)direction;
        particle.Play();
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
        changeScale(direction);
            spine.playWalkAnimation(direction);
        
    }
    void playPushAnim(Vector2 direction)
    {
        changeScale(direction);
        spine.playPushAnimation(direction);
    }
    void changeScale(Vector2 direction)
    {
        if (direction == new Vector2(-1, 0))
        {
            transform.localScale = new Vector3(1, 1, 1);

        }
        if (direction == new Vector2(1, 0))
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    void PlayDeadAnim()
    {
        StartCoroutine(DeadAnim());
    }
    IEnumerator DeadAnim()
    {
        spine.playDeadAnimation();
        yield return new WaitForSeconds(2.2f);
        levelLoader.LoadScene(0);
    }

   public  void playShockAnim()
    {
        
        StartCoroutine(ShockAnim());
    }
    IEnumerator ShockAnim()
    {
        isMoving = true;
        spine.playShockAnimation();
        yield return new WaitForSeconds(0.8f);
        spine.SetCharacterIdle();
        isMoving = false;
        isShock = false;
        
    }
}
