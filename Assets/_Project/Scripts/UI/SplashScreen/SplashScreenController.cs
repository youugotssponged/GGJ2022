using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class SplashScreenController : MonoBehaviour
{
    public Image SplashScreenVideoCover;
    private void Start()
    {
        if(SplashScreenVideoCover == null)
            SplashScreenVideoCover = GetComponent<Image>();

         StartCoroutine(FadeInSplashScreen());
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
            SkipScene();
    }

    private IEnumerator FadeInSplashScreen()
    {
        for (float i = 3; i >= 0; i -= Time.deltaTime) 
        {
            // set color with i as alpha
            var c = new Color(0, 0, 0, i);
            SplashScreenVideoCover.color = c;
            yield return null;
        }
    }

    private void SkipScene() => 
        GlobalSceneManager._Instance.UpdateSceneManagerState(GlobalSceneManager.SceneManagerState.MAINMENU);
}
