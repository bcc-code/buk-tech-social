using System;
using Buk.Multiplayer.Interfaces;
using Buk.PhysicsLogic.Interfaces;
using Mirror;
using UnityEngine;

namespace Buk.PhysicsLogic.Implementation.Multiplayer
{
  /** If you use this class, don't forget to put a NetworkInputReceiverController on the top level network component */
  public class MultiplayerPhysicsGun : MonoBehaviour, IGun, IInputReceiver
  {
    // The type of bullet this gun shoots.
    public GameObject bulletType;
    // How fast the bullet is launched
    public float maxMuzzleSpeed = 10.0f;
    // Seconds before you can shoot a new bullet.
    public float coolDown = .25f;
    // At what time was the last bullet shot.
    protected float lastShotTime = 0.0f;

    private Rigidbody shooterBody;
    public bool CanShoot { get => Time.fixedTime - lastShotTime >= coolDown; }

    public virtual void TriggerPressed(float time)
    {
      if (CanShoot)
      {
        Shoot(maxMuzzleSpeed);
      }
    }

    public virtual void TriggerReleased(float time)
    {
      // Do nothing
    }

    public void Awake()
    {

      if (bulletType == null)
      {
        throw new Exception("You must add a bullet type!");
      }

      if (bulletType.GetComponentInChildren<Rigidbody>() == null)
      {
        throw new Exception("Your bullet must have a Rigidbody to use it with this gun!");
      }
      shooterBody = GetComponentInParent<Rigidbody>();
    }

    public void Shoot(float speed)
    {
      lastShotTime = Time.fixedTime;
      // Create a new copy of bulletType using the gun's position and rotation.
      var bulletBody = Instantiate(bulletType, transform.position, transform.rotation)
        // Get the Rigidbody of that bullet, so that we can apply physics to it.
        .GetComponentInChildren<Rigidbody>();
      // If possible
      if (shooterBody)
      {
        // Make the bullet start moving just as fast as the shooter.
        // This makes the behaviour more realistic
        bulletBody.velocity = shooterBody.velocity;
      }
      // Apply speed to the bullet's body, relative to its current position and rotation
      bulletBody.AddRelativeForce(0f, speed, 0f, ForceMode.VelocityChange);
    }
  }
}
