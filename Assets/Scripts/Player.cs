using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    private int coinCount = 0;
    private enum Direction{
        Up,
        Down,
        Left,
        Right
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log("GlobalData from Player Script : " + globalData.playerHasStartedMoving.ToString());
    }

    private void Update()
    {
        scoreText.text = "" + score;
        if (!isHopping)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                globalData.playerHasStartedMoving = true;
                MoveCharacter(Vector3.right, Direction.Up);
                score++;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveCharacter(Vector3.left, Direction.Down);
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

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision detecté :" + collision.collider.tag);
        if (collision.collider.CompareTag("Evil Objects"))
        {
            KillPlayer();
        }
        if (collision.collider.CompareTag("Coin"))
        {
            CoinScript coinScript = collision.gameObject.GetComponentInParent<CoinScript>();
            if (coinScript != null)
            {
                coinCount++;
                if (coinScript.isSpecial)
                {
                    // Traitement spécial pour les pièces spéciales
                }
                Destroy(collision.gameObject);
            }
        }
        if (collision.collider.GetComponent<MovingObject>() != null)
        {
            if (collision.collider.GetComponent<MovingObject>().isLog)
            {
                transform.parent = collision.collider.transform;
            }

        }
        else
        {
            transform.parent = null;
        }
    }

    private void PerformMove(Vector3 difference) {
        animator.SetTrigger("hop");
        isHopping = true;
        int obstacleLayer = LayerMask.NameToLayer("obstacle");
        if (Physics.Raycast(transform.position, difference, out RaycastHit hit, difference.magnitude + 0.1f, obstacleLayer))
        {
            Vector3 slideDirection = Vector3.ProjectOnPlane(difference, hit.normal).normalized;
            float slideAmount = 0.2f;

            transform.position += slideDirection * slideAmount;
            //terrainGenerator.SpawnTerrain(false, transform.position);
        }
        else
        {
            transform.position = transform.position + difference;
            //terrainGenerator.SpawnTerrain(false, transform.position);
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
