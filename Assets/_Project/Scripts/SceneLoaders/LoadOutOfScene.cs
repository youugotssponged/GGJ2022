using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadOutOfScene : MonoBehaviour
{
    public GlobalSceneManager.SceneManagerState NextLevelToLoadInto;
    public Image OverlayFade;

    private void Awake()
    {
        if(OverlayFade == null)
            OverlayFade = GetComponent<Image>();

        GlobalStateManager.OnGameStateChanged += HandleGameStateChange;
    }
    private void OnDestroy() =>
        GlobalStateManager.OnGameStateChanged -= HandleGameStateChange;

    public void HandleGameStateChange(GlobalStateManager.GameState gameState)
    {
        if(gameState == GlobalStateManager.GameState.LOADING)
        {
            StartCoroutine(FadeToBlack());
        }
    }

    private IEnumerator FadeToBlack() 
    {
        for (float i = 0; i <= 4; i += Time.deltaTime) 
        {
            // set color with i as alpha  
            OverlayFade.color = new Color(0, 0, 0, i);
            yield return null;
        }
        GlobalSceneManager._Instance.UpdateSceneManagerState(NextLevelToLoadInto);
    }
}
