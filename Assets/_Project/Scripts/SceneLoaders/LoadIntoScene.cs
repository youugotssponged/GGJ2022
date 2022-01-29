using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class LoadIntoScene : MonoBehaviour
{
    public Image OverlayToFade;
    public Text TextTitle;
    public Text TextDisc;

    private Color TEXTRGB;

    public void Start()
    {
        StartCoroutine(FadeToBlack());
        TEXTRGB = TextTitle.color;
    }

    private IEnumerator FadeToBlack() 
    {
        for (float i = 4; i >= 0; i -= Time.deltaTime) 
        {
            // set color with i as alpha  
            OverlayToFade.color = new Color(0, 0, 0, i);
            yield return null;
        }
        StartCoroutine(FadeTextIn());
        // Fade in TEXT
    }

    private IEnumerator FadeTextIn()
    {
        for (float i = 0; i <= 2; i += Time.deltaTime) 
        {
            // set color with i as alpha  
            TextTitle.color = new Color(TEXTRGB.r, TEXTRGB.g, TEXTRGB.b, i);
            TextDisc.color = new Color(TEXTRGB.r, TEXTRGB.g, TEXTRGB.b, i);

            yield return null;
        }
        StartCoroutine(FadeTextOut());
    }

    private IEnumerator FadeTextOut()
    {
        for (float i = 2; i >= 0; i -= Time.deltaTime) 
        {
            // set color with i as alpha  
            TextTitle.color = new Color(TEXTRGB.r, TEXTRGB.g, TEXTRGB.b, i);
            TextDisc.color = new Color(TEXTRGB.r, TEXTRGB.g, TEXTRGB.b, i);

            yield return null;
        }
    }
}
