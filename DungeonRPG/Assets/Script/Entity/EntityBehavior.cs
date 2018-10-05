using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base class for entities.
/// </summary>
public class EntityBehavior : MonoBehaviour
{
    public int Health
    {
        get
        {
            return this.mHealth;
        }
        set
        {
            this.mHealth = value;
        }
    }
    private int mHealth;

    
}
