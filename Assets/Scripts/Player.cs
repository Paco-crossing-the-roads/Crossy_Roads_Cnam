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
        score++;
        scoreText.text = "Score: " + score;
        if (Input.GetKeyDown(KeyCode.K) && !isHopping)
        {

            float zDifference = 0;
            if (transform.position.z % 1 != 0)
            {
                zDifference = Mathf.Round(transform.position.z) - transform.position.z;
            }
            MoveCharacter(new Vector3(1, 0, zDifference));
        }
        else if (Input.GetKeyDown(KeyCode.S) && !isHopping)
        {
            MoveCharacter(new Vector3(0, 0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.D) && !isHopping)
        {
            MoveCharacter(new Vector3(0, 0, -1));
        }
    }

    private void MoveCharacter(Vector3 difference)
    {
        animator.SetTrigger("hop");
        isHopping = true;
        transform.position = (transform.position + difference);
        terrainGenerator.SpawnTerrain(false, transform.position);
    }

    public void FinishHop()
    {
        isHopping = false;
    }
}
