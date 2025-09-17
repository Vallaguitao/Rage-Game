using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

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
    public UnityEvent OnDialogueFinish;

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
        if(GameManager.gameManagerScript.playerControllerScript.InteractActionController.WasPressedThisFrame())
        {
            if (dialogueText.text.Equals(dialogueStorage[dialogueIndex]))
            {

                NextDialogue();
                
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
            OnDialogueFinish?.Invoke();
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
