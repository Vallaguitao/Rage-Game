using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [Header("Visuals")]
    [TextArea(10, 10)]
    public string[] dialogueStorage;
    public TextMeshProUGUI dialogueText;

    [Header("Dialogue Details")]
    [SerializeField] private int dialogueIndex;
    [SerializeField] private float textSpeed;

    public CutsceneControl cutsceneControl;

    // Start is called before the first frame update
    void Start()
    {
        dialogueIndex = 0;
        dialogueText.text = string.Empty;
        StartCoroutine("TypeDialogue");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogueText.text.Equals(dialogueStorage[dialogueIndex]))
            {
                if (gameObject.activeSelf) //testing
                {
                    NextDialogue();
                }
                
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueStorage[dialogueIndex];
            }
        }
    }

    private void NextDialogue()
    {
        if(dialogueIndex < dialogueStorage.Length - 1)
        {
            dialogueIndex++;
            dialogueText.text = string.Empty;
            StartCoroutine("TypeDialogue");
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator TypeDialogue()
    {
        foreach(char letter in dialogueStorage[dialogueIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
