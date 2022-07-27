using Buk.Multiplayer.Interfaces;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

namespace Buk.Multiplayer
{
  public class NetworkInputReceiverController : NetworkBehaviour
  {
    protected IInputReceiver inputReceiver;

    // The button to use as input for the receiver.
    public InputAction inputButton;
    // Tag to discern receiver.
    public string receiverTag;

    public void Start()
    {
      UpdateInputReceiver();
      // Input handling is only done for the local player.
      if (isLocalPlayer && inputButton != null)
      {
        inputButton.started += TriggerPressed;
        inputButton.canceled += TriggerReleased;
        inputButton.Enable();
      }
    }

    public void OnDestroy()
    {
      if (inputButton != null)
      {
        inputButton.started -= TriggerPressed;
        inputButton.canceled -= TriggerReleased;
      }
    }

    public void UpdateInputReceiver()
    {
      var inputReceivers = GetComponentsInChildren<IInputReceiver>();
      // No tag set, just return first component found.
      if (receiverTag == null)
      {
        inputReceiver = inputReceivers.First();
        return;
      }
      // Find first component that has a parent with matching tag.
      foreach (var inputReceiver in inputReceivers) {
        for (var obj = (inputReceiver as Component).transform; obj != null; obj = obj.parent)
        {
          if (obj.tag == receiverTag)
          {
            this.inputReceiver = inputReceiver;
            return;
          }
        }
      }
    }

    public void TriggerPressed(InputAction.CallbackContext context)
    {
      CmdInputPressed(Time.time);
    }

    public void TriggerReleased(InputAction.CallbackContext context)
    {
      CmdInputReleased(Time.time);
    }

    // This is what the server communication looks like for this implementation
    // This communication is done with Remote Actions, read more here
    // https://mirror-networking.gitbook.io/docs/guides/communications/remote-actions
    /*
                                              ┌───────────────────────┐
                                              │Server                 │
                                              │ ┌────────┐ ┌────────┐ │
                          ┌───────────────────┼─┤Server  | |Server  | │
                          │  ┌────────────────┼─►Player 1| |Player 2| │
                          │  │                │ └───────┬┘ └────────┘ │
                          │  │                └─────────┼─────────────┘
                          │  │                          │
          RpcInputPressed │  |CmdInputPressed           └──────────┐RpcInputPressed
              (time=1235) │  │    (time=1235)                      │    (time=1235)
                   ┌──────┼──┼──────────────────────────┐   ┌──────┼────────────────┐
                   │Client│1 │                          │   │Client|2               │
                   │ ┌────▼──┴┐              ┌────────┐ │   │ ┌────▼───┐ ┌────────┐ │
                   │ │Client  │              │Remote  │ │   │ │Remote  │ |Client  │ │
                   │ │Player 1│              │Player 2│ │   │ │Player 1│ |Player 2│ │
                   │ └────────┘              └────────┘ │   │ └────────┘ └────────┘ │
                   └────────────────────────────────────┘   └───────────────────────┘
    */

    [Command]
    public void CmdInputPressed(float time)
    {
      if (!isServer) return;
      RpcInputPressed(time);
    }

    [Command]
    public void CmdInputReleased(float time)
    {
      if (!isServer) return;
      RpcInputReleased(time);

    }

    [ClientRpc]
    public void RpcInputPressed(float time)
    {
      inputReceiver.TriggerPressed(time);
    }

    [ClientRpc]
    public void RpcInputReleased(float time)
    {
      inputReceiver.TriggerReleased(time);
    }
  }
}
