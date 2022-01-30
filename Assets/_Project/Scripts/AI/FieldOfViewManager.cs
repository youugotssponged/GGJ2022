using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewManager : MonoBehaviour
{
    public GameObject PlayerObject;
    public GameObject Enemy;
    private MoveTo EnemyMoveToClass;

    private Ray ray;
    // Start is called before the first frame update
    void Start()
    {
        ray = new Ray(Enemy.transform.position, (PlayerObject.transform.position - Enemy.transform.position).normalized * 20);
        EnemyMoveToClass = Enemy.GetComponent<MoveTo>();
    }

    // Update is called once per frame
    void Update()
    {
        ray.origin = Enemy.transform.position;
        ray.direction = (PlayerObject.transform.position - Enemy.transform.position).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == PlayerObject)
        {
            int layerMask = 1 << 2;

            layerMask = ~layerMask;

            //Debug.DrawRay(ray.origin, ray.direction * 20, Color.red, 2f);

            RaycastHit hitData;
            bool hit = Physics.Raycast(ray, out hitData, Mathf.Infinity, layerMask);


            if (hit)
            {
                if (hitData.collider.gameObject == PlayerObject)
                {
                    EnemyMoveToClass.CurrentChaseState = MoveTo.ChaseState.Chasing;
                }
            }

            //Vector3 pos = PlayerObject.transform.position;
            //Vector3 dir = (Enemy.transform.position - PlayerObject.transform.position).normalized;
            //Debug.DrawLine(pos, pos + dir * 10, Color.red, Mathf.Infinity);

            //Debug.DrawRay(Enemy.transform.position, Vector3.forward, Color.red);

            //RaycastHit hit;
            //// Does the ray intersect any objects excluding the player layer
            //if (Physics.Raycast(transform.position, direction, out hit))
            //{
            //    Debug.Log("Did Hit");
            //}
            //else
            //{
            //    Debug.Log("Did not Hit");
            //}

            //EnemyMoveToClass.CurrentChaseState = MoveTo.ChaseState.Chasing;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == PlayerObject)
        {
            int layerMask = 1 << 2;
            layerMask = ~layerMask;

            RaycastHit hitData;
            bool hit = Physics.Raycast(ray, out hitData, Mathf.Infinity, layerMask);


            if (hit)
            {
                if (hitData.collider.gameObject == PlayerObject)
                    EnemyMoveToClass.CurrentChaseState = MoveTo.ChaseState.Chasing;
                else
                    EnemyMoveToClass.CurrentChaseState = MoveTo.ChaseState.Searching;
            }
            else
                EnemyMoveToClass.CurrentChaseState = MoveTo.ChaseState.Searching;
        }
            
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == PlayerObject)
        {
            EnemyMoveToClass.CurrentChaseState = MoveTo.ChaseState.Searching;
        }
    }
}
