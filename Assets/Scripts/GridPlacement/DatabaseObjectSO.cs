using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ObjectDatabaseSO", menuName = "Scriptable Objects/ObjectDatabaseSO")]
public class ObjectDatabaseSO : ScriptableObject
{
    public List<ObjectData> objectsData;
}
[System.Serializable]
public class ObjectData
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public Vector2Int Size { get; private set; }
}
