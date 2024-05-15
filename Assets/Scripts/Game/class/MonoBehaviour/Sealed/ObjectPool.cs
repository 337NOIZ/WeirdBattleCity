
using System;

using System.Collections.Generic;

using UnityEngine;

public sealed class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance { get; private set; }

    private Dictionary<CharacterCode, Character> _characterPrefabDictionary = new Dictionary<CharacterCode, Character>();

    private Dictionary<ItemCode, DroppedItem> _droppedItemPrefabDictionary = new Dictionary<ItemCode, DroppedItem>();

    private Dictionary<ProjectileCode, Projectile> _projectilePrefabDictionary = new Dictionary<ProjectileCode, Projectile>();

    private Dictionary<ParticleEffectCode, ParticleEffect> _particleEffectPrefabDictionary = new Dictionary<ParticleEffectCode, ParticleEffect>();

    private Dictionary<CharacterCode, Stack<Character>> _characterPool = new Dictionary<CharacterCode, Stack<Character>>();

    private Dictionary<ItemCode, Stack<DroppedItem>> _droppedItemPool = new Dictionary<ItemCode, Stack<DroppedItem>>();

    private Dictionary<ProjectileCode, Stack<Projectile>> _projectilePool = new Dictionary<ProjectileCode, Stack<Projectile>>();

    private Dictionary<ParticleEffectCode, Stack<ParticleEffect>> _particleEffectPool = new Dictionary<ParticleEffectCode, Stack<ParticleEffect>>();

    private void Awake()
    {
        instance = this;

        var characterPrefabArray = Resources.LoadAll<Character>("Prefabs/Characters");

        var index_Max = characterPrefabArray.Length;

        for (int index = 0; index < index_Max; ++index)
        {
            _characterPrefabDictionary.Add(characterPrefabArray[index].characterCode, characterPrefabArray[index]);
        }

        var droppedItemPrefabArray = Resources.LoadAll<DroppedItem>("Prefabs/Item/DroppedItems");

        index_Max = droppedItemPrefabArray.Length;

        for (int index = 0; index < index_Max; ++index)
        {
            _droppedItemPrefabDictionary.Add(droppedItemPrefabArray[index].itemCode, droppedItemPrefabArray[index]);
        }

        var projectilePrefabArray = Resources.LoadAll<Projectile>("Prefabs/Projectiles");

        index_Max = projectilePrefabArray.Length;

        for (int index = 0; index < index_Max; ++index)
        {
            _projectilePrefabDictionary.Add(projectilePrefabArray[index].projectileCode, projectilePrefabArray[index]);
        }

        var particleEffectArray = Resources.LoadAll<ParticleEffect>("Prefabs/ParticleEffects");

        index_Max = particleEffectArray.Length;

        for (int index = 0; index < index_Max; ++index)
        {
            _particleEffectPrefabDictionary.Add(particleEffectArray[index].particleEffectCode, particleEffectArray[index]);
        }

        foreach (CharacterCode characterCode in Enum.GetValues(typeof(CharacterCode)))
        {
            _characterPool.Add(characterCode, new Stack<Character>());
        }

        foreach (ItemCode itemCode in Enum.GetValues(typeof(ItemCode)))
        {
            _droppedItemPool.Add(itemCode, new Stack<DroppedItem>());
        }

        foreach (ProjectileCode projectileCode in Enum.GetValues(typeof(ProjectileCode)))
        {
            _projectilePool.Add(projectileCode, new Stack<Projectile>());
        }

        foreach (ParticleEffectCode particleEffectCode in Enum.GetValues(typeof(ParticleEffectCode)))
        {
            _particleEffectPool.Add(particleEffectCode, new Stack<ParticleEffect>());
        }
    }

    public Character Pop(CharacterCode characterCode)
    {
        Character character;

        if (_characterPool[characterCode].Count > 0)
        {
            character = _characterPool[characterCode].Pop();
        }

        else
        {
            character = Instantiate(_characterPrefabDictionary[characterCode]);

            character.Awaken();
        }

        return character;
    }

    public DroppedItem Pop(ItemCode itemCode)
    {
        DroppedItem droppedItem;

        if (_droppedItemPool[itemCode].Count > 0)
        {
            droppedItem = _droppedItemPool[itemCode].Pop();
        }

        else
        {
            droppedItem = Instantiate(_droppedItemPrefabDictionary[itemCode]);

            droppedItem.Awaken();
        }

        return droppedItem;
    }

    public Projectile Pop(ProjectileCode projectileCode)
    {
        Projectile projectile;

        if (_projectilePool[projectileCode].Count > 0)
        {
            projectile = _projectilePool[projectileCode].Pop();
        }

        else
        {
            projectile = Instantiate(_projectilePrefabDictionary[projectileCode]);

            projectile.Awaken();
        }

        return projectile;
    }

    public ParticleEffect Pop(ParticleEffectCode particleEffectCode)
    {
        ParticleEffect particleEffect;

        if (_particleEffectPool[particleEffectCode].Count > 0)
        {
            particleEffect = _particleEffectPool[particleEffectCode].Pop();
        }

        else
        {
            particleEffect = Instantiate(_particleEffectPrefabDictionary[particleEffectCode]);
        }

        return particleEffect;
    }

    public void Push(Character character)
    {
        _characterPool[character.characterCode].Push(character);
    }

    public void Push(DroppedItem droppedItem)
    {
        _droppedItemPool[droppedItem.itemCode].Push(droppedItem);
    }

    public void Push(Projectile projectile)
    {
        _projectilePool[projectile.projectileCode].Push(projectile);
    }

    public void Push(ParticleEffect particleEffect)
    {
        _particleEffectPool[particleEffect.particleEffectCode].Push(particleEffect);
    }
}