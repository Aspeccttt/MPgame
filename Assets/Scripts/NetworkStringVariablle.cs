using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

//Network variable base gives me all the logic of a network variable but i can talor it to my needs that can be shared of the network.
public class NetworkStringVariablle : NetworkVariableBase
{
    private string _value;
    public string Value
    {
        get => _value; 
        set
        {
            if (_value != value) 
            { 
                _value = value;
                SetDirty(true); //Marking a ariable as dirty is a way to indicate that the variable's value has changed
                // since it was last synced over the network therefore this signals the network to include this
                // variaible during the next network update; 
            }
        }
    }

    public NetworkStringVariablle
        (
        string value = default, 
        NetworkVariableReadPermission readPerm = NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission writePerm = NetworkVariableWritePermission.Server) : 
        base(readPerm, writePerm
        )
        {
            _value = value;
        }

    public override void ReadDelta(FastBufferReader reader, bool keepDirtyDelta)
    {
        reader.ReadValueSafe(out _value);
    }

    public override void ReadField(FastBufferReader reader)
    {
        reader.ReadValueSafe(out _value);
    }

    public override void WriteDelta(FastBufferWriter writer)
    {
        writer.WriteValueSafe(_value);
    }

    public override void WriteField(FastBufferWriter writer)
    {
        writer.WriteValueSafe(_value);

    }
}
