/*
Script name : LevelPattern4Script
Author : Fabien Sacriste
Summary : 2 bullets bounce inbetween 2 walls indefinitely.
*/
using UnityEngine;
using System.Collections;

public class LevelPattern4Script : MonoBehaviour {

	private GameObject[] arrayOfSlots = new GameObject[2];
	private GameObject[] bullets = new GameObject[2];
	private Vector3[] startPos = new Vector3[2]{new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f)};
	private Vector3[] endPos = new Vector3[2]{new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f)};
	private float bullet1Direction = 1.0f;
	private float bullet2Direction = 1.0f;
	
	public float bulletSpeed;
	public bool synchronisedBullets = false;
	
	
	// Use this for initialization
	void Start () {
		
		// Get the objects
		arrayOfSlots[0] = this.transform.FindChild("Slot1").gameObject;
		arrayOfSlots[1] = this.transform.FindChild("Slot2").gameObject;
		
		bullets[0] = this.transform.FindChild("Bullet1").gameObject;
		bullets[1] = this.transform.FindChild("Bullet2").gameObject;
		
		GameObject wall1 = this.transform.FindChild("Wall1").gameObject;
		GameObject wall2 = this.transform.FindChild("Wall2").gameObject;
		
		
		// Define the start and end positions of the bullets
		startPos[0] = new Vector3(wall1.transform.localPosition.x + wall1.transform.localScale.x / 2.0f, arrayOfSlots[0].transform.localPosition.y, arrayOfSlots[0].transform.localPosition.z);
		endPos[0] = new Vector3(wall2.transform.localPosition.x - wall2.transform.localScale.x / 2.0f, arrayOfSlots[0].transform.localPosition.y, arrayOfSlots[0].transform.localPosition.z);
		
		startPos[1] = new Vector3(wall1.transform.localPosition.x + wall1.transform.localScale.x / 2.0f, arrayOfSlots[1].transform.localPosition.y, arrayOfSlots[1].transform.localPosition.z);
		endPos[1] = new Vector3(wall2.transform.localPosition.x - wall2.transform.localScale.x / 2.0f, arrayOfSlots[1].transform.localPosition.y, arrayOfSlots[1].transform.localPosition.z);
		
		// Set the position of the first bullet
		bullets[0].transform.position = startPos[0];
		
		// If bullets are synchronised, they will go in the same direction at the same time
		if(synchronisedBullets)
		{
			bullets[1].transform.localPosition = endPos[1];
			bullet2Direction = -1.0f;
		}
		else
		{
			bullets[1].transform.localPosition = startPos[1];
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		/*********************
			First bullet (top)
		*********************/
		//Move the bullet
		bullets[0].transform.localPosition = new Vector3(bullets[0].transform.localPosition.x + (Time.deltaTime * bulletSpeed * bullet1Direction), bullets[0].transform.localPosition.y, bullets[0].transform.localPosition.z);
		
		// The bullet has reached its end position
		if(bullets[0].transform.localPosition.x >= endPos[0].x)
		{
			// Set the position of the bullet to endPos in case of it went a bit farther
			bullets[0].transform.localPosition = new Vector3(endPos[0].x, bullets[0].transform.localPosition.y, bullets[0].transform.localPosition.z);
			// Change the direction
			bullet1Direction *= -1;
		}
		// The bullet has reached its start position
		else if(bullets[0].transform.localPosition.x <= startPos[0].x)
		{
			// Set the position of the bullet to startPos in case of it went a bit farther
			bullets[0].transform.localPosition = new Vector3(startPos[0].x, bullets[0].transform.localPosition.y, bullets[0].transform.localPosition.z);
			// Change the direction
			bullet1Direction *= -1;
		}
		
		/**************************
			Second bullet (bottom)
		**************************/
		//Move the bullet
		bullets[1].transform.localPosition = new Vector3(bullets[1].transform.localPosition.x + (Time.deltaTime * bulletSpeed * bullet2Direction), bullets[1].transform.localPosition.y, bullets[1].transform.localPosition.z);
		
		// The bullet has reached its end position
		if(bullets[1].transform.localPosition.x >= endPos[1].x)
		{
			// Set the position of the bullet to endPos in case of it went a bit farther
			bullets[1].transform.localPosition = new Vector3(endPos[1].x, bullets[1].transform.localPosition.y, bullets[1].transform.localPosition.z);
			// Change the direction
			bullet2Direction *= -1;
		}
		// The bullet has reached its start position
		else if(bullets[1].transform.localPosition.x <= startPos[1].x)
		{
			// Set the position of the bullet to startPos in case of it went a bit farther
			bullets[1].transform.localPosition = new Vector3(startPos[1].x, bullets[1].transform.localPosition.y, bullets[1].transform.localPosition.z);
			// Change the direction
			bullet2Direction *= -1;
		}
		
	}
}
