using Assets.Scripts;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 startingOffset;
    [SerializeField] private float smoothness;

    public GlobalData globalData;
    public bool autoMove = false; // Initially set to false
    public float speed = 0.25f;

    private bool playerHasStartedMoving = false; // Flag to track if the player has started moving

    private void Start() {
        if (player != null)
        {

            // Initial positioning of the camera relative to the player
            transform.position = Vector3.Lerp(transform.position, player.transform.position + startingOffset, smoothness);
        }
    }

    private void Update()
    {
        if (!PauseManager.IsPaused)
        {
            if (player != null)
            {
                // Check if the player has started moving
                if (!playerHasStartedMoving && player.GetComponent<Rigidbody>().velocity.magnitude > 0.1f)
                {
                    playerHasStartedMoving = true; // Set the flag to true once the player starts moving
                }

                if (globalData.playerHasStartedMoving == true)
                {
                    // Move the camera horizontally towards the player
                    float newX = Mathf.Lerp(transform.position.x, player.transform.position.x + offset.x, Time.deltaTime * speed);
                    // Keep the depth (z position) constant if not specified otherwise
                    float newZ = transform.position.z;
                    // Update the camera's position
                    transform.position = new Vector3(newX, transform.position.y, newZ);
                }
                else
                {
                    // // Smoothly follow the player's position with the specified offset
                    // Vector3 newPosition = Vector3.Lerp(transform.position, player.transform.position + offset, Time.deltaTime * smoothness);
                    // // Ensure the camera's y position remains constant unless specified otherwise
                    // newPosition.y = transform.position.y;
                    // transform.position = newPosition;
                    transform.position = Vector3.Lerp(transform.position, player.transform.position + startingOffset, smoothness);
                }
            }
        }
    }
}
