using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	public static Player Instance;

	public int Id;
	public string Nickname;
	public string Activesession;
	public string Machinename;

	void Awake()
	{
		if (Instance == null)
			Instance = this;

		if (Instance != this)
			Destroy(gameObject);

	}

	//At start, load data from GlobalControl.
	void Start () 
	{

	}

	void Update()
	{

	}

}