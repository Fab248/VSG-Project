/*
Script name : PlayerScript
Author : Fabien Sacriste
Summary : Manage the player. The player can move by pressing "Space", and the autoFire is on.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

	private LevelManagerScript levelManager;
	private float counterIntervalShoots = 0.0f;
	private Vector3 maxPos = new Vector3(0.0f, 0.0f, 0.0f);
	
	public float intervalBetweenTwoShoots = 0.0f;
	public float ammoSpeed = 0.0f;
	public float levelMovementSpeed = 0.0f;
	public float fallingSpeed = 0.0f;
	
	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManagerScript>();
		ammoSpeed /= 100.0f;
		maxPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKey(KeyCode.Space))
		{
			if(this.transform.position.y < maxPos.y)
			{
				this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + (Time.deltaTime * fallingSpeed * -1), this.transform.position.z);
			}
			else
			{
				this.transform.position = maxPos;
				levelManager.moveLevels(levelMovementSpeed);
			}
		}
		else
		{
			this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + (Time.deltaTime * fallingSpeed), this.transform.position.z);
		}
		
		List<GameObject> listOfEnemies = new List<GameObject>();
		
		listOfEnemies = levelManager.getListOfEnemies();
		
		GameObject nearestEnemy = null;
		
		float distance = int.MaxValue;
		float tempDistance = 0.0f;
		Vector3 enemysDirection = new Vector3(0.0f, 0.0f, 0.0f);
		RaycastHit hit;
		
		for(int i = 0; i < listOfEnemies.Count; ++i)
		{
			// Define the direction of the enemy from player's point of view
			enemysDirection = listOfEnemies[i].transform.position - this.transform.position;
			
			// Launch a ray toward the enemy
			if(Physics.Raycast(this.transform.position, enemysDirection, out hit))
			{
				// Test if the enemy has been seen
				if(hit.transform.position == listOfEnemies[i].transform.position )
				{
					tempDistance = Vector3.Distance(this.transform.position, listOfEnemies[i].transform.position);
					
					if(tempDistance < distance)
					{
						distance = tempDistance;
						nearestEnemy = listOfEnemies[i];
					}
//					shoot (this.transform.position, enemysDirection, ammoSpeed);
//					
				}
			}
		}
		
		//Debug.Log(nearestEnemy.name);
		if(nearestEnemy != null && counterIntervalShoots == 0.0f)
		{
			// Define the direction of the player from enemy's point of view
			enemysDirection = nearestEnemy.transform.position - this.transform.position;
			
			shoot (this.transform.position, enemysDirection, nearestEnemy.transform.position, ammoSpeed);
			// Start the interval with the second shoot
			counterIntervalShoots = intervalBetweenTwoShoots;
		}
		
		// Test if the counter for the interval has to be decreased
		if(counterIntervalShoots > 0.0f)
		{
			// The counter is decreased
			counterIntervalShoots -= Time.deltaTime;
			
			counterIntervalShoots =(counterIntervalShoots < 0.0f)?0.0f:counterIntervalShoots;
		}
	}
	
	// Shoot the kind of ammo selected.
	private void shoot(Vector3 startPosition, Vector3 direction, Vector3 enemyPosition, float speed)
	{
		// Create the bullet
		GameObject bullet = (GameObject)Instantiate(Resources.Load("Prefabs/Ammo/BulletPrefab"));
		
		int directionToAim = ((enemyPosition.x - startPosition.x)>0)?1:-1;
		
		startPosition.x +=  directionToAim * (this.transform.localScale.x/2 + 0.2f);
		startPosition.y -= 0.1f;
		
		BulletScript bulletOptions = bullet.GetComponent<BulletScript>();
		
		bulletOptions.setStartPos(startPosition);
		bulletOptions.setSpeed(speed);
		bulletOptions.setDirection(direction);
		bulletOptions.setRatio();
		
	}
	
	// If the bullet interacts with something else than another bullet or a rocket, it gets destroyed
	void OnCollisionEnter (Collision collision)
	{
		if(collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Rocket" || collision.gameObject.tag == "BorderOfScreen")
		{	
			levelManager.setIsPlayerDead(true);
			Destroy(this.gameObject);
		}
	}
}
