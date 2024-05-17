using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timeText;
    public GameManagerScript gameManager;
    public GlobalData globalData;
    public AudioClip coinSound;
    public AudioClip deathSound;
    public AudioClip fiftySound;
    private Animator animator;
    private bool isHopping;
    private bool isDead;
    private int score;
    private int lastMultipleOfFifty = 0;
    private float elapsedTime;
    private int scoreBack;

    private enum Direction{
        Up,
        Down,
        Left,
        Right
    }

    private void Start()
    {
        isDead = false;
        animator = GetComponent<Animator>();

        //Debug.Log("GlobalData from Player Script : " + globalData.playerHasStartedMoving.ToString());
        elapsedTime = 0f;

        scoreBack = 0;
        lastMultipleOfFifty = 0;
        score = 0;
        Debug.Log("GlobalData from Player Script : " + globalData.playerHasStartedMoving.ToString());

    }

    private void Update()
    {
        if (!PauseManager.IsPaused)
        {
            int currentMultipleOfFifty = score / 50;
            if (currentMultipleOfFifty > lastMultipleOfFifty)
            {
                SoundManager.instance.PlaySFX(fiftySound);
                lastMultipleOfFifty = currentMultipleOfFifty;
            }

            elapsedTime += Time.deltaTime;

            UpdateTimeText();

            scoreText.text = "" + score;

            if (!isHopping)
            {

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

    private void UpdateTimeText()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
                SoundManager.instance.PlaySFX(coinSound);
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
        if (!isDead)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;

        if (SoundManager.instance != null)
        {
            SoundManager.instance.StopMusic();
            SoundManager.instance.PlaySFX(deathSound);
        }

        globalData.playerScore = score;

        if (gameObject != null)
        {
            gameManager.GameOver();
            Destroy(gameObject);
        }

        Debug.Log("Player has been killed.");
    }
}
