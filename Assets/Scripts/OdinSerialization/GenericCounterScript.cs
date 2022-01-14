using UnityEngine;
using UnityEngine.UI;

public class GenericCounterScript : MonoBehaviour
{
    [SerializeField] private SaveGameState saveGameObj;
    [SerializeField] private Text incCounterTxt;
    int counter = 0;

    private void OnEnable()
    {
        saveGameObj.eventDataLoaded += RefreashLoadedData;
    }

    private void RefreashLoadedData()
    {
        counter = saveGameObj.noOfTaps;
        incCounterTxt.text = counter.ToString("00");
    }

    public void IncremnetCounter(int counterInc) {
        counter += counterInc;
        saveGameObj.noOfTaps = counter;
        incCounterTxt.text = counter.ToString("00");
    }

    private void OnDisable()
    {
        saveGameObj.eventDataLoaded -= RefreashLoadedData;
    }
}
