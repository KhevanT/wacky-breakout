using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns new balls in game
/// </summary>
public class BallSpawner : MonoBehaviour
{
    // Fields

    [SerializeField]
    GameObject prefabBall;

    // Timer support
    Timer randomSpawnTimer;

    // Spawn location support
    Vector2 spawnLocationMin;
    Vector2 spawnLocationMax;
    bool retrySpawn = false;

    // Balls Left support
    HUD hud;


    // Start is called before the first frame update
    void Start()
    {
        // Get HUD script
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();

        // Initialise timer
        randomSpawnTimer = gameObject.AddComponent<Timer>();
        randomSpawnTimer.Duration = Random.Range(
            ConfigurationUtils.RandomSpawnerMin, ConfigurationUtils.RandomSpawnerMax);
        randomSpawnTimer.Run();

        // Cache collider values
        GameObject tempBall = Instantiate<GameObject>(prefabBall);
        BoxCollider2D collider = tempBall.GetComponent<BoxCollider2D>();
        float ballColliderHalfWidth = collider.size.x / 2;
        float ballColliderHalfHeight = collider.size.y / 2;

        // Spawn location configuration [FROM MATERIALS]
        // [BIT TRICKY]
        spawnLocationMin = new Vector2(
            tempBall.transform.position.x - ballColliderHalfWidth,
            tempBall.transform.position.y - ballColliderHalfHeight);
        spawnLocationMax = new Vector2(
            tempBall.transform.position.x + ballColliderHalfWidth,
            tempBall.transform.position.y + ballColliderHalfHeight);
        Destroy(tempBall);

        // Spawn first ball
        GameObject firstBall = Instantiate<GameObject>(prefabBall);
        // SpawnBall();
        Debug.Log("Spawning first ball");

        // Add SpawnBall as listener for ballDied and ballLost events
        EventManager.AddBallDiedListener(SpawnBall);
        EventManager.AddBallLostListener(SpawnBall);
    }

    // Update is called once per frame
    void Update()
    {
        if(hud.BallsLeft > 0)
        {
            // Spawn a bear every 5-10 seconds
            if (randomSpawnTimer.Finished)
            {
                // Stop timer
                randomSpawnTimer.Stop();

                // Spawn ball
                SpawnBall();

                // Rerun timer
                randomSpawnTimer.Run();
            }
        }
        
        // Check for retry of spawn ball
        if (retrySpawn)
        {
            SpawnBall();
        }
    }

    /// <summary>
    /// Spawns a ball
    /// </summary>
    void SpawnBall()
    {
        if(hud.BallsLeft > 0)
        {
            // Check for collision
            // [BIT TRICKY]
            if (Physics2D.OverlapArea(spawnLocationMin, spawnLocationMax) == null)
            {
                // If no collision, spawn a ball

                // Spawn a ball
                Instantiate(prefabBall);

                // Update balls left
                hud.UpdateBallsLeft();

                // Send debug message
                Debug.Log("Ball has been spawned");

                // No need to retry
                retrySpawn = false;

            }
            else
            {
                // Retry later
                retrySpawn = true;
                Debug.Log("Retrying to spawn ball");
            }
        }
    }
}
