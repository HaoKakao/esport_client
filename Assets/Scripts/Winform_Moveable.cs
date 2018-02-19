using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class Winform_Moveable : MonoBehaviour {

	[DllImport("user32.dll")]

	static extern IntPtr GetForegroundWindow ();

	[DllImport("user32.dll")]

	static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

	[DllImport("user32.dll")]

	static extern bool ReleaseCapture();

	const int WM_NCLBUTTONDOWN = 0xA1;
	const int HT_CAPTION = 0x2;

	// Use this for initialization
	void Start () {
		
	}

	public void updatexd()
	{
		ReleaseCapture ();
		SendMessage (GetForegroundWindow (), WM_NCLBUTTONDOWN, HT_CAPTION, 0);
		Debug.Log ("I'm hitting ");
	}
}
