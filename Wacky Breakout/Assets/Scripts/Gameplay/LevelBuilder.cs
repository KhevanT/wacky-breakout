using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    // Fields

    // Blocks
    [SerializeField]
    GameObject prefabStandardBlock;
    [SerializeField]
    GameObject prefabBonusBlock;
    [SerializeField]
    GameObject prefabPickupBlock;

    // Paddle
    [SerializeField]
    GameObject prefabPaddle;

    // Block cache values
    float blockWidth;
    float blockHeight;

    // Row support
    int noOfRows;
    int blocksPerRow;
    float leftOffset;
    float topRowOffset;
    float totalBlockWidth;

    // Spawn support
    Vector2 position;
    float screenWidth;
    float screenHeight;
    float screenZ;

    // Methods

    // Start is called before the first frame update
    void Start()
    {
        // Cache block collider values
        GameObject tempBlock = Instantiate<GameObject>(prefabStandardBlock);
        BoxCollider2D collider = tempBlock.GetComponent<BoxCollider2D>();
        float blockWidth = collider.size.x;
        float blockHeight = collider.size.y;
        Destroy(tempBlock);

        // Screen Utils
        screenWidth = ScreenUtils.ScreenRight * 2;
        screenHeight = ScreenUtils.ScreenTop * 2;

        // Calculate initial spawn position for blocks and rows
        // [TRICKY & IMPERFECT]
        noOfRows = 3;
        blocksPerRow = 9; // Ideally here you should use screen width and stuff
        totalBlockWidth = blocksPerRow * blockWidth;
        leftOffset = (ScreenUtils.ScreenLeft +
            (screenWidth - totalBlockWidth) / 2 + blockWidth / 2) / 2;
        topRowOffset = ScreenUtils.ScreenTop - (blockHeight * 3 / 2);

        // Finalise position
        Vector2 position = new Vector2(leftOffset, topRowOffset);

        /*
        // Debug
        Debug.Log("The Screen width is " + screenWidth);
        Debug.Log("The Screen height is " + screenHeight);
        Debug.Log("The block height is " + blockHeight);
        Debug.Log("The block width is " + blockWidth);
        Debug.Log("The number of blocks per row is " + blocksPerRow);
        Debug.Log("Left offset position is " + leftOffset);
        Debug.Log("Screen Left is " + ScreenUtils.ScreenLeft);
        */


        // Build three rows of standard blocks

        // SpawnRows(position, noOfRows, blocksPerRow, 1); [TOO BUGGY, DEBUG NEEDED]

        // Run loop the no. of times as no. of rows
        for (int i = 0; i < noOfRows; i++)
        {
            // Debug.Log("First loop has been run");

            // Run loop as many times as no. of blocks per row
            for (int e = 0; e < blocksPerRow; e++)
            {
                // Debug.Log("Second loop has been run");

                // Spawn random block
                SpawnBlock(position, 0);

                // Debug.Log("Spawned block at" + position);

                // Change x value
                position.x += blockWidth / 2;
            }

            // Reset left offset
            position.x = leftOffset;

            // Change height
            position.y += blockHeight / 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Spawns block at given location
    /// </summary>
    /// <param name="position"> position </param>
    /// <param name="index"> index </param>
    void SpawnBlock(Vector2 position, int index)
    {
        // Index Map
        // 0 -> Random Block
        // 1 -> Standard Block
        // 2 -> Bonus Block
        // 3 -> Pickup Block

        if (index == 0)
        {
            int standardBlockProbability = ConfigurationUtils.StandardBlockProbability;
            int pickupBlockProbability = standardBlockProbability + ConfigurationUtils.PickupBlockProbability;
            // int bonusBlockProbability = (100 - (standardBlockProbability + pickupBlockProbability)) + ConfigurationUtils.BonusBlockProbability;

            // Spawn random block
            int random = Random.Range(0, 101);
            if (random <= standardBlockProbability)
            {
                // Spawn standard block
                SpawnBlock(position, 1);

                // Debug.Log("For Standard Block Random was " + random);
            }
            else if (random <= pickupBlockProbability && random >= standardBlockProbability)
            {
                // Spawn pickup block
                SpawnBlock(position, 3);

                // Debug.Log("For Pickup block Random was " + random);
            }
            else if (random <= 100 && random >= pickupBlockProbability)
            {
                // Spawn bonus block
                SpawnBlock(position, 2);

                // Debug.Log("For Bonus block Random was " + random);
            }

        }
        else if (index == 1)
        {
            // Spawns standard block
            Instantiate(prefabStandardBlock, position, Quaternion.identity);
        }
        else if (index == 2)
        {
            // Spawns bonus block
            Instantiate(prefabBonusBlock, position, Quaternion.identity);
        }
        else if (index == 3)
        {
            // Spawns pickup block
            Instantiate(prefabPickupBlock, position, Quaternion.identity);
        }
    }

    /// <summary>
    /// Spawns a set of rows of blocks
    /// </summary>
    /// <param name="startingPos"> starting position for row </param>
    /// <param name="noOfRows"> no. of rows </param>
    /// <param name="blocksPerRow"> no. of blocks per row </param>
    /// <param name="index"> index for type of block </param>
    void SpawnRows(Vector2 startingPos, int noOfRows, int blocksPerRow, int index)
    {
        //
        // [BUGGY] [DEBUG NEEDED ASAP]
        //

        // Run loop the no. of times as no. of rows
        for (int i = 0; i < noOfRows; i++)
        {
            Debug.Log("First loop has been run");

            // Run loop as many times as no. of blocks per row
            for (int e = 0; e < blocksPerRow; e++)
            {
                Debug.Log("Second loop has been run");

                // Spawn block
                SpawnBlock(startingPos, index);

                Debug.Log("Spawned block at" + position);

                // Change x value
                startingPos.x += blockWidth / 2;
            }

            // Reset left offset
            startingPos.x = leftOffset;

            // Change height
            startingPos.y += blockHeight / 2;
        }
    }
}
