using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remoteplayercontroller: UDPListener{
	public override void UDPMessageReceived(string message){
		Debug.Log ("Listener:" + message);
		switch (message) {
		default:
			break;
		}
	}
}

