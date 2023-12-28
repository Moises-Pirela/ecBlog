using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.EventSystems.EventTrigger;

public class EntityContainer
{
    public Entity[] Entities;
    public ComponentArray[] Components;

    private int AvailableEntityId;
    private int RecycledEntityId = -1;

    public EntityContainer()
    {
        Entities = new Entity[World.MAX_ENTITIES];
        Components = new ComponentArray[(int)ComponentType.Max];

        for (int i = 0; i < Components.Length; i++)
        {
            Components[i] = GetComponentArray((ComponentType)i);
        }
    }

    public void CreateEntity(GameObject entityPrefab)
    {
        Entity entity = entityPrefab.AddComponent<Entity>();

        if (RecycledEntityId != -1)
        {
            entity.Id = RecycledEntityId;
            RecycledEntityId = -1;
        }
        else
        {
            entity.Id = AvailableEntityId;
            AvailableEntityId++;
        }

        if (entity.Id > World.MAX_ENTITIES)
        {
            GameObject.Destroy(entityPrefab); //This could be a poolable object

            return;
        }

        IComponent[] components = entityPrefab.GetComponents<IComponent>();

        for (int i = 0;i < components.Length;i++) 
        {
            ComponentType componentType = components[i].GetComponentType();

            Components[(int)componentType].Add(entity.Id, components[i]);
        }
    }

    public void RemoveEntity(int entityID)
    {
        Entity entity = Entities[entityID];
        IComponent[] components = entity.GetComponents<IComponent>();

        for (int i = 0; i< components.Length;i++) 
        {
            Components[(int)components[i].GetComponentType()].Remove(entityID);
        }

        Entities[entityID].Id = -1;

        RecycledEntityId = entityID;

        GameObject.Destroy(entity.gameObject); //NOTE: We can pool these entities to make sure we're being as efficient as possible with entity creation and removal

    }

    public void Tick()
    {
        for (int i = 0; i < Components.Length; i++) 
        {
            Components[i].UpdateComponents();
        }
    }

    public static ComponentArray GetComponentArray(ComponentType componentType)
    {
        switch (componentType)
        {
            case ComponentType.Movement:
                return new ComponentArray<MovementComponent>();
            case ComponentType.Health:
                return new ComponentArray<HealthComponent>();
        }

        return null;
    }
}


