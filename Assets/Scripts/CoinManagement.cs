using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManagement : MonoBehaviour
{
    public int InitialCounter;
    public int CurrentCounter;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in gameObject.transform)
        {
            InitialCounter++;
        }
    }
    // Update is called once per frame
    void Update()
    {
        CountCoins();
    }

    void CountCoins()
    {
        CurrentCounter = gameObject.transform.childCount;
    }
}
