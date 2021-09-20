using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// A container for the configuration data
/// </summary>
public class ConfigurationData
{
    #region Fields

    // Config data dictionary
    Dictionary<ConfigurationDataValueName, float> values =
        new Dictionary<ConfigurationDataValueName, float>();

    const string ConfigurationDataFileName = "ConfigurationData.csv";

    // Stream reader and writer
    StreamReader input;
    StreamWriter output;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the paddle move units per second
    /// </summary>
    public float PaddleMoveUnitsPerSecond
    {
        get { return values[ConfigurationDataValueName.paddleMoveUnitsPerSecond]; }
    }

    /// <summary>
    /// Gets the impulse force to apply to move the ball
    /// </summary>
    public float BallImpulseForce
    {
        get { return values[ConfigurationDataValueName.ballImpulseForce]; }
    }

    /// <summary>
    /// Gets the life time seconds for ball
    /// </summary>
    public float BallLifeTimeSeconds
    {
        get { return values[ConfigurationDataValueName.ballLifeTimeSeconds]; }
    }

    /// <summary>
    /// Gets the min value for random ball spawner
    /// </summary>
    public float RandomSpawnerMin
    {
        get { return values[ConfigurationDataValueName.randomSpawnerMin]; }
    }

    /// <summary>
    /// Gets the max value for random ball spawner
    /// </summary>
    public float RandomSpawnerMax
    {
        get { return values[ConfigurationDataValueName.randomSpawnerMax]; }
    }

    /// <summary>
    /// Gets the points value for standard block
    /// </summary>
    public int StandardBlockPoints
    {
        get { return (int)values[ConfigurationDataValueName.standardBlockPoints]; }
    }

    /// <summary>
    /// Gets the points value for bonus block
    /// </summary>
    public int BonusBlockPoints
    {
        get { return (int)values[ConfigurationDataValueName.bonusBlockPoints]; }
    }

    /// <summary>
    /// Gets the points value for pick up block
    /// </summary>
    public int PickupBlockPoints
    {
        get { return (int)values[ConfigurationDataValueName.pickupBlockPoints]; }
    }

    /// <summary>
    /// Gets the probability value for spawning standard block out of 100
    /// </summary>
    public int StandardBlockProbability
    {
        get { return (int)values[ConfigurationDataValueName.standardBlockProbability]; }
    }

    /// <summary>
    /// Gets the probability value for spawning bonus block out of 100
    /// </summary>
    public int BonusBlockProbability
    {
        get { return (int)values[ConfigurationDataValueName.bonusBlockProbability]; }
    }


    /// <summary>
    /// Gets the probability value for spawning standard block out of 100
    /// </summary>
    public int PickupBlockProbability
    {
        get { return (int)values[ConfigurationDataValueName.pickupBlockProbability]; }
    }

    /// <summary>
    /// Gets the total number of balls at the start of the game
    /// </summary>
    public int NoOfBalls
    {
        get { return (int)values[ConfigurationDataValueName.noOfBalls]; }
    }

    /// <summary>
    /// Gets duration of the freezer pickup effect 
    /// </summary>
    public float FreezerEffectDuration
    {
        get { return (float)values[ConfigurationDataValueName.freezerEffectDuration]; }
    }

    /// <summary>
    /// Gets duration of the speedup pickup effect 
    /// </summary>
    public float SpeedupEffectDuration
    {
        get { return (float)values[ConfigurationDataValueName.speedupEffectDuration]; }
    }

    /// <summary>
    /// Gets the multiplier factor for speedup effect
    /// </summary>
    public float SpeedupFactor
    {
        get { return (float)values[ConfigurationDataValueName.speedupFactor]; }
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor
    /// Reads configuration data from a file. If the file
    /// read fails, the object contains default values for
    /// the configuration data
    /// </summary>
    public ConfigurationData()
    {
        // Read and save configuration data from file
        input = null;

        try
        {
            // Create a streamreader object
            input = File.OpenText(Path.Combine(
                Application.streamingAssetsPath, ConfigurationDataFileName));

            // Populate values [VERY TRICKY]
            string currentLine = input.ReadLine();
            while (currentLine != null)
            {
                // Split line into two
                string[] tokens = currentLine.Split(',');

                // Store in the first half of split tokens as value name
                // Parse it to try and match with value from the enum
                // Then type cast it as a config data val for dictionary
                // [VERY TRICKY]
                ConfigurationDataValueName valueName =
                    (ConfigurationDataValueName)Enum.Parse(
                        typeof(ConfigurationDataValueName), tokens[0]);

                // Add value name and then a value(as float)
                values.Add(valueName, float.Parse(tokens[1]));

                // Refresh new line to be processed
                currentLine = input.ReadLine();
            }
        }
        catch (Exception e)
        {
            // Set default values
            SetDefaultValues();
        }
        finally
        {
            // CLose if not null
            if (input != null)
            {
                input.Close();
            }
        }
    }

    /// <summary>
    /// Sets default values for all data in case of error
    /// </summary>
    void SetDefaultValues()
    {
        //Clear dictionary
        values.Clear();

        // Add each value individually
        values.Add(ConfigurationDataValueName.ballImpulseForce, 250);
        values.Add(ConfigurationDataValueName.paddleMoveUnitsPerSecond, 10);
        values.Add(ConfigurationDataValueName.ballLifeTimeSeconds, 20);
        values.Add(ConfigurationDataValueName.randomSpawnerMin, 5);
        values.Add(ConfigurationDataValueName.randomSpawnerMax, 10);
        values.Add(ConfigurationDataValueName.standardBlockPoints, 10);
        values.Add(ConfigurationDataValueName.bonusBlockPoints, 50);
        values.Add(ConfigurationDataValueName.pickupBlockPoints, 20);
        values.Add(ConfigurationDataValueName.standardBlockProbability, 55);
        values.Add(ConfigurationDataValueName.bonusBlockProbability, 15);
        values.Add(ConfigurationDataValueName.pickupBlockProbability, 30);
        values.Add(ConfigurationDataValueName.noOfBalls, 5);
        values.Add(ConfigurationDataValueName.freezerEffectDuration, 1);
        values.Add(ConfigurationDataValueName.speedupEffectDuration, 3);
        values.Add(ConfigurationDataValueName.speedupFactor, 1.5f);
    }

    #endregion
}
