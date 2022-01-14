using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

[ShowOdinSerializedPropertiesInInspector] [CreateAssetMenu(fileName = "GenericData", menuName = "ScriptableObjects/Data Scriptable Object", order = 1)]
public class GenericDataSO : ScriptableObject, ISerializationCallbackReceiver
{
    public string prefabName;
    public int numberOfPrefabsToCreate;
    public Vector3[] spawnPoints;

    [SerializeField, HideInInspector]
    private SerializationData serializationData;

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        try {
            UnitySerializationUtility.DeserializeUnityObject(this, ref this.serializationData);
        } catch(System.Exception exp) { }
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        try {
            UnitySerializationUtility.SerializeUnityObject(this, ref this.serializationData);
        } catch (System.Exception exp) { }
    }
}
