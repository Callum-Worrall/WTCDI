﻿using UnityEngine;
using System.Collections;

public class AttackBox : MonoBehaviour 
{
	float m_total;
	bool m_collided;
	// Use this for initialization
	void Start () 
	{
		m_total = 1.0f;
		m_collided = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_total -= 1.0f * Time.deltaTime;

		if(m_total <= 0)
		{
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter (Collider a_other)
	{
		if(m_collided == false)
		{
			if(a_other.collider.name == "Cat")
			{
				GameObject player = (GameObject)a_other.gameObject;
				player.GetComponent<Player>().AdjustCurrentHealth(-1);
				m_collided = true;
			}

		}

	}
}
