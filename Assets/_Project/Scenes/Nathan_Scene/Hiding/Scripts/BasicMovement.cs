using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
	public bool CanMove = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (CanMove)
        {

			var horizontal = Input.GetAxis("Horizontal");
			var vertical = Input.GetAxis("Vertical");
			transform.Translate(new Vector3(0, 0, vertical) * (2f * Time.deltaTime));
			transform.Rotate(new Vector3(0, horizontal, 0) * (200f * Time.deltaTime));
			
		}
		
	}

}
