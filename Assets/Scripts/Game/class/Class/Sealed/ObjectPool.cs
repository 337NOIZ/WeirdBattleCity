
using System;

using System.Collections.Generic;

using UnityEngine;

public sealed class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance { get; private set; }

    private Dictionary<CharacterCode, Stack<Character>> characterPool = new Dictionary<CharacterCode, Stack<Character>>();

    private Dictionary<ItemCode, Stack<DroppedItem>> droppedItemPool = new Dictionary<ItemCode, Stack<DroppedItem>>();

    private Dictionary<ProjectileCode, Stack<Projectile>> projectilePool = new Dictionary<ProjectileCode, Stack<Projectile>>();

    private Dictionary<ParticleEffectCode, Stack<ParticleEffect>> particleEffectPool = new Dictionary<ParticleEffectCode, Stack<ParticleEffect>>();

    private Dictionary<CharacterCode, Character> characterPrefabs;

    private Dictionary<ItemCode, DroppedItem> droppedItemPrefabs;

    private Dictionary<ProjectileCode, Projectile> projectilePrefabs;

    private Dictionary<ParticleEffectCode, ParticleEffect> particleEffectPrefabs;

    private void Awake()
    {
        instance = this;

        foreach (CharacterCode characterCode in Enum.GetValues(typeof(CharacterCode)))
        {
            characterPool.Add(characterCode, new Stack<Character>());
        }

        foreach (ItemCode itemCode in Enum.GetValues(typeof(ItemCode)))
        {
            droppedItemPool.Add(itemCode, new Stack<DroppedItem>());
        }

        foreach (ProjectileCode projectileCode in Enum.GetValues(typeof(ProjectileCode)))
        {
            projectilePool.Add(projectileCode, new Stack<Projectile>());
        }

        foreach (ParticleEffectCode particleEffectCode in Enum.GetValues(typeof(ParticleEffectCode)))
        {
            particleEffectPool.Add(particleEffectCode, new Stack<ParticleEffect>());
        }
    }

    private void Start()
    {
        characterPrefabs = GameMaster.instance.characterPrefabs;

        droppedItemPrefabs = GameMaster.instance.droppedItemPrefabs;

        projectilePrefabs = GameMaster.instance.projectilePrefabs;

        particleEffectPrefabs = GameMaster.instance.particleEffectPrefabs;
    }

    public Character Pop(CharacterCode characterCode)
    {
        Character character;

        if (characterPool[characterCode].Count > 0)
        {
            character = characterPool[characterCode].Pop();

            character.gameObject.SetActive(true);
        }

        else
        {
            character = Instantiate(characterPrefabs[characterCode], transform);

            character.Initialize();
        }

        return character;
    }

    public DroppedItem Pop(ItemCode itemCode)
    {
        DroppedItem droppedItem;

        if (droppedItemPool[itemCode].Count > 0)
        {
            droppedItem = droppedItemPool[itemCode].Pop();

            droppedItem.gameObject.SetActive(true);
        }

        else
        {
            droppedItem = Instantiate(droppedItemPrefabs[itemCode], transform);
        }

        return droppedItem;
    }

    public Projectile Pop(ProjectileCode projectileCode)
    {
        Projectile projectile;

        if (projectilePool[projectileCode].Count > 0)
        {
            projectile = projectilePool[projectileCode].Pop();

            projectile.gameObject.SetActive(true);
        }

        else
        {
            projectile = Instantiate(projectilePrefabs[projectileCode], transform);
        }

        return projectile;
    }

    public ParticleEffect Pop(ParticleEffectCode particleEffectCode)
    {
        ParticleEffect particleEffect;

        if (particleEffectPool[particleEffectCode].Count > 0)
        {
            particleEffect = particleEffectPool[particleEffectCode].Pop();

            particleEffect.gameObject.SetActive(true);
        }

        else
        {
            particleEffect = Instantiate(particleEffectPrefabs[particleEffectCode], transform);

            particleEffect.Initialize();
        }

        return particleEffect;
    }

    public void Push(Character character)
    {
        characterPool[character.characterCode].Push(character);
    }

    public void Push(DroppedItem droppedItem)
    {
        droppedItemPool[droppedItem.itemCode].Push(droppedItem);
    }

    public void Push(Projectile projectile)
    {
        projectilePool[projectile.projectileCode].Push(projectile);
    }

    public void Push(ParticleEffect particleEffect)
    {
        particleEffectPool[particleEffect.particleEffectCode].Push(particleEffect);
    }
}