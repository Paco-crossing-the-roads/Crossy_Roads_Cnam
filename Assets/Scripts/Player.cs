using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private Text scoreText;

    public GameManagerScript gameManager;

    public GlobalData globalData;

    private Animator animator;
    private bool isHopping;
    private int score;
    private int scoreBack;
    private enum Direction{
        Up,
        Down,
        Left,
        Right
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        scoreBack = 0;
        Debug.Log("GlobalData from Player Script : " + globalData.playerHasStartedMoving.ToString());
    }

    private void Update()
    {
        if (!PauseManager.IsPaused)
        {
            scoreText.text = "" + score;

            if (!isHopping)
            {
                float currentPositionX = transform.position.x;

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    globalData.playerHasStartedMoving = true;
                    MoveCharacter(Vector3.right, Direction.Up);
                    if (scoreBack == 0)
                    {
                        score++;
                    }
                    else
                    {
                        scoreBack--;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    MoveCharacter(Vector3.left, Direction.Down);
                    scoreBack++;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    MoveCharacter(Vector3.forward, Direction.Left);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    MoveCharacter(Vector3.back, Direction.Right);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Evil Objects"))
        {
            KillPlayer();
        }
        if (collision.collider.CompareTag("Coin"))
        {
            CoinScript coinScript = collision.gameObject.GetComponentInParent<CoinScript>();
            if (coinScript != null)
            {
                score += coinScript.isSpecial ? 10 : 5;
                Destroy(collision.gameObject);
            }
        }
        if (collision.collider.GetComponent<MovingObject>() != null)
        {
            if (collision.collider.GetComponent<Log>())
            {
                transform.parent = collision.collider.transform;
            }

        }
        else
        {
            transform.parent = null;
        }
    }

    private void RectifPosition() {
        Vector3 currentPosition = transform.position;

        Vector3 roundedPosition = new Vector3(
            Mathf.RoundToInt(currentPosition.x),
            Mathf.RoundToInt(currentPosition.y),
            Mathf.RoundToInt(currentPosition.z)
        );

        transform.position = roundedPosition;
    }

    private void PerformMove(Vector3 difference) {
        animator.SetTrigger("hop");
        isHopping = true;

        string obstacleTag = "obstacle";
        if (Physics.Raycast(transform.position, difference, out RaycastHit hit, difference.magnitude + 0.1f)
            && hit.collider.gameObject.CompareTag(obstacleTag))
        {
                Vector3 slideDirection = Vector3.ProjectOnPlane(difference, hit.normal).normalized;
                float slideAmount = 0.2f;

                transform.position += slideDirection * slideAmount;
                RectifPosition();
        }
        else
        {
            transform.position = transform.position + difference;

            RectifPosition();
        }
    }


    private void MoveCharacter(Vector3 difference, Direction direction)
    {
        PerformMove(difference);

        // Rotate the character to face the direction of movement
        float angle = 0;
        switch (direction)
        {
            case Direction.Up:
                angle = 0;
                terrainGenerator.SpawnTerrain(false, transform.position);
                break;
            case Direction.Down:
                angle = 180;
                break;
            case Direction.Left:
                angle = 270;
                break;
            case Direction.Right:
                angle = 90;
                break;
        }
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    public void FinishHop()
    {
        isHopping = false;
    }

    void OnBecameInvisible()
    {
        KillPlayer();
    }

    private void KillPlayer()
    {
        if (gameObject != null) {
            Destroy(gameObject);
            gameManager.GameOver();
            Debug.Log("Player has been killed.");
        }
    }
}
