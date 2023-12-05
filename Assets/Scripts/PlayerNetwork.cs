using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private NetworkVariable<int> _randomNumber = new NetworkVariable<int>(0,
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    private NetworkStringVariablle _randomStr = new ("Testing MEGUS", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    //similar to awake
    public override void OnNetworkSpawn()
    {
        _randomNumber.OnValueChanged += OnMyIntChanged;
    }

    public override void OnNetworkDespawn()
    {
        _randomNumber.OnValueChanged -= OnMyIntChanged;
    }

    private void OnMyIntChanged(int oldValue, int newValue)
    {
        Debug.Log($" Owner: {OwnerClientId} My old INT: " +oldValue + 
            ", has been changed to: " +newValue);
    }

    private NetworkVariable<myConstructor> _customData = new NetworkVariable<myConstructor>(new myConstructor
    {
        _randomInt = 27,
        _randomBool = false,
        _randomStr = "HOHOHOHOHO TESTING CHRISTMAS"
    });

    public struct myConstructor : INetworkSerializable
    {
        public int _randomInt;
        public string _randomStr;
        public bool _randomBool;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _randomInt);
            serializer.SerializeValue(ref _randomBool);
            serializer.SerializeValue(ref _randomStr);
        }
    }

    private void Update()
    {
        movement();
    }

    void movement()
    {
        if (!IsOwner) return;

        Vector3 movementDirection = new Vector3(0, 0, 0);
        float speed = 3f;

        if (Input.GetKey(KeyCode.W)) movementDirection.z++;
        if (Input.GetKey(KeyCode.S)) movementDirection.z--;
        if (Input.GetKey(KeyCode.D)) movementDirection.x++;
        if (Input.GetKey(KeyCode.A)) movementDirection.x--;

        transform.position += movementDirection * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R))
        {
            _randomNumber.Value = Random.Range(0, 100);
            _randomStr.Value = "Merry Chrimstas to you too!!!!";
        }
    }

    //Example of a server RPC that will only execute on the server.
    [ServerRpc]
    private void TestServerRpc()
    {

    }

    //Example of a client RPC that will only execute on the server.
    [ClientRpc]
    private void TestCientRPC()
    {

    }

}
