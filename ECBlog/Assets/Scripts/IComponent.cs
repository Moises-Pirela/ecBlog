public enum ComponentType
{
    Movement = 0,
    Health,
    Max
}

public interface IComponent
{
    public int OwningEntityId { get; set; }

    public ComponentType GetComponentType();
    public void Tick();
}


