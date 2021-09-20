using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bonus Block
/// </summary>
public class BonusBlock : Block
{
    // Start is called before the first frame update
    override protected void Start()
    {
        // Call start method of parent
        base.Start();

        blockPoints = ConfigurationUtils.BonusBlockPoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
