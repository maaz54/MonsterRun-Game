using ObjectPool.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public class ObjectPooler : MonoBehaviour, IObjectPooler
    {
        // List of objects that can be pooled
        private List<IPoolableObject> poolableObjects = new();

        // Pool an object and return it
        public T Pool<T>(IPoolableObject _object, Transform parent = null) where T : MonoBehaviour
        {
            T obj = null;
            IPoolableObject poolableObject = poolableObjects.Find(i => i.ObjectID == _object.ObjectID);
            if (poolableObject == null)
            {
                obj = Instantiate(_object.Transform.gameObject).GetComponent<T>();
            }
            else
            {
                obj = poolableObject.Transform.gameObject.GetComponent<T>();
                poolableObjects.Remove(poolableObject);
            }

            if (obj != null)
            {
                obj.gameObject.SetActive(true);
                obj.transform.SetParent(parent);
            }

            return obj;
        }

        // Remove an object from the pool
        public void Remove<T>(T _object) where T : IPoolableObject
        {
            _object.Transform.gameObject.SetActive(false);
            _object.Transform.gameObject.transform.SetParent(transform);
            poolableObjects.Add(_object);
        }
    }
}
