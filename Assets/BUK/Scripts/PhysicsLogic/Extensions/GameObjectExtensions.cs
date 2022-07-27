using UnityEngine;

public static class GameObjectExtensions
{
  public static T FindTypeInChildrenWithTag<T>(this GameObject parent, string tag) where T : class
  {
    foreach (var typeMatch in parent.GetComponentsInChildren<T>())
    {
      if (tag == null || typeMatch is Component comp && comp.tag == tag)
      {
        return typeMatch;
      }
    }
    return null;
  }
}
