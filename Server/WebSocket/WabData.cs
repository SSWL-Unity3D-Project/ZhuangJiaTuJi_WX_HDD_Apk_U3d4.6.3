using System;
using UnityEngine;
using System.Collections.Generic;
using BestHTTP;
using BestHTTP.WebSocket;
using LitJson;

public class WabData
{
    /// <summary>  
    /// The WebSocket address to connect  
    /// </summary>  
    private string address = "ws://192.168.1.16:1818";

    /// <summary>  
    /// Default text to send  
    /// </summary>  
    private string _msgToSend = "Hello World!";

    /// <summary>  
    /// Debug text to draw on the gui  
    /// </summary>  
    private string _text = string.Empty;

    /// <summary>  
    /// Saved WebSocket instance  
    /// </summary>  
    private WebSocket _webSocket;

    /// <summary>
    /// WebSocket通讯控制组件.
    /// </summary>
    public WebSocketSimpet m_WebSocketSimpet;
    public WebSocket WebSocket { get { return _webSocket; } }
    public string Address
    {
        set { address = value; }
        get { return address; }
    }
    public string Text { get { return _text; } }

    public string MsgToSend
    {
        get { return _msgToSend; }
        set
        {
            _msgToSend = value;
            SendMsg(value);
        }
    }

    public void OpenWebSocket()
    {
        if (_webSocket == null)
        {
            // Create the WebSocket instance  
            _webSocket = new WebSocket(new Uri(address));

            if (HTTPManager.Proxy != null)
                _webSocket.InternalRequest.Proxy = new HTTPProxy(HTTPManager.Proxy.Address, HTTPManager.Proxy.Credentials, false);

            // Subscribe to the WS events  
            _webSocket.OnOpen += OnOpen;
            _webSocket.OnMessage += OnMessageReceived;
            _webSocket.OnClosed += OnClosed;
            _webSocket.OnError += OnError;

            // Start connecting to the server  
            _webSocket.Open();
        }
    }

    public void SendMsg(string msg)
    {
        // Send message to the server  
        _webSocket.Send(msg);
    }

    public void CloseSocket()
    {
        // Close the connection  
        _webSocket.Close(1000, "Bye!");
    }

    /// <summary>  
    /// Called when the web socket is open, and we are ready to send and receive data  
    /// </summary>  
    void OnOpen(WebSocket ws)
    {
        Debug.Log("Unity:"+"-WebSocket Open!\n");
    }

    /// <summary>
    /// Called when we received a text message from the server.
    /// </summary>
    void OnMessageReceived(WebSocket ws, string message)
    {
        //Debug.Log("Unity:"+"OnMessageReceived -> message == " + message);
        if (m_WebSocketSimpet != null)
        {
            m_WebSocketSimpet.OnMessageReceived(message);
        }
    }

    /// <summary>  
    /// Called when the web socket closed  
    /// </summary>  
    void OnClosed(WebSocket ws, UInt16 code, string message)
    {
        Debug.Log("Unity:"+string.Format("-WebSocket closed! Code: {0} Message: {1}\n", code, message));
        _webSocket = null;
    }

    /// <summary>  
    /// Called when an error occured on client side  
    /// </summary>  
    void OnError(WebSocket ws, Exception ex)
    {
        string errorMsg = string.Empty;
        if (ws.InternalRequest.Response != null)
            errorMsg = string.Format("Status Code from Server: {0} and Message: {1}", ws.InternalRequest.Response.StatusCode, ws.InternalRequest.Response.Message);

        Debug.Log("Unity:"+string.Format("-An error occured: {0}\n", ex != null ? ex.Message : "Unknown Error " + errorMsg));
        _webSocket = null;
    }
}