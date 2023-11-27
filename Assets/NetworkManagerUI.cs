using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button ServerBTN;
    [SerializeField] private Button HostBTN;
    [SerializeField] private Button ClientBTN;

    private void Awake()
    {
        ServerBTN.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
        });
        
        HostBTN.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });
        
        ClientBTN.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }
}
