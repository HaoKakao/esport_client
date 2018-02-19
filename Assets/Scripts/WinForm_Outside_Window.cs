using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows.Forms;
using System.Drawing;

public class WinForm_Outside_Window : MonoBehaviour {

	System.Windows.Forms.Button button1;
	Form form;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.W)) {
			form = new Form();

			button1 = new System.Windows.Forms.Button ();

			button1.Name = "Button1";
			button1.Text = "Testuj";
			button1.Location = new Point (50, 60);
			button1.Width = 80;
			form.Controls.Add (button1);


			form.ShowDialog ();
		}
	}
}
