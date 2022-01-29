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
         StartCoroutine(WaitForVideoToEnd());
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
            SkipScene();
    }

    private IEnumerator FadeInSplashScreen()
    {
        for (float i = 2; i >= 0; i -= Time.deltaTime) 
        {
            // set color with i as alpha
            var c = new Color(0, 0, 0, i);
            SplashScreenVideoCover.color = c;
            yield return null;
        }
    }

    private IEnumerator FadeVideoOut(){
        for (float i = 0; i <= 1; i += Time.deltaTime) 
        {
            // set color with i as alpha
            var c = new Color(0, 0, 0, i);
            SplashScreenVideoCover.color = c;
            yield return null;
        }
        GlobalSceneManager._Instance.UpdateSceneManagerState(GlobalSceneManager.SceneManagerState.MAINMENU);
    }


    private IEnumerator WaitForVideoToEnd(){
        yield return new WaitForSeconds(13);
        StartCoroutine(FadeVideoOut());
    }

    private void SkipScene() => 
        GlobalSceneManager._Instance.UpdateSceneManagerState(GlobalSceneManager.SceneManagerState.MAINMENU);
}
