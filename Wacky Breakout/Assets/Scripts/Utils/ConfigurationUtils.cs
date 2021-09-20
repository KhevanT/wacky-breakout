using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides access to configuration data
/// </summary>
public static class ConfigurationUtils
{
    // For config data support
    static ConfigurationData configData;

    #region Properties

    /// <summary>
    /// Gets the paddle move units per second
    /// </summary>
    public static float PaddleMoveUnitsPerSecond
    {
        get { return configData.PaddleMoveUnitsPerSecond; }
    }

    /// <summary>
    /// Gets the impulse force to apply to move the ball
    /// </summary>
    public static float BallImpulseForce
    {
        get { return configData.BallImpulseForce; }
    }

    /// <summary>
    /// Gets the life time seconds for ball
    /// </summary>
    public static float BallLifeTimeSeconds
    {
        get { return configData.BallLifeTimeSeconds; }
    }

    /// <summary>
    /// Gets the min value for random ball spawner
    /// </summary>
    public static float RandomSpawnerMin
    {
        get { return configData.RandomSpawnerMin; }
    }

    /// <summary>
    /// Gets the max value for random ball spawner
    /// </summary>
    public static float RandomSpawnerMax
    {
        get { return configData.RandomSpawnerMax; }
    }
    
    /// <summary>
    /// Gets the points value for standard block
    /// </summary>
    public static int StandardBlockPoints
    {
        get { return configData.StandardBlockPoints; }
    }

    /// <summary>
    /// Gets the points value for bonus block
    /// </summary>
    public static int BonusBlockPoints
    {
        get { return configData.BonusBlockPoints; }
    }

    /// <summary>
    /// Gets the points value for pickup block
    /// </summary>
    public static int PickupBlockPoints
    {
        get { return configData.PickupBlockPoints; }
    }

    /// <summary>
    /// Gets the probability value for spawning standard block out of 100
    /// </summary>
    public static int StandardBlockProbability
    {
        get { return configData.StandardBlockProbability; }
    }

    /// <summary>
    /// Gets the probability value for spawning bonus block out of 100
    /// </summary>
    public static int BonusBlockProbability
    {
        get { return configData.BonusBlockProbability; }
    }

    /// <summary>
    /// Gets the probability value for spawning pickup block out of 100
    /// </summary>
    public static int PickupBlockProbability
    {
        get { return configData.PickupBlockProbability; }
    }

    /// <summary>
    /// Gets the total number of balls at the start of the game
    /// </summary>
    public static int NoOfBalls
    {
        get { return configData.NoOfBalls; }
    }

    /// <summary>
    /// Gets duration for the freezer pickup effect
    /// </summary>
    public static float FreezerEffectDuration
    {
        get { return configData.FreezerEffectDuration; }
    }

    /// <summary>
    /// Gets duration for the freezer pickup effect
    /// </summary>
    public static float SpeedupEffectDuration
    {
        get { return configData.SpeedupEffectDuration; }
    }

    /// <summary>
    /// Gets the multiplier factor for speedup effect
    /// </summary>
    public static float SpeedupFactor
    {
        get { return configData.SpeedupFactor; }
    }


    #endregion

    /// <summary>
    /// Initializes the configuration utils
    /// </summary>
    public static void Initialize()
    {
        // Create a new config data object to store properties
        configData = new ConfigurationData();
    }
}
