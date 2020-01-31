// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEngine;

namespace ScriptableVariable.Unite2017.Variables
{
  [CreateAssetMenu]
  public class BoolVariable : UnityEngine.ScriptableObject
  {
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
    public bool Value;

    public void SetValue(bool value)
    {
      Value = value;
    }

    public void SetValue(BoolVariable value)
    {
      Value = value.Value;
    }

    public void Invert(bool amount)
    {
      Value = !amount;
    }

    public void Invert(BoolVariable amount)
    {
      Value = !amount.Value;
    }
    public void Invert()
    {
      Value = !Value;
    }
  }
}
