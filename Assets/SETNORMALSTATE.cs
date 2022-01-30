using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SETNORMALSTATE : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GlobalStateManager._Instance.UpdateGameState(GlobalStateManager.GameState.PLAYER_NORMAL);   
    }
}
