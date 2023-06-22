using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class IP : MonoBehaviour
{
    private Text m_Text;

    void Start()
    {
        m_Text = GetComponent<Text>();
    }

    void Update()
    {
        m_Text.text = "Adresse IP : " + GetLocalIPv4();
    }
    public string GetLocalIPv4()
    {
        IPHostEntry host;
        string localIP = "0.0.0.0";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }
}
