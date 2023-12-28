using UnityEngine;

public class World : MonoBehaviour
{
    public const int MAX_ENTITIES = 200;

    public EntityContainer EntityContainer;

    public void Awake()
    {
        EntityContainer = new EntityContainer();
    }

    public void Start()
    {
        var playerEx = GameObject.Instantiate(Resources.Load("PlayerExample")) as GameObject;

        EntityContainer.CreateEntity(playerEx);
    }

    private void Update()
    {
        EntityContainer.Tick();
    }
}


