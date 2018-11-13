using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [HideInInspector]
    private Animation animation;
    //체력
    public float hp = 60f;
    //이동속도
    private float moveSpeed = 1f;

    private bool death = false;

    private int randomNum;

    void Start()
    {
        animation = GetComponent<Animation>();
    }

    private void Update()
    {
        if(!death)
            move();
    }

    private void move()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        animation.Play("walk");
    }


    public void TakeDamage(float amount)
    {
        hp -= amount;
        Debug.Log(hp);
        if (hp <= 0f)
        {
            Die();       
        }
    }

    private void Die()
    {
        randomNum = Random.Range(0, 2);
        if (randomNum == 0)
            animation.Play("death1");
        else if (randomNum == 1)
            animation.Play("death2");

        death = true;
        Destroy(gameObject, 2f);
    }
}
