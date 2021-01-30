using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Speed of the Player's movement
    public float speed;

    // The Rigidbody2D component for the Player Game Object
    private Rigidbody2D rb;

    // The change in position for the Player Game Object which is obtained from the input
    private Vector3 changeInPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Set rb to the value of the Rigidbody2D component for the Player Game Object
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Every frame - Reset the change in position to 0
        changeInPosition = Vector3.zero;

        // Get horizontal and vertical input, which will be the change in position for the Player
        changeInPosition.x = Input.GetAxisRaw("Horizontal");
        changeInPosition.y = Input.GetAxisRaw("Vertical");

        // If there is input, move the player
        if(changeInPosition != Vector3.zero)
        {
            MovePlayer();
        }
    }

    /**
     * Method name: MovePlayer
     * Description: The method for movement of the Player Game Object
     **/
    void MovePlayer()
    {
        // Move the Player based on the Player's change in position (obtained from the input) and the Player's speed
        rb.MovePosition(transform.position + (changeInPosition * speed * Time.deltaTime));
    }
}
