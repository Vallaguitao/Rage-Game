using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreenNextStage : MonoBehaviour
{
    public void BlackScreenLoadNextStage(int stageIndex)
    {
        GameManager.gameManagerScript.LoadStageSelect(stageIndex);
    }
}
