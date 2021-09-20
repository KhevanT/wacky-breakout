using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Fields

    // Player Speed
    float MoveUnitsPerSecond;

    // Clamping Support
    float halfColliderWidth;
    float halfColliderHeight;

    // Rigidbody Support
    Rigidbody2D rb2d;

    // Ball bounce support
    const float bounceAngleHalfRange = 60 * Mathf.PI / 180;

    // Frozen effect support
    bool frozen;
    Timer frozenTimer;
    [SerializeField]
    Sprite frozenSprite;
    [SerializeField]
    Sprite defaultSprite;

    // Speed up effect support
    Timer speedUpTimer;
    [SerializeField]
    Sprite speedUpSprite;

    // Start is called before the first frame update
    void Start()
    {
        // Frozen effect support
        frozen = false;
        frozenTimer = gameObject.AddComponent<Timer>();

        // speedup effect support
        speedUpTimer = gameObject.AddComponent<Timer>();

        // Clamping Support
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        halfColliderWidth = collider.size.x / 2;
        halfColliderHeight = collider.size.y / 2;

        // Get rigidbody
        rb2d = gameObject.GetComponent<Rigidbody2D>();

        // Player Speed
        MoveUnitsPerSecond = ConfigurationUtils.PaddleMoveUnitsPerSecond;

        // Add self as listener for freezerActivatedEvent
        EventManager.AddFreezerListener(ActivateFreezerEffect);

        // Add self as listener for fspeedUpActivatedEvent
        EventManager.AddSpeedupListener(ActivateSpeedupEffect);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // Basic movement
        MovePosition();

        // Check for frozen timer
        if (frozenTimer.Finished)
        {
            // Set frozen false
            frozen = false;

            // Reset sprite
            gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;

            // Stop timer
            frozenTimer.Stop();
        }

        if (speedUpTimer.Finished)
        {
            // Reset sprite
            gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;

            // Stop timer
            speedUpTimer.Stop();
        }
    }

    /// <summary>
    /// Move player based on horizontal input
    /// </summary>
    void MovePosition()
    {
        // Basic Movement

        // Detect horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");

        // Move sideways if input detected
        if (horizontalInput != 0 && frozen == false)
        {
            // Get position
            Vector2 position = rb2d.position;

            // Decide velocity
            Vector2 velocity = new Vector2(horizontalInput * MoveUnitsPerSecond * Time.fixedDeltaTime, 0);

            // Check for clamping
            position.x = CalculateClampedX(position.x);

            // Move Rigidbody
            rb2d.MovePosition(position + velocity);
        }
    }

    /// <summary>
    /// Calculates clamped version of given x value 
    /// </summary>
    /// <param name="xValue"> Position X value of player</param>
    /// <returns></returns>
    float CalculateClampedX(float xValue)
    {
        // Check if the x value is currently out of bounds
        if (xValue - halfColliderWidth < ScreenUtils.ScreenLeft)
        {
            // Move rightwards
            xValue = ScreenUtils.ScreenLeft + halfColliderWidth;
        }
        else if (xValue + halfColliderWidth > ScreenUtils.ScreenRight)
        {
            // Move leftwards
            xValue = ScreenUtils.ScreenRight - halfColliderWidth;
        }

        return xValue;
    }

    /// <summary>
    /// Called when player enters in a collision
    /// </summary>
    /// <param name="collision"> collision object</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        // This whole section of code was taken from given materials
        // I don't understand the angle calculation part tbh

        // Proceed if collision is a ball and if its a top collision
        if (coll.gameObject.CompareTag("Ball") &&
            CheckTopCollision(coll))
        {
            // Calculate new direction for ball
            float ballOffsetFromPaddleCenter = transform.position.x -
                coll.transform.position.x;
            float normalizedBallOffset = ballOffsetFromPaddleCenter /
                halfColliderWidth;
            float angleOffset = normalizedBallOffset * bounceAngleHalfRange;
            float angle = Mathf.PI / 2 + angleOffset;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            // Tell ball to set direction to new direction
            Ball ballScript = coll.gameObject.GetComponent<Ball>();
            ballScript.SetDirection(direction);
        }
    }

    /// <summary>
    /// Method to check if collision has occured with top of collider
    /// </summary>
    /// <param name="coll">collision object</param>
    /// <returns>true if top collision and false if not</returns>
    bool CheckTopCollision(Collision2D coll)
    {
        // STILL GIVES ERROR
        // [UNFINISHED]

        // Tolerance value
        const float tol = 0.05f;

        // I HAVE VERY LITTLE IDEA ON HOW TO USE CONTACT POINTS
        // THE CODE BELOW IS FROM THE SOLUTION
        // AND MY FEEBLE ATTEMPT TO EXPLAIN THE CALCULATION

        // Use the contact points of colliders to
        // Check for top collision
        ContactPoint2D[] contacts = coll.contacts;

        // We know(not really), if both contact points are on same y location, it is top collision
        // And here we use that idea to compare the first and second contact points
        // Using a small tolerance value and the absolute(non negative) value of their difference
        // And then returning if it's true or false
        return (Mathf.Abs(contacts[0].point.y - contacts[1].point.y) < tol);
    }

    /// <summary>
    /// Activates freezer effect that stops paddle in place for given duration
    /// </summary>
    /// <param name="duration"> duration </param>
    void ActivateFreezerEffect(float duration)
    {
        // [BUG]
        // May conflict with speedup sprite if both blocks are hit

        if (frozenTimer.Running)
        {
            // Add duration to timer
            frozenTimer.AddTime(duration);
        }
        else
        {
            // Activate effect normally
            // Set frozen status
            frozen = true;

            // Switch out to frozen sprite
            gameObject.GetComponent<SpriteRenderer>().sprite = frozenSprite;

            // Activate frozen timer
            frozenTimer.Duration = duration;

            frozenTimer.Run();
        }
    }

    /// <summary>
    /// Activates speedup effect that increases ball speed
    /// </summary>
    /// <param name="duration"> duration </param>
    void ActivateSpeedupEffect(float duration)
    {
        // [BUG]
        // May conflict with frozen sprite if both blocks are hit

        if (speedUpTimer.Running)
        {
            // Add extra time to timer
            speedUpTimer.AddTime(duration);
        }
        else
        {
            // Switch out to frozen sprite
            gameObject.GetComponent<SpriteRenderer>().sprite = speedUpSprite;

            // Run timer
            speedUpTimer.Duration = duration;
            speedUpTimer.Run();
        }
    }
}
