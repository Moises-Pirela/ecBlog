using UnityEngine;

public class MovementComponent : MonoBehaviour, IComponent
{
    public Vector3 MovementVector;
    public float MovementSpeed;
    private int owningEntityId = -1;
    public int OwningEntityId { get => owningEntityId; set => owningEntityId = value; }

    public ComponentType GetComponentType()
    {
        return ComponentType.Movement;
    }

    public void Tick()
    {
        Vector3 targetPosition = transform.position + MovementVector * MovementSpeed * Time.deltaTime;

        transform.position = targetPosition;
    }
}


