using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private Text scoreText;

    private Animator animator;
    private bool isHopping;
    private int score;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        scoreText.text = "Score: " + score;
        if (!isHopping)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                MoveCharacter(Vector3.right);
                score++;
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                MoveCharacter(Vector3.left);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                MoveCharacter(Vector3.forward);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                MoveCharacter(Vector3.back);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
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
        transform.position = (transform.position + difference);
        terrainGenerator.SpawnTerrain(false,transform.position);
    }

    public void FinishHop()
    {
        isHopping = false;
    }
}
