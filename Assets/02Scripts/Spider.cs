using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour {

    [HideInInspector]
    private Animation animation;

    private float moveSpeed = 1f;

	void Start () {
        animation = GetComponent<Animation>();
	}
	
	void Update () {
        move();   
	}

    private void move()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        animation.Play("walk");
    }
}
