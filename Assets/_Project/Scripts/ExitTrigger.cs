using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("PLAYER HAS ENTERED");
            GlobalStateManager._Instance.UpdateGameState(GlobalStateManager.GameState.LOADING);
        }
    }
}
