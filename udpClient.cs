using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Net.Sockets;
using System.Net;

public class udpClient : MonoBehaviour {
	
	public static UdpClient client ;
	public String responseData ;
	// Use this for initialization
	public UDPListener[] listeners;
	void Start () {
		Debug.Log ("Start");
		SocketConnect ("127.0.0.1", 9090);
		client.BeginReceive(new AsyncCallback(recv), null);
		Application.runInBackground = true;
	}
	 void recv(IAsyncResult res)
	{
		IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
		byte[] received = client.EndReceive(res, ref RemoteIpEndPoint);
		responseData = System.Text.Encoding.ASCII.GetString(received, 0, received.Length);
		Debug.Log("Received: " + responseData);  
		if (listeners != null) {
			for(int i=0;i<listeners.Length;i++){
				listeners[i].UDPMessageReceived (responseData);
			}
		}

		client.BeginReceive(new AsyncCallback(recv), null);
	}
	// Update is called once per frame
	void Update () {
	}

	void OnDisable(){
		Debug.Log ("OnDisable");
		SocketDisconnect ();
	}

	String receiveMessage(){
		try 
		{
			// String to store the response ASCII representation.
			responseData = String.Empty;

			//IPEndPoint object will allow us to read datagrams sent from any source.
			IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

			// Read the first batch of the TcpServer response bytes.
			Byte[] data  = client.Receive(ref RemoteIpEndPoint);
			responseData = System.Text.Encoding.ASCII.GetString(data, 0, data.Length);
			Debug.Log("Received: " + responseData);  
			return responseData;
		} 
		catch (ArgumentNullException e) 
		{
			Debug.Log("ArgumentNullException: "+ e);
		} 
		catch (SocketException e) 
		{
			Debug.Log("SocketException: "+ e);
		}
		return String.Empty;
	}
	public static void sendMessage(String message){
		try 
		{
			// Translate the passed message into ASCII and store it as a Byte array.
			Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);         

			// Send the message to the connected UdpServer. 
			client.Send(data, data.Length);
//			Debug.Log("Sent: "+ message); 
		} 
		catch (ArgumentNullException e) 
		{
			Debug.Log("ArgumentNullException: "+ e);
		} 
		catch (SocketException e) 
		{
			Debug.Log("SocketException: "+ e);
		}
	 }
	void SocketConnect(String server, Int32 port) 
	{
		try 
		{
			// Create a TcpClient.
			// Note, for this client to work you need to have a TcpServer 
			// connected to the same address as specified by the server, port
			// combination.
			client = new UdpClient(server, port);   
			client.Connect(server, port);   
		} 
		catch (ArgumentNullException e) 
		{
			Debug.Log("ArgumentNullException: "+ e);
		} 
		catch (SocketException e) 
		{
			Debug.Log("SocketException: "+ e);
		}
	}
	void SocketDisconnect() 
	{
		try 
		{      
			client.Close();
		} 
		catch (ArgumentNullException e) 
		{
			Debug.Log("ArgumentNullException: "+ e);
		} 
		catch (SocketException e) 
		{
			Debug.Log("SocketException: "+ e);
		}
	}

}
