using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
    // Fields

    // Score support
    protected int blockPoints;
    protected HUD hud;

    // PointsAddedEvent support
    PointsAddedEvent pointsAddedEvent = new PointsAddedEvent();

    // Start is called before the first frame update
    virtual protected void Start()
    {
        // Get HUD
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();

        // Add self as invoker for pointsAddedEvent
        EventManager.AddPointsAddedInvoker(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Called when something collides with block
    /// </summary>
    /// <param name="collision"></param>
    virtual protected void OnCollisionEnter2D(Collision2D coll)
    {
        // Only proceed if it's a ball
        if (coll.gameObject.CompareTag("Ball"))
        {
            // Play sound effect
            AudioManager.Play(AudioClipName.BlockBreak);

            // Add points
            pointsAddedEvent.Invoke(blockPoints);

            // Destroy block
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Adds a listener for speedupeffect event
    /// </summary>
    /// <param name="listener"></param>
    public void AddPointsAddedListener(UnityAction<int> listener)
    {
        pointsAddedEvent.AddListener(listener);
    }
}
