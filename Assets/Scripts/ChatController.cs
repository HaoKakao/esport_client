using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;
using System.Text.RegularExpressions;


public class ChatController : MonoBehaviour {



	public ChatClass chatclass;
	public GameObject[] gameobj;
	public int objnr = 5;


	string JsonToString (string target, string s){
		string[] newString = Regex.Split (target, s);
		return newString [1];
	}

	void Start () {
		SocketIOComponent socket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();
		socket.On("PLAYER_JOINED", PlayerJoined);
		socket.On("LOAD_MSG_TO_CHAT", LoadMSG);

	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			if (chatclass.SendTextChat.text.Length > 0) {
				SendMsg ();
			}
		}
	}

	void SendMsg(){
		SocketIOComponent socket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();
		if (chatclass.SendTextChat.text.Length > 0) {
			Dictionary<string, string> data = new Dictionary<string, string> ();
			data ["id"] = Player.Instance.Id.ToString(); // Pobieramy wartosc z inputa haslo
			data ["msg"] = chatclass.SendTextChat.text; // Pobieramy wartosc z inputa haslo
			socket.Emit ("CHAT_MSG", new JSONObject (data)); // Wysyłamy wartosci do servera
			chatclass.SendTextChat.text = "";
			chatclass.SendTextChat.ActivateInputField ();

		}
	
	}

	private void PlayerJoined( SocketIOEvent evt){

		chatclass.ChatWindow.text += "Player <b>";
		chatclass.ChatWindow.text += JsonToString(evt.data.GetField ("username").ToString(), "\"");
		chatclass.ChatWindow.text += "</b> joined.\n";

		chatclass.ChatText.text += "Player <b>";
		chatclass.ChatText.text += JsonToString(evt.data.GetField ("username").ToString(), "\"");
		chatclass.ChatText.text += "</b> joined.\n";

	}

	private void LoadMSG( SocketIOEvent evt){

		string color;
		if(JsonToString(evt.data.GetField ("active_session").ToString(), "\"") == Player.Instance.Activesession)
		{
			color = "<color=#cc9900>";
		}
		else
		{
			color = "<color=#0048a2>";
		}
		chatclass.ChatWindow.text += color + "[" + JsonToString(evt.data.GetField ("time").ToString(), "\"") + "] " + JsonToString(evt.data.GetField ("username").ToString(), "\"") + ":</color> ";
		chatclass.ChatWindow.text += JsonToString(evt.data.GetField ("message").ToString(), "\"") +"\n";

		chatclass.ChatText.text += color + "[" + JsonToString(evt.data.GetField ("time").ToString(), "\"") + "] " + JsonToString(evt.data.GetField ("username").ToString(), "\"") + ":</color> ";
		chatclass.ChatText.text += JsonToString(evt.data.GetField ("message").ToString(), "\"") +"\n";
	}

}
