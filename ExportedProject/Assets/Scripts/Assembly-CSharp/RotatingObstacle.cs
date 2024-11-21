using System;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
	[SerializeField]
	[Range(-100f, 100f)]
	private float rotaionSpeed;

	[SerializeField]
	[Range(0f, 10f)]
	private float movementFactor;

	[SerializeField]
	private Vector3 movementVector;

	private Vector3 startingPosition;

	[SerializeField]
	private float period;

	private void Start()
	{
		startingPosition = base.transform.position;
	}

	private void Update()
	{
		base.transform.Rotate(0f, 0f, rotaionSpeed * 10f * Time.deltaTime);
		if (period != 0f)
		{
			float num = Mathf.Sin(Time.time / period * (MathF.PI * 2f));
			movementFactor = (num + 1f) / 2f;
			Vector3 vector = movementVector * movementFactor;
			base.transform.position = startingPosition + vector;
		}
	}
}
