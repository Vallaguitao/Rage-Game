using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintText : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI hintTexts;

    [Header("Hints")]
    [TextArea(10, 10)]
    public string[] hintStorage;

    // Start is called before the first frame update
    void Start()
    {
        hintTexts = GetComponent<TextMeshProUGUI>();
        int randomIndex = Random.Range(0, hintStorage.Length);

        hintTexts.SetText(hintStorage[randomIndex]);
    }

}
