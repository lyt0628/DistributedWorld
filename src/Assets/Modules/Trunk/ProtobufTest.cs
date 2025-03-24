using Google.Protobuf;
using QS.Network;
using QS.Trunk;
using System;
using System.Linq;
using System.Net.Sockets;
using UnityEngine;

public class ProtoBufTest : MonoBehaviour
{
    private const string ServerIP = "127.0.0.1";
    private const int ServerPort = 8080;

    private async void Start()
    {

        // 创建 Protobuf 消息
        var message = new Msg
        {
            AttackerId = 42
        };

        // 连接到服务器
        TcpClient client = new(ServerIP, ServerPort);
        Debug.Log("Connected to server.");
        NetworkStream stream = client.GetStream();

        // 编码并发送消息
        byte[] messageBytes = message.ToByteArray();
        byte[] lengthBytes = Varint32Util.EncodeVarint32Header((uint)messageBytes.Length);
        await stream.WriteAsync(lengthBytes, 0, lengthBytes.Length);
        await stream.WriteAsync(messageBytes, 0, messageBytes.Length);
        Debug.Log("Data sent to server.");

        // 接收响应长度
        byte[] respLenBuff = new byte[5];
        int nRespRead = await stream.ReadAsync(respLenBuff, 0, respLenBuff.Length);
        int respLen = Varint32Util.DecodeVarint32Header(respLenBuff, out int respHeaderLen);
        Debug.Log($"Response length: {respLen}");

        // 接收响应内容
        byte[] responseBuffer = new byte[respLen];
        var respRest = respLenBuff
            .Skip(respHeaderLen)
            .ToArray();

        int totalBytesRead = nRespRead - respHeaderLen;
        Array.Copy(respRest, responseBuffer, totalBytesRead);
        while (totalBytesRead < respLen)
        {
            int bytesToRead = respLen - totalBytesRead;
            int bytesReceived = await stream.ReadAsync(responseBuffer, totalBytesRead, bytesToRead);
            if (bytesReceived == 0)
            {
                throw new Exception("Server closed the connection prematurely.");
            }
            totalBytesRead += bytesReceived;
            Debug.Log($"Bytes received: {bytesReceived}, Total bytes read: {totalBytesRead}");
        }
        Debug.Log("Response received from server.");

        // 解码响应
        var response = Msg.Parser.ParseFrom(responseBuffer);
        Debug.Log($"Response content: {response.AttackerId}");

        // 关闭连接
        stream.Close();
        client.Close();


    }
}