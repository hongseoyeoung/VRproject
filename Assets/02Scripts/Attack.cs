using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    //public Enemy _Enemy;

    private float damage = 20f;

	void Start () {
        //_Enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
	}
	
	void Update () {
       
	}

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("공격");

            Enemy _Enemy = col.transform.GetComponent<Enemy>();
            if(_Enemy != null)
            {
                _Enemy.TakeDamage(damage);
            }
        }
    }
}
