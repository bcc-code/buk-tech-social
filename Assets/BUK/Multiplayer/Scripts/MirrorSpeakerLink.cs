using System;
using System.Linq;
using Mirror;
using Photon.Voice.Unity;
using UnityEngine;

namespace Buk.Multiplayer
{
  /// <summary>
  /// Make sure the player's Speaker is correctly identified
  /// </summary>
  public class MirrorSpeakerLink : NetworkBehaviour
  {
    public void LateLink()
    {
      CmdLateLink();
    }

    [Command]
    public void CmdLateLink()
    {
      if (!isServer) return;
      RpcLateLink();
    }

    [ClientRpc]
    public void RpcLateLink()
    {
      var speaker = GetComponentInChildren<Speaker>();
      if (speaker == null)
      {
        throw new ApplicationException("Speaker not found! Add a speaker to this object or its children.");
      }
      var voiceConnection = FindObjectsOfType<VoiceConnection>().First();
      if (voiceConnection == null)
      {
        throw new ApplicationException("VoiceConnection not found! Add a VoiceConnection to the scene.");
      }
      voiceConnection.TryLateLinkingUsingUserData(speaker, netId);
    }
  }
}
