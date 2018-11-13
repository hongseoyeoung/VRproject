using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_move : MonoBehaviour {

    private float moveSpeed = 10f;

    public Ray _GunRay;

    void Start () {
        //_GunController = GameObject.Find("Gun").GetComponent<GunController>();
        _GunRay = GameObject.Find("Gun").GetComponent<GunController>().ray;

        //태그로 찾기
        //_GunController = GameObject.FindWithTag("Gun").GetComponent<GunController>().ray;

        Destroy(gameObject, 2f);
	}
	
	void Update () {
        move();
	}

    private void move()
    {
        //ray 방향으로 총알 이동
        transform.Translate(_GunRay.direction * moveSpeed * Time.deltaTime);

    }
}
