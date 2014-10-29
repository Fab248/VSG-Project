/*
Script name : EnemyScript
Author : Fabien Sacriste
Summary : Behavior of an enemy. Only shoot when he sees the player.
			Speed of the ammo, the interval between two shoots and kind of ammo can be selected
			in order to have various kind of enemies.
*/
using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public enum ammo{Bullet, Rocket};
	
	private GameObject player;
	private Vector3 playersDirection;
	private RaycastHit hit;
	private float counterIntervalShoots = 0.0f;
	
	public ammo KindOfAmmo;
	public float intervalBetweenTwoShoots = 0.0f;
	public float ammoSpeed = 0.0f;
	public float rocketDistanceBeforeAutoDestructionMin = 0.0f;
	public float rocketDistanceBeforeAutoDestructionMax = 0.0f;
	
	// Use this for initialization
	void Start () {
	
		// Get the player, to get its position later on.
		player = GameObject.FindGameObjectWithTag("Player");
		
		ammoSpeed /= 100.0f;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// Define the direction of the player from enemy's point of view
		playersDirection = player.transform.position - this.transform.position;
		
		// Launch a ray toward the player
		if(Physics.Raycast(this.transform.position, playersDirection, out hit))
		{
			// Test if the player has been seen and if the enemy can shoot
			if(hit.transform.position == player.transform.position && counterIntervalShoots == 0.0f)
			{
				shoot (this.transform.position, playersDirection, ammoSpeed);
				// Start the interval with the second shoot
				counterIntervalShoots = intervalBetweenTwoShoots;
			}
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
	private void shoot(Vector3 startPosition, Vector3 direction, float speed)
	{
		// Create the bullet
		GameObject bullet = (GameObject)Instantiate(Resources.Load("Prefabs/Ammo/"+KindOfAmmo+"Prefab"));
		
		if(KindOfAmmo == ammo.Bullet)
		{
			BulletScript bulletOptions = bullet.GetComponent<BulletScript>();
			
			startPosition.x += this.transform.localScale.x/2 + 0.2f;
			startPosition.y -= 0.1f;
			bulletOptions.setStartPos(startPosition);
			bulletOptions.setSpeed(speed);
			bulletOptions.setDirection(direction);
			bulletOptions.setRatio();
		}
		else
		{
			RocketScript rocketOptions = bullet.GetComponent<RocketScript>();
			
			startPosition.x += this.transform.localScale.x/2 + 0.5f;
			startPosition.y -= 0.1f;
			rocketOptions.setStartPos(startPosition);
			rocketOptions.setSpeed(speed);
			rocketOptions.setDirection(direction);
			rocketOptions.setRatio();
			rocketOptions.setDistanceBeforeAutoDestruction(rocketDistanceBeforeAutoDestructionMin, rocketDistanceBeforeAutoDestructionMax);
		}
		
	}
}
