using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IComponent
{
    public bool IsDead = false;
    public float MaxHealth;
    public float CurrentHealth;

    private int owningEntityId = -1;
    public int OwningEntityId { get => owningEntityId; set => owningEntityId = value; }

    public ComponentType GetComponentType()
    {
        return ComponentType.Health;
    }

    public void Tick()
    {
        if (IsDead) return;

        IsDead = CurrentHealth <= 0;
    }
}


