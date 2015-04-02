using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour
{

	public bool pickedUp;

	// Use this for initialization
	void Start ()
	{
		pickedUp = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetButtonDown("Pickup"))
		{
			if (pickedUp == false)
			{
				PickUp();
			}
			else
				Drop();
		}
	}

	void PickUp()
	{
		GameObject[] Corpse = GameObject.FindGameObjectsWithTag("Stunned");

		for (int i = 0; i < Corpse.Length; i++)
		{
			if (Corpse[i].transform.position.x < (transform.position.x + 1.25f) &&
			    Corpse[i].transform.position.x > (transform.position.x - 1.25f) && pickedUp == false)
			{
				Corpse[i].transform.parent = transform;

				pickedUp = true;
			}
		}
	}

	void Drop()
	{
		transform.GetChild(0).GetComponent<Enemy>().SetCondition(0);

		transform.GetChild(0).parent = null;

		pickedUp = false;
	}
}
