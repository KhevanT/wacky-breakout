using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EffectUtils
{
    // Fields
    static SpeedupEffectMonitor speedupEffectMonitor;

    // Properties

    /// <summary>
    /// Returns the time left for the speedup event
    /// </summary>
    public static float SpeedupTimeLeft
    {
        get { return speedupEffectMonitor.TimeLeft; }
    }

    /// <summary>
    /// Returns whether the speedup effect is still active
    /// </summary>
    public static bool SpeedupActive
    {
        get { return speedupEffectMonitor.SpeedupActive; }
    }

    // Methods
    public static void Initialize()
    {
        // [DEBUG] gives error, cannot add monobehaviours with new
        speedupEffectMonitor = new SpeedupEffectMonitor();
    }
}
