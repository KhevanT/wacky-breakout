using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    // Rigidbody support
    Rigidbody2D rb2d;

    // Ball Impulse force
    float BallImpulseForce;

    // Freeze Z Rotation
    RigidbodyConstraints2D pos;

    // Timer support
    Timer deathTimer;
    Timer moveTimer;

    // Spawning support
    BallSpawner ballSpawner;

    // Event support
    SpeedupEffectActivated speedupEvent = new SpeedupEffectActivated();
    FreezerEffectActivated freezerEvent = new FreezerEffectActivated();
    BallDied ballDiedEvent = new BallDied();
    BallLost ballLostEvent = new BallLost();
    Timer speedUpTimer;

    // Start is called before the first frame update
    void Start()
    {
        // Start Death Timer
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = ConfigurationUtils.BallLifeTimeSeconds;
        deathTimer.Run();

        // Start move Timer
        moveTimer = gameObject.AddComponent<Timer>();
        moveTimer.Duration = 1f;
        moveTimer.Run();

        // Ball Spawner support
        ballSpawner = Camera.main.GetComponent<BallSpawner>();

        // Obtain rigidbody
        rb2d = GetComponent<Rigidbody2D>();

        // Freeze rotation [POSSIBLY BUGGY]
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Cache collider values
        // done in BallSpawner.cs

        // Add self as invoker to BallDied & BallLost events
        EventManager.AddBallDiedInvoker(this);
        EventManager.AddBallLostInvoker(this);

        // Add method as listener to speedup event
        EventManager.AddSpeedupListener(ActivateSpeedupEffect);
        speedUpTimer = gameObject.AddComponent<Timer>();
    }


    // Update is called once per frame
    void Update()
    {
        // Test
        // Make ball move after moveTimer
        if (moveTimer.Finished)
        {
            // Stop timer
            moveTimer.Stop();

            // Move ball
            MoveBall();
        }

        // Destroy ball when timer is finished
        if (deathTimer.Finished)
        {
            // Stop timer
            moveTimer.Stop();

            // Invoke ball died event
            ballDiedEvent.Invoke();

            // Send debug message
            Debug.Log("Ball has been destroyed");

            // Destroy ball
            Destroy(gameObject);
        }

        // Check for speedup timer
        if (speedUpTimer.Finished && !EffectUtils.SpeedupActive)
        {
            // Return balls to original speed
            rb2d.velocity *= 1 / ConfigurationUtils.SpeedupFactor;

            // Stop timer
            speedUpTimer.Stop();
        }
    }

    /// <summary>
    /// Gets ball moving
    /// </summary>
    void MoveBall()
    {
        if (EffectUtils.SpeedupActive)
        {
            // Move ball with speedup 

            // Create vector for direction
            BallImpulseForce = ConfigurationUtils.BallImpulseForce;
            Vector2 direction = new Vector2(
                (float)(Random.Range(-1, 1)), -1) * BallImpulseForce;

            // Add force to rigidbody
            rb2d.AddForce(direction, ForceMode2D.Force);

            // Speed up ball
            ActivateSpeedupEffect(EffectUtils.SpeedupTimeLeft);
        }
        else
        {
            // Move ball without speedup

            // Create vector for direction
            BallImpulseForce = ConfigurationUtils.BallImpulseForce;
            Vector2 direction = new Vector2(
                (float)(Random.Range(-1, 1)), -1) * BallImpulseForce;

            // Add force to rigidbody
            rb2d.AddForce(direction, ForceMode2D.Force);
        }
    }

    /// <summary>
    /// Find new direction after collision
    /// </summary>
    /// <param name="direction"></param>
    public void SetDirection(Vector2 direction)
    {
        // Set rigidbody's velocity to current speed multiplied
        // by new direction (bit tricky and so I referred to solution)
        float speed = rb2d.velocity.magnitude;
        rb2d.velocity = direction * speed;
    }

    /// <summary>
    /// Called when object enters collision
    /// </summary>
    /// <param name="coll"></param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        // Freeze Z Axis rotation
        pos = RigidbodyConstraints2D.FreezeRotation;

        if (!coll.gameObject.CompareTag("Block"))
        {
            // Play sound effect
            AudioManager.Play(AudioClipName.BallBounce);
        }
    }

    /// <summary>
    /// Called when object goes out of view
    /// </summary>
    void OnBecameInvisible()
    {
        // Spawn new ball

        // Invoke ball lost event
        ballLostEvent.Invoke();

        // Send debug message
        Debug.Log("Ball has been destroyed");

        // Destroy ball
        Destroy(gameObject);
    }

    /// <summary>
    /// Adds a listener for ballDied event
    /// </summary>
    /// <param name="listener"> listener </param>
    public void AddBallDiedListener(UnityAction listener)
    {
        ballDiedEvent.AddListener(listener);
    }


    /// <summary>
    /// Adds a listener for ballLost event
    /// </summary>
    /// <param name="listener"> listener </param>
    public void AddBallLostListener(UnityAction listener)
    {
        ballLostEvent.AddListener(listener);
    }

    /// <summary>
    /// Speeds up all balls for a given duration
    /// </summary>
    /// <param name="duration"> duration </param>
    void ActivateSpeedupEffect(float duration)
    {
        if (speedUpTimer.Running && EffectUtils.SpeedupActive)
        {
            // Add extra time to timer
            speedUpTimer.AddTime(duration);
        }
        else
        {
            // Add speed to all balls
            // Add force to rigidbody
            rb2d.velocity *= ConfigurationUtils.SpeedupFactor;

            // Activate speedup timer
            speedUpTimer.Duration = duration;
            speedUpTimer.Run();
        }
    }
}
