namespace Buk.Multiplayer.Interfaces
{
  public interface IInputReceiver
  {
    public void TriggerPressed(float time);
    public void TriggerReleased(float time);
  }
}
