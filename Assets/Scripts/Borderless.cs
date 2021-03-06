﻿
/*
 
 * Custom fullscreen and Borderless window script by Martijn Dekker (Pixelstudio)
 
 * For questions pls contact met at martijn.pixelstudio@gmail.com
 
 * version 0.1
 
 *
 
 */



using System;

using System.Collections;

using System.Runtime.InteropServices;

using System.Diagnostics;

using UnityEngine;



public class Borderless : MonoBehaviour

{

	public Rect screenPosition;



	[DllImport("user32.dll")]

	static extern IntPtr SetWindowLong (IntPtr hwnd,int  _nIndex ,int  dwNewLong);

	[DllImport("user32.dll")]

	static extern bool CloseWindow(IntPtr hWnd); // minimalizowanie okna

	[DllImport("user32.dll")]

	static extern bool DestroyWindow(IntPtr hWnd); // zamkniecie okna

	[DllImport("user32.dll")]

	static extern bool SetWindowPos (IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

	[DllImport("user32.dll")]

	static extern IntPtr GetForegroundWindow ();

	// not used rigth now

	//const uint SWP_NOMOVE = 0x2;

	//const uint SWP_NOSIZE = 1;

	//const uint SWP_NOZORDER = 0x4;

	//const uint SWP_HIDEWINDOW = 0x0080;



	const uint SWP_SHOWWINDOW = 0x0040;

	const int GWL_STYLE = -16;

	const int WS_BORDER = 1;





	void Start ()

	{
		int xr = (Screen.currentResolution.width / 2 - (int)screenPosition.width /2);
		int yr = (Screen.currentResolution.height / 2 - (int)screenPosition.height /2);
		SetWindowLong(GetForegroundWindow (), GWL_STYLE, WS_BORDER);

		bool result = SetWindowPos (GetForegroundWindow (), 0, xr, yr, (int)screenPosition.width,(int) screenPosition.height, SWP_SHOWWINDOW);
		// bool cycki = DestroyWindow(GetForegroundWindow ()); zamkniecie okna 

	}

	void Update ()
	{
	}



}
