/*
Script name : BulletScript
Author : Fabien Sacriste
Summary : Move the bullet from its position toward the direction set.
*/
using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	private Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);
	private float speed = 0.0f;
	private Vector3 ratio = new Vector3(0.0f, 0.0f, 0.0f);
	
	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		this.transform.position = new Vector3(this.transform.position.x + ratio.x * Time.deltaTime, this.transform.position.y + ratio.y * Time.deltaTime, this.transform.position.z + ratio.z * Time.deltaTime);
		
	}
	
	// Set the departure of the bullet
	public void setStartPos(Vector3 position)
	{
		this.transform.position = position;
	}
	
	// Set the direction of the bullet
	public void setDirection(Vector3 position)
	{
		direction = position;
	}
	
	// Set the speed of the bullet
	public void setSpeed(float velocity)
	{
		speed = velocity;
	}
	
	// Set the ratio between the speed and the direction
	public void setRatio()
	{
		ratio = direction * speed;
	}
	
	// If the bullet interacts with something else than another bullet or a rocket, it gets destroyed
	void OnCollisionEnter (Collision collision)
	{
		if(collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Rocket")
			Destroy(this.gameObject);
	}
}
