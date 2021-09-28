using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public ObjectPool(GameObject _parent, GameObject _prefab, int _limit, int _overflowPoolGrowthAmount)
    {
        parent = _parent;
        prefab = _prefab;
        limit = _limit;
        overflowAmount = _overflowPoolGrowthAmount;
        Initialize();
    }

    private List<GameObject> objPool; //Saves the created objects.

    private GameObject parent; //The parent where the object (clones) are spawned.
    private GameObject prefab; //The object that gets created.
    private int limit; //The max number of created objects.
    private int overflowAmount; //The number of objects added to the pool if the pool is too small.

    private void Initialize()
    {
        objPool = new List<GameObject>();
        AddObjectsToPool(limit);
    }

    /// <summary>
    /// Get the object from the list
    /// </summary>
    public GameObject GetObjectFromPool()
    {
        //Go's through all the objects in the list.
        for (int i = 0; i < objPool.Count; i++)
        {
            //Selects an non active object.
            if (!objPool[i].activeSelf)
            {
                //Actives the object
                objPool[i].SetActive(true);

                //Returns the object.
                return objPool[i];
            }
        }

        //Adds an object to the list, if there are none.
        AddObjectsToPool(overflowAmount);

        return GetObjectFromPool();
    }

    /// <summary>
    /// Adds a certain amount of objects to the list.
    /// </summary>
    private void AddObjectsToPool(int _growthSize)
    {
        //Makes the object x amount of times.
        for (int i = 0; i < _growthSize; i++)
        {
            //Creates the object.
            GameObject obj = GameObject.Instantiate(prefab, parent.transform);
            obj.SetActive(false);
            objPool.Add(obj);
        }
    }

    /// <summary>
    /// Deactivates the given object.
    /// </summary>
    public void DeactivateObject(GameObject obj)
    {
        //Checks to see if this is a object
        if (objPool.Contains(obj))
        {
            obj.SetActive(false);
        }
    }
}
