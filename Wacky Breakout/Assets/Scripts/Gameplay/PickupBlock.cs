using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Pick Up Block
/// </summary>
public class PickupBlock : Block
{
    // Fields

    PickupEffect pickupBlockType;

    // Sprites
    [SerializeField]
    Sprite freezerSprite;
    [SerializeField]
    Sprite speedupSprite;

    // Event support
    FreezerEffectActivated freezerEvent = new FreezerEffectActivated();
    SpeedupEffectActivated speedupEvent = new SpeedupEffectActivated();



    // Methods

    // Start is called before the first frame update
    override protected void Start()
    {
        // Decide block type
        // Select one of the type from PickUpEffect enum
        // [STACK OVERFLOW]
        pickupBlockType = (PickupEffect)Random.Range(0, System.Enum.GetValues(typeof(PickupEffect)).Length);

        // Select a sprite & invoker event based on type
        if (pickupBlockType == PickupEffect.Freezer)
        {
            // Freezer

            // Choose sprite
            gameObject.GetComponent<SpriteRenderer>().sprite = freezerSprite;

            // Add self as invoker for freezer event
            EventManager.AddFreezerInvoker(this);
        }
        else if (pickupBlockType == PickupEffect.Speedup)
        {
            // Speedup

            // Choose sprite
            gameObject.GetComponent<SpriteRenderer>().sprite = speedupSprite;

            // Add self as invoker for freezer event
            EventManager.AddSpeedupInvoker(this);
        }

        // Run parent's start method
        base.Start();

        // Initialise points
        blockPoints = ConfigurationUtils.PickupBlockPoints;
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Called when something collides with block
    /// </summary>
    /// <param name="collision"></param>
    override protected void OnCollisionEnter2D(Collision2D coll)
    {
        // Activate pickup effect
        if (coll.gameObject.CompareTag("Ball"))
        {
            PickUpEffect(pickupBlockType);
        }

        base.OnCollisionEnter2D(coll);
    }

    /// <summary>
    /// Gives pickup effect to object
    /// </summary>
    /// <param name="pickupBlockType"> block type </param>
    void PickUpEffect(PickupEffect pickupBlockType)
    {
        // [UNFINISHED]

        if (pickupBlockType == PickupEffect.Freezer)
        {
            // Invoke freezer event
            freezerEvent.Invoke(ConfigurationUtils.FreezerEffectDuration);
        }
        else if (pickupBlockType == PickupEffect.Speedup)
        {
            // Add speeedup effect code here
            speedupEvent.Invoke(ConfigurationUtils.SpeedupEffectDuration);
        }
    }

    /// <summary>
    /// Adds a listener for speedup effect event
    /// </summary>
    /// <param name="listener"> listener </param>
    public void AddSpeedUpListener(UnityAction<float> listener)
    {
        speedupEvent.AddListener(listener);
    }

    /// <summary>
    /// Adds a listener for freezer effect event
    /// </summary>
    /// <param name="listener"> listener </param>
    public void AddFreezerListener(UnityAction<float> listener)
    {
        freezerEvent.AddListener(listener);
    }
}
