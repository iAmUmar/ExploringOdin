using UnityEngine;
using UnityEngine.UI;

public class AddNamesScript : MonoBehaviour
{
    [SerializeField] private SaveGameState saveGameObj;
    [SerializeField] private InputField namesField, itemField;
    [SerializeField] private Text namesTxt, itemTxt;

    private string allNames = default, allItems = default;

    private void OnEnable()
    {
        saveGameObj.eventDataLoaded += RefreashLoadedData;
    }

    private void RefreashLoadedData()
    {
        if (saveGameObj.listOfNames.Count > 0) {
            foreach (string objStr in saveGameObj.listOfNames)
                allNames += objStr + " || ";
            namesTxt.text = allNames;
        }

        if (saveGameObj.itemDictionary == null) {
            saveGameObj.itemDictionary = new System.Collections.Generic.Dictionary<int, string>();
        } else if (saveGameObj.itemDictionary.Count > 0) {
            foreach (string objStr in saveGameObj.itemDictionary.Values)
                allItems += objStr + " || ";
            itemTxt.text = allItems;
        }
    }

    public void AddNameToList() {
        if (namesField.text.Trim().Equals(""))
            return ;

        saveGameObj.listOfNames.Add(namesField.text);
        namesField.placeholder.gameObject.GetComponent<Text>().text = "Press Enter after adding Name";

        allNames += namesField.text + " || ";
        namesTxt.text = allNames;

        Debug.Log("Name Added");

        namesField.text = "";
    }

    public void AddItemToDictionary() {
        if (itemField.text.Trim().Equals(""))
            return;

        saveGameObj.itemDictionary.Add(saveGameObj.itemDictionary.Count, itemField.text);

        allItems += itemField.text + " || ";
        itemTxt.text = allItems;

        Debug.Log("Item Added");

        itemField.text = "";
    }

    private void OnDisable()
    {
        saveGameObj.eventDataLoaded -= RefreashLoadedData;
    }
}
