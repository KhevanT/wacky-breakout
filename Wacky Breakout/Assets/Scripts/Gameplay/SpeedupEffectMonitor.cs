using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Monitors whether the speedup effect is active or not
/// </summary>
public class SpeedupEffectMonitor : MonoBehaviour
{
    // Fields

    bool speedupActive = false;
    float timeLeft;
    Timer speedupTimer;


    // Properties

    /// <summary>
    /// Returns time left in speedup effect duration
    /// </summary>
    public float TimeLeft
    {
        get { return timeLeft; }
    }

    /// <summary>
    /// Returns whether the speedup effect is active
    /// </summary>
    public bool SpeedupActive
    {
        get { return speedupActive; }
    }

    // Methods

    // Start is called before the first frame update
    void Start()
    {
        // Add method as listener for Speedup effect event
        EventManager.AddSpeedupListener(SpeedupEffectActivated);

        // Add timer
        speedupTimer = gameObject.AddComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update time left
        timeLeft = speedupTimer.TimeLeft;

        if (speedupTimer.Finished)
        {
            Debug.Log("Speedup effect is over");

            // Stop timer
            speedupTimer.Stop();

            // Reset bool
            speedupActive = false;
        }
    }

    /// <summary>
    /// Updates all monitor values when speedup event is invoked
    /// </summary>
    /// <param name="duration"> duration </param>
    void SpeedupEffectActivated(float duration)
    {
        if (speedupTimer.Running)
        {
            // Add time to running timer
            speedupTimer.AddTime(duration);
        }
        else
        {
            // Start whole process as if its for the first time

            Debug.Log("Speedup effect is applied");

            // Set bool for effect
            speedupActive = true;

            // Run speedup timer
            speedupTimer.Duration = duration;
            speedupTimer.Run();
        }
    }
}
