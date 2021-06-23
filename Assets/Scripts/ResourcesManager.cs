using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    private Dictionary<PrefabEnum, GameObject> _prefabDictionary = new Dictionary<PrefabEnum, GameObject>();
    private const string prefabsPrefix = "Prefabs/";


    private void Awake()
    {
        foreach (PrefabEnum prefabType in (PrefabEnum[])Enum.GetValues(typeof(PrefabEnum)))
        {
            AddPrefab(prefabType);
        }
    }
    private void AddPrefab(PrefabEnum prefabType)
    {
        _prefabDictionary.Add(prefabType, Resources.Load<GameObject>(prefabsPrefix + prefabType.ToString()));
    }

    public GameObject GetPrefab(PrefabEnum objectName)
    {
        return _prefabDictionary[objectName];
    }
}
