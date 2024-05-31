using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class Pool<T> where T : PoolableMono
    {
        private Stack<T> _pool = new Stack<T>();
        private T _prefab;
        private Transform _parent;

        private PoolingType _poolingType;

        public Pool(T prefab, Transform parent, PoolingType type, int count)
        {
            _prefab = prefab;
            _parent = parent;
            _poolingType = type;

            for(int i = 0; i < count; i++) 
            {
                T obj = GameObject.Instantiate(prefab, parent);
                obj.type = _poolingType;
                obj.gameObject.name = _poolingType.ToString();
                obj.gameObject.SetActive(false);
                _pool.Push(obj);
            }
        }

        public T Pop()
        {
            T obj = null;
            if(_pool.Count <= 0)
            {
                obj = GameObject.Instantiate(_prefab, _parent);
                obj.type = _poolingType;
                obj.gameObject.name = _poolingType.ToString();
                obj.gameObject.SetActive(false);
                _pool.Push(obj);
            }
            else
            {
                obj = _pool.Pop();
                obj.gameObject.SetActive(true);
            }
            return obj;
        }

        public void Push(T obj)
        {
            obj.gameObject.SetActive(false);
            _pool.Push(obj);
        }
    }
}