using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // The Player GameObject
    public GameObject player;

    // The difference between the Camera's position and the Player's position
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called once per frame. Player moves first, then camera follows
    void LateUpdate()
    {
        // Update the position of the Camera so that it follows the Player GameObject
        transform.position = player.transform.position + offset;
    }
}
