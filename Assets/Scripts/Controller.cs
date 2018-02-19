using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO; // Potrzebne do dzialanai socket.io
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class Controller : MonoBehaviour {

	public ButtonLogin login;
	public SocketIOComponent socket; // uzywa class socketa.io

	string JsonToString (string target, string s){
		string[] newString = Regex.Split (target, s);
		return newString [1];
	}


	// Use this for initialization
	void Start () {
		socket.On("PLAY", OnUserPlay);
		login.LoginButton.onClick.AddListener (loginstr); // pobiera wartosci po klikniecia buttona zaloguj
	}

	void Update () {
			if (login.LoginInput.GetComponent<InputField> ().isFocused == true) {
				if (Input.GetKeyDown (KeyCode.Tab)) {
					login.PasswordInput.ActivateInputField ();
				}
			}

			if (Input.GetKeyDown (KeyCode.Return)) {
				if (login.LoginInput.text.Length > 3 && login.PasswordInput.text.Length < 3) {
					login.PasswordInput.ActivateInputField ();
				} else if (login.LoginInput.text.Length > 3 && login.PasswordInput.text.Length > 3) {
					loginstr ();
					Debug.Log (login.LoginInput.text);
				}
			}
	}

		

	void loginstr(){
		Dictionary<string, string> data = new Dictionary<string, string> ();
		data["name"] = login.LoginInput.text; // Pobieramy wartosc z inputa login
		data["password"] = login.PasswordInput.text; // Pobieramy wartosc z inputa haslo
		data["machine_name"] = Environment.MachineName; // Environment.MachineName - nazwa komputera
		socket.Emit ("PLAY", new JSONObject (data)); // Wysyłamy wartosci do servera

	}
		

	private void OnUserPlay( SocketIOEvent evt){
		Debug.Log ("Message from server is: " + evt.data +" OnUserPlay"); // Odbieramy sprecyzowane data od servera - evt.data.GetField("name")
		Player.Instance.Id = Int16.Parse(evt.data.GetField ("id").ToString ());
		Player.Instance.Nickname = JsonToString (evt.data.GetField ("username").ToString (), "\"");
		Player.Instance.Activesession = JsonToString (evt.data.GetField ("active_session").ToString (), "\"");
		Player.Instance.Machinename = Environment.MachineName;
		Application.LoadLevel("ClientScreen");
	
	
	}


}
