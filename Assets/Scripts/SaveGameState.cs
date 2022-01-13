using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows;
using Sirenix.Serialization;
using System.Collections.Generic;

public class SaveGameState : MonoBehaviour, ISerializationCallbackReceiver, ISupportsPrefabSerialization
{
    public int noOfTaps = default;
    public List<string> listOfNames;
    public Dictionary<int, string> itemDictionary;
    public GenericDataSO dataScriptable;

    public UnityAction eventDataLoaded;

    private string filePath = default;

    [SerializeField, HideInInspector]
    private SerializationData serializationData;
    public SerializationData SerializationData { get => this.serializationData; set => this.serializationData = value; }
    
    private void Awake()
    {
        filePath = Application.persistentDataPath + "/DataFile.bin";
    }

    private void Start()
    {
        listOfNames = new List<string>();
        //itemDictionary = new Dictionary<int, string>();
        LoadState();
    }

    public void OnBeforeSerialize()
    {
        try {
            UnitySerializationUtility.SerializeUnityObject(this, ref this.serializationData);
        } catch (System.Exception exp) { }
    }

    public void OnAfterDeserialize()
    {
        try {
            UnitySerializationUtility.DeserializeUnityObject(this, ref this.serializationData);
        } catch (System.Exception exp) { }
    }


    public void LoadState()
    {
        if (!File.Exists(filePath)) SaveData(); // No state to load

        // Unity should be allowed to handle serialization and deserialization of its own weird objects.
        // So if your data-graph contains UnityEngine.Object types, you will need to provide Odin with
        // a list of UnityEngine.Object which it will then use as an external reference resolver.
        List<Object> unityObjectReferences = new List<Object>();

        //DataFormat dataFormat = DataFormat.Binary;
        //DataFormat dataFormat = DataFormat.JSON;
        //DataFormat dataFormat = DataFormat.Nodes;


        var bytes = File.ReadAllBytes(filePath);

        // If you have a string to deserialize, get the bytes using UTF8 encoding
        // var bytes = System.Text.Encoding.UTF8.GetBytes(jsonString);

        var data = SerializationUtility.DeserializeValue<SaveGameState>(bytes, DataFormat.Binary, unityObjectReferences);
        
        noOfTaps = data.noOfTaps;
        listOfNames = data.listOfNames;
        itemDictionary = data.itemDictionary;
        dataScriptable.prefabName = data.dataScriptable.prefabName;
        dataScriptable.numberOfPrefabsToCreate = data.dataScriptable.numberOfPrefabsToCreate;
        dataScriptable.spawnPoints = data.dataScriptable.spawnPoints;

        Debug.Log("Prefab Name = " + data.dataScriptable.prefabName);
        Debug.Log("SpawnPoint = " + data.dataScriptable.numberOfPrefabsToCreate);

        Debug.Log("Data Loaded..");
        eventDataLoaded.Invoke();
    }

    public void SaveData()
    {
        byte[] bytes = SerializationUtility.SerializeValue(this, DataFormat.Binary);
        File.WriteAllBytes(filePath, bytes);
    }
}
