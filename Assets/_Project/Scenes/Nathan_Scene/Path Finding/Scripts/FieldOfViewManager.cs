using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewManager : MonoBehaviour
{
    public GameObject PlayerObject;
    public GameObject Enemy;
    private MoveTo EnemyMoveToClass;
    // Start is called before the first frame update
    void Start()
    {
        EnemyMoveToClass = Enemy.GetComponent<MoveTo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == PlayerObject)
        {
            Debug.Log("Follow Player");
            EnemyMoveToClass.CurrentChaseState = MoveTo.ChaseState.Chasing;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == PlayerObject)
        {
            Debug.Log("Stop Following Player");
            EnemyMoveToClass.CurrentChaseState = MoveTo.ChaseState.Searching;
        }
    }
}
