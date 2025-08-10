using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{

    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Slider loadingSlider;

    private void Start()
    {
        eventSystem = GameObject.FindObjectOfType<EventSystem>();
    }

    #region Main Menu Buttons Control

    public void TextColorNormal(TextMeshProUGUI textInvolved)
    {
        textInvolved.color = new Color(0.705527f, 0.8792453f, 0.1841438f, 1);
    }

    public void TextColorBlack(TextMeshProUGUI textInvolved)
    {
        textInvolved.color = Color.black;
    }

    public void ChangeSelectedObject(GameObject buttonToBeSelected)
    {
        eventSystem.SetSelectedGameObject(buttonToBeSelected);
    }

    public void RemoveSelectedObject()
    {
        eventSystem.SetSelectedGameObject(null);
    }


    public void LoadLevel(string sceneNameToLoad)
    {
        StartCoroutine(LoadLevelAsynch(sceneNameToLoad));
    }

    IEnumerator LoadLevelAsynch(string levelToLoad)
    {

        if (loadingSlider != null)
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

            while (!loadOperation.isDone)
            {
                float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
                loadingSlider.value = progressValue;
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            print("No Slider");
        }

    }


    public void ExitGame()
    {
        #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
        #else
                        Application.Quit(); // 
        #endif
    }

    #endregion
}
