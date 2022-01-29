using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationController : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> gameObjectHints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject obj in gameObjectHints) {
            obj.transform.LookAt(player.transform);
        }
    }

    void ShowHintOnGameObject(string hint, GameObject gameObject) {
        gameObjectHints.Add(gameObject);
    }

}
