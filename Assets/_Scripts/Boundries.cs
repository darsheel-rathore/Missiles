using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundries : MonoBehaviour {

	private float[,] mainQuad = new float[4,2];
	private float multiProduct;


	public Vector2 SetUpBoundry()
	{
		mainQuad = new float[,] {{1, 1}, {1, -1}, {-1, 1}, {-1, -1}};

		float newDeltaX, newDeltaY;
		Vector2 finalVector = new Vector2(0, 0);

		switch(Random.Range(1, 3))
		{

			// Case for Left || Right
			case 1:
				newDeltaX = RandomCalc(1f, 1.5f) * mainQuad[Random.Range(0, 4), 0];
				newDeltaY = RandomCalc(0f, 1f) * mainQuad[Random.Range(0, 4), 1];
				finalVector =  (new Vector2(newDeltaX, newDeltaY) + new Vector2(0.5f, 0));
				break;

			// Case for Top || Down
			case 2:
				newDeltaX = RandomCalc(0f, 1f) * mainQuad[Random.Range(0, 4), 0];
				newDeltaY = RandomCalc(1f, 1.5f) * mainQuad[Random.Range(0, 4), 1];
				finalVector =  (new Vector2(newDeltaX, newDeltaY) + new Vector2(0, 0.5f));
				break;
		}
		return Camera.main.ViewportToWorldPoint(finalVector);
	}
	

	private float RandomCalc(float min, float max)
	{
		float x = Random.Range(min, max);
		return x;
	}



}
