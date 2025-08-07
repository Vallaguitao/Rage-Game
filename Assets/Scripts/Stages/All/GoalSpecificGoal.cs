using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalSpecificGoal : MonoBehaviour
{
    //theres a better way to do this but i'm lazy
    [SerializeField] private TextMeshProUGUI availableCoinsText;
    [SerializeField] private int overallCoinsCountPermanent;
    [SerializeField] private int overallCoinsCount;
    [SerializeField] private int collectedCoins;
    private int testing;

    [SerializeField] GameObject finishline;

    // Start is called before the first frame update
    void Start()
    {
        availableCoinsText = GetComponent<TextMeshProUGUI>();
        var coinStorage = Object.FindObjectsOfType<Coins>();

        overallCoinsCount = coinStorage.Length;
        overallCoinsCountPermanent = overallCoinsCount;

        testing = overallCoinsCount;
        availableCoinsText.SetText($"{collectedCoins} / {overallCoinsCount}");

        finishline = GameObject.Find("Finish Line");

        finishline.SetActive( false );
    }

    // Update is called once per frame
    void Update()
    {
        var coinStorage = Object.FindObjectsOfType<Coins>();

        overallCoinsCount = coinStorage.Length;

        if (testing > overallCoinsCount)
        {
            testing--;
            UpdateCollectedCoins();
        }

        if(collectedCoins == overallCoinsCountPermanent)
        {
            finishline.SetActive( true );
        }

    }

    public void UpdateCollectedCoins()
    {
        collectedCoins++;

        availableCoinsText.SetText($"{collectedCoins} / {overallCoinsCountPermanent}");
    }
}
