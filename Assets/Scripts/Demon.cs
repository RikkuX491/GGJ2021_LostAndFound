using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
    // Determines whether the Demon can or cannot move
    public static bool canMove;

    // The speed at which the Demon can move
    public float speed;

    // The Transform of the target that the Demon will follow
    public Transform target;

    // The radius at which the Heartbeat Audio should start playing
    public float radiusToStartHeartbeat;

    // The radius at which the Very Fast Heartbeat Audio should start playing
    public float radiusToStartVeryFastHeartbeat;

    // The background music for the current scene
    public AudioSource currentBackgroundMusic;

    // Play this sound when the Demon has reached the Player
    public AudioSource iFoundYouAudio;

    // Play this sound when the Demon is getting closer to reaching the Player
    public AudioSource heartbeat;

    // Play this sound when the Demon is very close to reaching the Player
    public AudioSource veryFastHeartbeat;

    // Are these audios playing or not?
    bool heartbeatAudioIsPlaying = false;
    bool veryFastHeartbeatAudioIsPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        // At the Start of this part of the game, the Demon can move
        canMove = true;

        // Find the Player Game Object and get the transform for the Player Game Object
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            CheckDistance();
        }
    }

    // Check the distance from the Demon to the Player
    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= radiusToStartVeryFastHeartbeat)
        {
            if (!veryFastHeartbeatAudioIsPlaying)
            {
                StartVeryFastHeartbeat();
            }
        }
        else
        {
            if(Vector3.Distance(target.position, transform.position) <= radiusToStartHeartbeat)
            {
                if (!heartbeatAudioIsPlaying)
                {
                    StartHeartbeat();
                }
            }
            else
            {
                // Make the "heartbeat" audio stop playing
                heartbeat.Stop();

                // heartbeat is not playing anymore
                heartbeatAudioIsPlaying = false;
            }
        }
    }

    void StartHeartbeat()
    {
        // Make the "Very Fast heartbeat" audio stop playing
        veryFastHeartbeat.Stop();

        // veryFastHeartbeat is not playing anymore
        veryFastHeartbeatAudioIsPlaying = false;

        // Play the "heartbeat" audio
        heartbeat.Play(0);

        // heartbeat is now playing
        heartbeatAudioIsPlaying = true;
    }

    void StartVeryFastHeartbeat()
    {
        // Make the "heartbeat" audio stop playing
        heartbeat.Stop();

        // heartbeat is not playing anymore
        heartbeatAudioIsPlaying = false;

        // Play the "Very Fast heartbeat" audio
        veryFastHeartbeat.Play(0);

        // veryFastHeartBeat is now playing
        veryFastHeartbeatAudioIsPlaying = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the Demon collides with the Player...
        if (collision.gameObject.CompareTag("Player"))
        {
            // Stop the Demon from moving
            canMove = false;

            // Stop the player from moving
            PlayerMovement.canMove = false;

            // Make the current background music stop playing
            currentBackgroundMusic.Stop();

            // Make the "Very Fast heartbeat" audio stop playing
            veryFastHeartbeat.Stop();

            // Play the "You Found Me" audio
            iFoundYouAudio.Play(0);
        }
    }
}
