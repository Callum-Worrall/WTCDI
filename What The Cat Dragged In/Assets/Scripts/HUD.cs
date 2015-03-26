using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnGUI()
	{
		//Stamina Bar
		GameObject player = GameObject.Find("Cat");
		
		int currentHealth = player.GetComponent<Player>().m_currentHealth;
		int maxHealth = player.GetComponent<Player>().m_maxHealth;
		
		if (currentHealth > 0)
		{
			GUI.Box(new Rect(10, 10, 20 * currentHealth, 20), "");
		}
		
		GUI.Box(new Rect(10, 10, 20 * maxHealth, 20), currentHealth.ToString() + "/" + maxHealth.ToString());
	}
}
