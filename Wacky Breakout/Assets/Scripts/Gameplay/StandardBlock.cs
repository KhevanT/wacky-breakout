using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Standard Block
/// </summary>
public class StandardBlock : Block
{
    // Fields

    // Start is called before the first frame update
    override protected void Start()
    {
        // Call start method of parent
        base.Start();

        // Initialise points
        blockPoints = ConfigurationUtils.StandardBlockPoints;

        // Randomly select one of the standard block sprites
        // [CANCELLED]
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
