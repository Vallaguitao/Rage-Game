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

    [SerializeField] private CanvasGroup optionGroup;

    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle vSynchToggle;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
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

    //--------------------Options-------------------------

    public void OptionButton()
    {
        optionGroup.alpha = 1;
    }

    public void FullScreen()
    {
        if(fullscreenToggle.isOn)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
        
    }

    public void VSynch()
    {
        if(vSynchToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }

    public void ScreenResolution()
    {

        int width = 0;
        int height = 0;

        switch(resolutionDropdown.value)
        {
            case 0:
                width = 3840;
                height = 2160;
                break;
            case 1:
                width = 2560;
                height = 1440;
                break;
            case 2:
                width = 1920;
                height = 1080;
                break;
            case 3:
                width = 1366;
                height = 768;
                break;
            case 4:
                width = 1280;
                height = 720;
                break;
            default:
                break;
        }

        Screen.SetResolution(width, height , fullscreenToggle.isOn);
    }

    //--------------------Load Level-------------------------
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
