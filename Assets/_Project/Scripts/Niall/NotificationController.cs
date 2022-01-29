using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class NotificationController : MonoBehaviour
{
    public GameObject player;
    public static Dictionary<GameObject, Canvas> gameObjectHints = new Dictionary<GameObject, Canvas>();
    public Canvas gameCanvas;
    public Text notifPrefab;
    private List<Text> notifs = new List<Text>() {};
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (InputMapper.GetKeyDown("Interact")) {
            ShowNotification("Picked up key");
        }
        foreach (KeyValuePair<GameObject, Canvas> pair in gameObjectHints) {
            pair.Value.GetComponentInChildren<Text>().transform.RotateAround(pair.Key.transform.position, Vector3.up, Time.deltaTime * 45.0f);
        }
    }

    void ShowHintOnGameObject(GameObject gameObject, string hint) {
        Canvas c = Instantiate(gameCanvas, gameObject.transform);
        Text t = c.GetComponentInChildren<Text>();
        t.text = hint;
        gameObjectHints.Add(gameObject, c);
    }

    void RemoveHintOnGameObject(GameObject gameObject) {
        gameObjectHints.Remove(gameObject);
    }

    void ShowNotification(string msg) {
        Text notif = Instantiate(notifPrefab, gameCanvas.transform);
        notif.text = msg;
        notif.transform.Translate(Vector3.right * 100 * (notifs.Count));
        notifs.Add(notif);
        StartCoroutine(FadeInNotif(notif));
    }

    private IEnumerator FadeInNotif(Text notif) {
        for (float i = 0; i < 2; i += Time.deltaTime) {
            notif.color = new Color(255, 0, 0, i);
            yield return null;
        }
        StartCoroutine(FadeOutNotif(notif));
    }

    private IEnumerator FadeOutNotif(Text notif) {
        yield return new WaitForSeconds(1);
        for (float i = 3; i > 0; i -= Time.deltaTime) {
            notif.color = new Color(255, 0, 0, i);
            yield return null;
        }
        Destroy(notif.gameObject);
        notifs.Remove(notif);
        foreach (Text n in notifs)
            n.transform.Translate(Vector3.right * -100);
    }

}
