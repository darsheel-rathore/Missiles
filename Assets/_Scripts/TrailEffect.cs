using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEffect : MonoBehaviour {

	Rigidbody2D rb;

    void Start()
	{
		rb = GetComponent<Rigidbody2D>();
    }

	public void UpdatePosition(Vector3 missilePosition)
	{
		transform.position = missilePosition - transform.forward;
	}

	public void UpdateAngularVelocity(float angularVelocity)
	{
		rb.angularVelocity = angularVelocity;
	}

}
