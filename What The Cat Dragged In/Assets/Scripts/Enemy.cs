using UnityEngine;
using System.Collections;

//Enemy enums
enum Facing
{
	LEFT = -1,
	RIGHT = 1
};

public enum Condition
{
	ACTIVE,
	STUNNED,
	DEAD,
	PICKEDUP
};

public class Enemy : MonoBehaviour 
{
	Facing m_facing; // which way the enemy is facing
	public Condition m_states; // is enemy alive or dead, etc
	GameObject m_player; // the enemy knows the player object
	public float m_speed; // hows fast it moves
	public GameObject m_attack; // the prefab for the AttackBox
	public int m_maxHealth;
	public int m_currentHealth;

	// Use this for initialization
	void Start () 
	{
		m_player = GameObject.Find("Cat").gameObject;
		m_facing = Facing.LEFT;
		m_states = Condition.ACTIVE;
	}

	void Follow(GameObject a_other) // The enemy will follow whatever is passed in
	{
		Vector2 pos = transform.position;
		Quaternion rot = transform.rotation;
		Vector2 otherPos = a_other.transform.position;
		rot.z = 0;
		
		if(otherPos.x < pos.x )
		{
			m_facing = Facing.LEFT;
		}
		
		if(otherPos.x > pos.x )
		{
			m_facing = Facing.RIGHT;
		}
		
		pos.x += (m_speed * (float)m_facing) * Time.deltaTime; // modfiy the x via which way the enemy is facing
		
		transform.position = pos;
		transform.rotation = rot;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (transform.parent != null)
		{
			m_states = Condition.PICKEDUP;
			transform.tag = "PickedUp";
		}


		if(m_currentHealth <= 0 && transform.parent == null)
		{
			m_states = Condition.DEAD;
			Material matColor = renderer.material;
			matColor.color = new Color(1,0,0);
			renderer.material = matColor;
			if (!transform.CompareTag("Corpse"))
			{
				transform.tag = "Corpse";
			}
		}
		else if(m_currentHealth <= 10 * m_maxHealth / 100 && transform.parent == null) // If below a certain percentage the enemy will be stunned
		{
			m_states = Condition.STUNNED;
			Material matColor = renderer.material;
			matColor.color = new Color(0,1,1);
			renderer.material = matColor;
			if (!transform.CompareTag("Stunned"))
			{
				transform.tag = "Stunned";
			}
		}

		if(m_states == Condition.ACTIVE && transform.parent == null)
		{
			Material matColor = renderer.material;
			matColor.color = new Color(1,1,1);
			renderer.material = matColor;
			Follow(m_player);
			if (!transform.CompareTag("Enemy"))
			{
				transform.tag = "Enemy";
			}
		}
	}
	
	void Attack()
	{
		Vector2 pos = transform.position;
		Quaternion rot = transform.rotation;

		int isAttacking = transform.childCount; // check if the enemy has any attackbox

		if(isAttacking > 0)
		{
			return;
		}

		GameObject attack = (GameObject)Instantiate(m_attack, pos + new Vector2(1 * (float)m_facing, pos.y), rot); // create the attackbox
		attack.transform.parent = transform;


	}

	void OnTriggerStay (Collider a_other) 
	{
		if(a_other.collider.name == "Cat" && m_states == Condition.ACTIVE) // if the player object comes within range of the enemies attack range
		{
			Attack ();
		}
	}

	public void AdjustCurrentHealth(int health)
	{
		m_currentHealth += health;
	}

	public int GetCondition()
	{
		if (m_states == Condition.ACTIVE)
		{
			return 1;
		}

		if (m_states == Condition.STUNNED)
		{
			return 0;
		}

		if (m_states == Condition.DEAD)
		{
			return -1;
		}

		if (m_states == Condition.PICKEDUP)
		{
			return -2;
		}

		return -1;
	}

	public void SetCondition(int state)
	{
		if (state == 1)
		{
			m_states = Condition.ACTIVE;
		}
		
		if (state == 0)
		{
			m_states = Condition.STUNNED;
		}
		
		if (state == -1)
		{
			m_states = Condition.DEAD;
		}

		if (state == -2)
		{
			m_states = Condition.PICKEDUP;
		}
	}
}
