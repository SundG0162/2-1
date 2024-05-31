using ObjectPooling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    private Dictionary<PoolingType, Pool<PoolableMono>> _pools = new Dictionary<PoolingType, Pool<PoolableMono>>();

    public PoolingTableSO poolTable;

    private void Awake()
    {
        foreach (PoolingItemSO item in poolTable.datas)
        {
            CreatePool(item);
        }
    }

    private void CreatePool(PoolingItemSO item)
    {
        Pool<PoolableMono> pool = new Pool<PoolableMono>(item.prefab, transform, item.prefab.type, item.poolCount);

        _pools.Add(item.prefab.type, pool);
    }

    public PoolableMono Pop(PoolingType type)
    {
        if (!_pools.ContainsKey(type))
        {
            Debug.LogError($"Prefab doesn't exist on pool : {type.ToString()}");
            return null;
        } 

        PoolableMono item = _pools[type].Pop();
        item.ResetItem();
        return item;
    }

    public void Push(PoolableMono item, bool resetParent = false)
    {
        if (resetParent)
            item.transform.parent = transform;
        _pools[item.type].Push(item);
    }
}