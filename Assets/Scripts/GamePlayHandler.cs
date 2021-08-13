using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayHandler : MonoBehaviour
{
	int currentLevel = 0;

	public List<Transform> spawnPoints;
	public List<Transform> shootPoints;
	public List<GameObject> badGuys;
	public List<Level> level = new List<Level>();
	List<Enemy> enemies = new List<Enemy>();

	List <GameObject> enemiesInGame = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
    	enemies.Add(new Enemy(badGuys[0], spawnPoints[0], shootPoints[0]));
    	enemies.Add(new Enemy(badGuys[0], spawnPoints[1], shootPoints[1]));
    	enemies.Add(new Enemy(badGuys[0], spawnPoints[1], shootPoints[2]));
    	Debug.Log("enemies: "+ enemies.Count);

    	level.Add(new Level(2, enemies));
    	level[currentLevel].createWaves();
    	Debug.Log("level[currentLevel].waves: "+ level[currentLevel].waves.Count);

    	foreach(Enemy enemy in level[currentLevel].waves[0].enemiesInWave){
    		GameObject enemyObj = Instantiate(enemy.character, enemy.spawnPosition.position, Quaternion.identity);
			enemyObj.GetComponent<EnemyAiTutorial>().shootFromPosition = enemy.shootPosition;
			enemiesInGame.Add(enemyObj);
    	}
     //    	badGuys[0].GetComponent<EnemyAiTutorial>().shootFromPosition = shootPoints[0];
    }

    // Update is called once per frame
    void Update()
    {
    	foreach(GameObject enemyobj in enemiesInGame){
    		if (enemyobj.GetComponent<EnemyAiTutorial>().isDead == true){
    			enemiesInGame = new List<GameObject>();
		    	foreach(Enemy enemy in level[currentLevel].waves[1].enemiesInWave){
		    		GameObject enemyObj = Instantiate(enemy.character, enemy.spawnPosition.position, Quaternion.identity);
					enemyObj.GetComponent<EnemyAiTutorial>().shootFromPosition = enemy.shootPosition;
					enemiesInGame.Add(enemyObj);
		    	}
    		}
    	}

    	// if(level[currentLevel].levelCompleted) {
    	// 	currentLevel += 1;
    	// 	level[currentLevel].startNextWave();
    	// }

  //   	startLevel1();
  //   	if(wave == 1 && badGuys[0].GetComponent<EnemyAiTutorial>().isDead){
  //   		wave += 1;
  //   		instantiatedNextLevel = false;
  //   		startLevel2();
		// }
    }

    void startLevel1() {
    	// if(level == 1 && !instantiatedNextLevel){
    	// 	badGuys[0] = Instantiate(badGuys[0], spawnPoint1.position, Quaternion.identity);
     //    	badGuys[0].GetComponent<EnemyAiTutorial>().shootFromPosition = shootPoint1;
     //    	instantiatedNextLevel = true;
    	// }

    }

    void startLevel2() {
    	//if(level == 2 && !instantiatedNextLevel){
    		// instantiatedNextLevel = true;
    		// if(badGuys[0].GetComponent<EnemyAiTutorial>().isDead){
	    	// 	badGuys[1]= Instantiate(badGuys[1], spawnPoint2.position, Quaternion.identity);
	     //  		badGuys[1].GetComponent<EnemyAiTutorial>().shootFromPosition = shootPoint2;
	     //  		badGuys[2]= Instantiate(badGuys[2], spawnPoint2.position, Quaternion.identity);
	     //  		badGuys[2].GetComponent<EnemyAiTutorial>().shootFromPosition = shootPoint3;
	    	// }
    	//}
    }
}
