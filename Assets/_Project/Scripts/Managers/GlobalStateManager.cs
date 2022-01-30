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

            // loading is now an informational state
             case GameState.LOADING:
                 break;
            case GameState.ENDGAME_GOOD:
            case GameState.ENDGAME_BAD:
            case GameState.PLAYER_DEAD:
            case GameState.PLAYER_NORMAL:
            case GameState.PLAYER_SHADOW_REALM:
                break;

            default: 
                throw new ArgumentException(
                    @"Game State: [LIMBO], Game State Either Doesn't exist or 
                        You forgot to add a case for your newly added State... 
                        Please check the Global State Manager");
        };

        // Invoke to whoever may be listening to any Game State Changes
        OnGameStateChanged?.Invoke(gameState);
    }

    private void HandleStartGameState() =>
        GlobalSceneManager._Instance.UpdateSceneManagerState(GlobalSceneManager.SceneManagerState.LEVEL_DENIAL);
}