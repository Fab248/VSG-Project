/*
Script name : LevelPattern3Script
Author : Fabien Sacriste
Summary : Shoot a bullet across the screen, from a side to the other.
			3 slots are set, and are randomly used to define the departure of the bullet.
*/
using UnityEngine;
using System.Collections;

public class LevelPattern3Script : MonoBehaviour {
	
	public enum direction {RIGHT, LEFT};
	
	private GameObject[] arrayOfSlots = new GameObject[3];
	private GameObject bullet;
	private bool bulletShot = false;
	private int bulletDirection = 0;
	
	public float bulletSpeed;
	public direction wayToShoot;
	
	
	// Use this for initialization
	void Start () {
	
		// Get the different objects
		bullet = this.transform.FindChild("Bullet").gameObject;
		arrayOfSlots[0] = this.transform.FindChild("Slot1").gameObject;
		arrayOfSlots[1] = this.transform.FindChild("Slot2").gameObject;
		arrayOfSlots[2] = this.transform.FindChild("Slot3").gameObject;
		
		// Define the direction of the bullet
		bulletDirection = (wayToShoot == direction.RIGHT)?1:-1;
	
	}
	
	// Update is called once per frame
	void Update () {
	
		// Test if a bullet has been shot already, if not a slot is picked up randomly and used as a departure for the bullet
		if(bulletShot == false)
		{
			bullet.transform.localPosition = arrayOfSlots[Random.Range(0,3)].transform.localPosition;
			bulletShot = true;
		}
		
		// Move the bullet
		bullet.transform.localPosition = new Vector3(bullet.transform.localPosition.x + (Time.deltaTime * bulletSpeed * bulletDirection), bullet.transform.localPosition.y, bullet.transform.localPosition.z);
		
		// If the bullet is off the screen bulletShot is defined to false so it can be shot at the next frame
		if(!isVisibleFrom(bullet.renderer, Camera.main))
			bulletShot = false;
	
	}
	
	public bool isVisibleFrom(Renderer renderer, Camera camera)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
	}
}
