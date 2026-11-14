using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnOptions
{
    public Vector3 position;
}

public abstract class BaseEntityLoader<TEntity, TOptions> : MonoBehaviour
    where TEntity : MonoBehaviour
    where TOptions : SpawnOptions
{
    [SerializeField]
    protected TEntity prefab;

    protected abstract IEnumerable<TOptions> GetSpawnOptions();

    protected virtual void Setup(TEntity entity, TOptions options) { }

    public void Init()
    {
        foreach (TOptions options in GetSpawnOptions())
        {
            SpawnEntity(options);
        }
    }

    protected TEntity SpawnEntity(TOptions options)
    {
        TEntity entity = Instantiate(prefab, options.position, Quaternion.identity);
        Setup(entity, options);
        return entity;
    }
}
