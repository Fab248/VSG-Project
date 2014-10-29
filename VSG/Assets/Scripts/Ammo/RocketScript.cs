/*
Script name : RocketScript
Author : Fabien Sacriste
Summary : Move the Rocket from its position toward the direction set.
			Explodes at the end.
*/
using UnityEngine;
using System.Collections;

public class RocketScript : MonoBehaviour {

	private Vector3 startPos = new Vector3(0.0f, 0.0f, 0.0f);
	private Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);
	private float speed = 0.0f;
	private Vector3 ratio = new Vector3(0.0f, 0.0f, 0.0f);
	private float distanceBeforeAutoDestruction = 0.0f;
	private bool autoDestructionActivated = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!autoDestructionActivated)
			this.transform.position = new Vector3(this.transform.position.x + ratio.x * Time.deltaTime, this.transform.position.y + ratio.y * Time.deltaTime, this.transform.position.z + ratio.z * Time.deltaTime);
		
		// Test if the destruction has been activated and if the particles are done
		if(autoDestructionActivated && !this.GetComponent<ParticleSystem>().isPlaying)
		{
			destruction();
		}
		
		if(Vector3.Distance(startPos, this.transform.position) >= distanceBeforeAutoDestruction && !autoDestructionActivated)
		{
			autoDestructionActivated = true;
			this.GetComponent<SphereCollider>().enabled = true;
			this.GetComponent<CapsuleCollider>().enabled = false;
			this.GetComponent<Renderer>().enabled = false;
			this.GetComponent<ParticleSystem>().Play();
		}
	}
	
	// Set the departure of the rocket
	public void setStartPos(Vector3 position)
	{
		this.transform.position = position;
		startPos = position;
	}
	
	// Set the direction of the rocket
	public void setDirection(Vector3 position)
	{
		direction = position;
	}
	
	// Set the speed of the rocket
	public void setSpeed(float velocity)
	{
		speed = velocity;
	}
	
	// Set the ratio between the speed and the direction
	public void setRatio()
	{
		ratio = direction * speed;
	}
	
	// Set the distance before the rocket explodes by itself
	public void setDistanceBeforeAutoDestruction(float distanceMin, float distanceMax)
	{	
		distanceBeforeAutoDestruction = Random.Range(distanceMin, distanceMax);
	}
	
	// If the rocket interacts with something else than another bullet or a rocket, it gets destroyed
	void OnCollisionEnter (Collision collision)
	{
		if(collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Rocket")
		{
			autoDestructionActivated = true;
			this.GetComponent<SphereCollider>().enabled = true;
			this.GetComponent<CapsuleCollider>().enabled = false;
			this.GetComponent<Renderer>().enabled = false;
			this.GetComponent<ParticleSystem>().Play();
		}
	}
	
	// Destroy the rocket
	private void destruction()
	{
		Destroy(this.gameObject);
	}
}
