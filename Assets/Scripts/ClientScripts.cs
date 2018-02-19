using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SocketIO;
using System.Text.RegularExpressions;

public class ClientScripts : MonoBehaviour {

	public GameObject[] gameobj;

	string JsonToString (string target, string s){
		string[] newString = Regex.Split (target, s);
		return newString [1];
	}

	public class Part : IEquatable<Part>
	{
		public int ID { get; set; }
		public string Username { get; set; }
		public int Online { get; set; }

		public override string ToString()
		{
			return "ID: " + ID + " Username: " + Username + " Online: " + Online;
		}
		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			Part objAsPart = obj as Part;
			if (objAsPart == null) return false;
			else return Equals(objAsPart);
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		public bool Equals(Part other)
		{
			if (other == null) return false;
			return (this.ID.Equals(other.ID));
		}
		// Should also override == and != operators.

	}

	List<Part> parts = new List<Part>();

	// Use this for initialization
	void Start () {
		SocketIOComponent socket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();
		socket.Emit ("LOLEK");
		socket.On("FLISTXXXXXX", FriendsList);
		socket.On("LF_Load_Friend", LF_Load_Friend);
		Debug.Log ("Message froxdasdsadasd");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void FriendsList( SocketIOEvent evt)
	{
		string statustext;
		for (int i = 0; i < evt.data.Count; i++)
		{
			int frid = Int32.Parse(evt.data[i].GetField("id").ToString());
			string frname = JsonToString (evt.data [i].GetField ("username").ToString (), "\"");
			int fron = Int32.Parse(evt.data [i].GetField ("online").ToString ());


				parts.Add(new Part() { ID = frid, Username = frname, Online = fron });

		}

		parts.Sort(delegate(Part x, Part y)
			{
				if (x.Online == y.Online)
				{
					return x.Username.CompareTo(y.Username);
				}
				else
				{
					return y.Online.CompareTo(x.Online);
				}

			});
		for (int i = 0; i < parts.Count; i++)
		{
			gameobj = new GameObject[parts.Count];
			gameobj[i] = Instantiate (Resources.Load ("PlayerTab")) as GameObject;
			gameobj[i].name = parts[i].Username;
			GameObject canvas = GameObject.Find("Canvas/FriendsList/Viewport/Content");
			gameobj[i].transform.SetParent(canvas.transform, false);
			GameObject Screenname = GameObject.Find("Canvas/FriendsList/Viewport/Content/" + parts[i].Username + "/NickName");
			Screenname.GetComponent<Text> ().text = parts[i].Username;
			GameObject Screenstatus = GameObject.Find("Canvas/FriendsList/Viewport/Content/" + parts[i].Username + "/Status");
			if (parts[i].Online == 1) {
				statustext = "<color=#48a42c>Online</color>";
			} else {
				statustext = "<color=#4c4c4c>Offline</color>";
			}
			Screenstatus.GetComponent<Text> ().text = statustext;


		}
			
		foreach (Part aPart in parts)
		{
			Debug.Log (aPart);
		}

	}



	private void LF_Load_Friend( SocketIOEvent evt)
	{
		string statustext;

		int frid = Int32.Parse(evt.data.GetField("id").ToString());
		string frname = JsonToString (evt.data.GetField ("username").ToString (), "\"");
		int fron = Int32.Parse(evt.data.GetField ("online").ToString ());
		for(int i=0;i<parts.Count;i++)
		{
			if (parts[i].ID == frid) 
			{
				parts[i].Online = fron;
			}
		}

		parts.Sort(delegate(Part x, Part y)
			{
				if (x.Online == y.Online)
				{
					return x.Username.CompareTo(y.Username);
				}
				else
				{
					return y.Online.CompareTo(x.Online);
				}

			});

		if (fron == 1) {
			statustext = "<color=#48a42c>Online</color>";
		} else {
			statustext = "<color=#4c4c4c>Offline</color>";
		}
		GameObject ChangeToOnline = GameObject.Find("Canvas/FriendsList/Viewport/Content/" + frname + "/Status");
		ChangeToOnline.GetComponent<Text> ().text = statustext;
		for (int i = 0; i < parts.Count; i++)
		{
			GameObject sortObjects = GameObject.Find("Canvas/FriendsList/Viewport/Content/" + parts[i].Username);
			sortObjects.transform.SetSiblingIndex (i);
			
		}
	}
}
