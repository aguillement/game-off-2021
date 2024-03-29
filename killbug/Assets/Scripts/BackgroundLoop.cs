using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
	public float scrollSpeed;
	public float scrollOffset;
	Vector3 startPos;

	float newPos;

	void Start()
	{
		startPos = transform.position;
	}

	void Update()
	{
		newPos = Mathf.Repeat(Time.time * -scrollSpeed, scrollOffset);

		transform.position = startPos + Vector3.up * newPos;
	}
}
