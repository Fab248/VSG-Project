/*
Script name : EnemyPatternScript
Author : Fabien Sacriste
Summary : The aim of this script is to send the number of enemies present in the enemyPattern.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPatternScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	public List<GameObject> getListOfEnemies()
	{
		
		List<GameObject> listOfEnemies = new List<GameObject>();
		
		// Get the enemies and put them in a list before to send them.
		EnemyScript[] enemies = this.GetComponentsInChildren<EnemyScript>();
		
		foreach(EnemyScript enemy in enemies)
		{
			listOfEnemies.Add(enemy.gameObject);
		}
		
		return listOfEnemies;
	 }
	 
	 public void setPlayerIsDead(bool isDead)
	 {
		// Get the enemies and put them in a list before to send them.
		EnemyScript[] enemies = this.GetComponentsInChildren<EnemyScript>();
		
		foreach(EnemyScript enemy in enemies)
		{
			enemy.setPlayerIsDead(isDead);
		}
	 }
}
