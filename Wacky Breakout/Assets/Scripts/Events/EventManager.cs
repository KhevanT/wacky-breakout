using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    // Fields

    // PointsAddedEvent
    static List<Block> pointsAddedInvokers = new List<Block>();
    static List<UnityAction<int>> pointsAddedListeners = new List<UnityAction<int>>();

    // SpeedupEffectActivated Event
    static List<PickupBlock> speedUpInvokers = new List<PickupBlock>();
    static List<UnityAction<float>> speedUpListeners = new List<UnityAction<float>>();

    // FreezerActivated Event
    static List<PickupBlock> freezerInvokers = new List<PickupBlock>();
    static List<UnityAction<float>> freezerListeners = new List<UnityAction<float>>();

    // Ball Died Event
    static List<Ball> ballDiedInvokers = new List<Ball>();
    static List<UnityAction> ballDiedListeners = new List<UnityAction>();

    // Ball Lost Event
    static List<Ball> ballLostInvokers = new List<Ball>();
    static List<UnityAction> ballLostListeners = new List<UnityAction>();

    // Game Over Event


    // Methods

    // Methods for PointsAdded Event

    /// <summary>
    /// Adds invoker for pointsAddedInvoker
    /// </summary>
    /// <param name="invoker"> invoker block</param>
    public static void AddPointsAddedInvoker(Block invoker)
    {
        // Add invoker to list
        pointsAddedInvokers.Add(invoker);

        // Add invoker as invoker to each listener in list
        foreach (UnityAction<int> listener in pointsAddedListeners)
        {
            invoker.AddPointsAddedListener(listener);
        }
    }

    /// <summary>
    /// Adds listener for pointsAddedInvoker
    /// </summary>
    /// <param name="listener"> listener delegate</param>
    public static void AddPointsAddedListener(UnityAction<int> listener)
    {
        // Add listener to list
        pointsAddedListeners.Add(listener);

        // Add listener as listener to each invoker in list
        foreach (Block invoker in pointsAddedInvokers)
        {
            invoker.AddPointsAddedListener(listener);
        }
    }

    // Methods for SpeedupEffectActivated Event

    /// <summary>
    /// Adds invoker for speedupEffectActivated event
    /// </summary>
    /// <param name="invoker"> pickup block invoker</param>
    public static void AddSpeedupInvoker(PickupBlock invoker)
    {
        // Add invoker to list
        speedUpInvokers.Add(invoker);

        // Add invoker as invoker to each listener in list
        foreach (UnityAction<float> listener in speedUpListeners)
        {
            invoker.AddSpeedUpListener(listener);
        }
    }

    /// <summary>
    /// Adds listener for speedupEffectActivated event
    /// </summary>
    /// <param name="listener"> listener </param>
    public static void AddSpeedupListener(UnityAction<float> listener)
    {
        // Add listener to list
        speedUpListeners.Add(listener);

        // Add listener as listener to each invoker in list
        foreach (PickupBlock invoker in speedUpInvokers)
        {
            invoker.AddSpeedUpListener(listener);
        }
    }

    // Methods for FreezerEffectActivated Event

    /// <summary>
    /// Adds invoker for freezerEffectActivated event
    /// </summary>
    /// <param name="invoker"> pickup block invoker</param>
    public static void AddFreezerInvoker(PickupBlock invoker)
    {
        // Add invoker to list
        freezerInvokers.Add(invoker);

        // Add invoker as invoker to each listener in list
        foreach (UnityAction<float> listener in freezerListeners)
        {
            invoker.AddFreezerListener(listener);
        }
    }

    /// <summary>
    /// Adds listener for freezerEffectActivated event
    /// </summary>
    /// <param name="listener"> listener </param>
    public static void AddFreezerListener(UnityAction<float> listener)
    {
        // Add listener to list
        freezerListeners.Add(listener);

        // Add listener as listener to each invoker in list
        foreach (PickupBlock invoker in freezerInvokers)
        {
            invoker.AddFreezerListener(listener);
        }
    }

    // Methods for BallDied Event

    /// <summary>
    /// Adds invoker for ballDied event
    /// </summary>
    /// <param name="invoker"> ball invoker</param>
    public static void AddBallDiedInvoker(Ball invoker)
    {
        // Add invoker to list
        ballDiedInvokers.Add(invoker);

        // Add invoker as invoker to each listener in list
        foreach (UnityAction listener in ballDiedListeners)
        {
            invoker.AddBallDiedListener(listener);
        }
    }

    /// <summary>
    /// Adds listener for BallDied event
    /// </summary>
    /// <param name="listener"> listener </param>
    public static void AddBallDiedListener(UnityAction listener)
    {
        // Add listener to list
        ballDiedListeners.Add(listener);

        // Add listener as listener to each invoker in list
        foreach (Ball invoker in ballDiedInvokers)
        {
            invoker.AddBallDiedListener(listener);
        }
    }

    // Methods for BallLost Event

    /// <summary>
    /// Adds invoker for ballLost event
    /// </summary>
    /// <param name="invoker"> ball invoker</param>
    public static void AddBallLostInvoker(Ball invoker)
    {
        // Add invoker to list
        ballLostInvokers.Add(invoker);

        // Add invoker as invoker to each listener in list
        foreach (UnityAction listener in ballLostListeners)
        {
            invoker.AddBallLostListener(listener);
        }
    }

    /// <summary>
    /// Adds listener for BallDied event
    /// </summary>
    /// <param name="listener"> listener </param>
    public static void AddBallLostListener(UnityAction listener)
    {
        // Add listener to list
        ballLostListeners.Add(listener);

        // Add listener as listener to each invoker in list
        foreach (Ball invoker in ballLostInvokers)
        {
            invoker.AddBallLostListener(listener);
        }
    }
}
