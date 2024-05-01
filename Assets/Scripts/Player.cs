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

    private Animator animator;
    private bool isHopping;
    private int score;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        scoreText.text = "" + score;
        if (!isHopping)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveCharacterForward(Vector3.right);
                score++;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveCharacter(Vector3.left);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveCharacter(Vector3.forward);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveCharacter(Vector3.back);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Evil Objects"))
        {
            KillPlayer();
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

    private void MoveCharacter(Vector3 difference)
    {
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

    private void MoveCharacterForward(Vector3 difference)
    {
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
            terrainGenerator.SpawnTerrain(false, transform.position);
        }
    }


    public void FinishHop()
    {
        isHopping = false;
    }

    private void KillPlayer()
    {
        Destroy(gameObject);
        gameManager.GameOver();
        Debug.Log("Player has been killed.");
    }
}
