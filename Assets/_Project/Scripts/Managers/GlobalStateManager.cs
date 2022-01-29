using System;
using UnityEngine;

public class GlobalStateManager : MonoBehaviour
{
    public enum GameState : int
    {
        START_GAME = 0,
        LOADING,
        ENDGAME_GOOD,
        ENDGAME_BAD,
        PLAYER_DEAD,
        PLAYER_NORMAL,
        PLAYER_SHADOW_REALM,
    }

    public GameState CurrentGameState;

    public static GlobalStateManager _Instance;
    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        _Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void UpdateGameState(GameState gameState)
    {
        CurrentGameState = gameState;

        switch(gameState)
        {
            case GameState.START_GAME:
                HandleStartGameState();
                break;

            // No longer neeeded needed, loading is now an informational state
             case GameState.LOADING:
                 break;

            // TODO: Handle states e.g what does the game manager have to do other than set curr state
            case GameState.ENDGAME_GOOD:
                break;
            case GameState.ENDGAME_BAD:
                break;
            case GameState.PLAYER_DEAD:
                break;
            case GameState.PLAYER_NORMAL:
                break;
            case GameState.PLAYER_SHADOW_REALM:
                break;

            default: 
                throw new ArgumentException(
                    @"Game State: LIMBO, Game State Either Doesn't exist or 
                        something went wrong... 
                        Please check the Global State Manager");
        };

        // Invoke to whoever may be listening to any Game State Changes
        OnGameStateChanged?.Invoke(gameState);
    }


    private void HandleStartGameState() =>
        GlobalSceneManager._Instance.UpdateSceneManagerState(GlobalSceneManager.SceneManagerState.LEVEL_DENIAL);
   
    // private void HandleLoadingState() =>
    //     GlobalSceneManager._Instance.UpdateSceneManagerState(GlobalSceneManager.SceneManagerState.LOADINGSCREEN);

}