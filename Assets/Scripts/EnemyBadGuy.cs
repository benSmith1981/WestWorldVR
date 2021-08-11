using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBadGuy : MonoBehaviour
{
	public GameObject mEnemy;
    // Start is called before the first frame update
    void Start()
    {
    	mEnemy = transform.GetChild(0).gameObject;
    	moveUpwards();
    }

    void moveUpwards() {
    	// mEnemy = transform.GetChild(0).gameObject;
        Vector3 enemyPos = mEnemy.transform.position;
        enemyPos.y += 2.0f;

        iTween.MoveTo(mEnemy, enemyPos, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
