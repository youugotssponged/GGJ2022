using System;
using UnityEngine;

public class GlobalStateManager : MonoBehaviour
{
    public enum GameState : int
    {
        START_GAME = 0,
        LOADING,
        END_GAME_GOOD,
        END_GAME_BAD,
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

            case GameState.LOADING:
                HandleLoadingState();
                break;

            case GameState.END_GAME_GOOD:
                HandleEndGameGoodState();
                break;

            case GameState.END_GAME_BAD:
                HandleEndGameBadState();
                break;

            default: 
                HandleLimbo();
                break;
        };

        // Invoke to whoever may be listening to any Game State Changes
        OnGameStateChanged?.Invoke(gameState);
    }


    private void HandleStartGameState() =>
        GlobalSceneManager._Instance.UpdateSceneManagerState(GlobalSceneManager.SceneManagerState.LEVEL_DENIAL);


    private void HandleEndGameGoodState() =>
        GlobalSceneManager._Instance.UpdateSceneManagerState(GlobalSceneManager.SceneManagerState.ENDSCREEN_GOOD);


    private void HandleEndGameBadState() =>
        GlobalSceneManager._Instance.UpdateSceneManagerState(GlobalSceneManager.SceneManagerState.ENDSCREEN_BAD);
   
    private void HandleLoadingState() =>
        GlobalSceneManager._Instance.UpdateSceneManagerState(GlobalSceneManager.SceneManagerState.LOADINGSCREEN);

    private void HandleLimbo() =>
        throw new ArgumentException("Game State: LIMBO, Game State Either Doesn't exist or something went wrong... Please check the Global State Manager");

}