// From: https://forum.unity.com/threads/draw-a-field-only-if-a-condition-is-met.448855/

using UnityEngine;
using System;

namespace CustomAttributes
{
  public enum ComparisonType
  {
    Equals = 1,
    NotEqual = 2,
    GreaterThan = 3,
    SmallerThan = 4,
    SmallerOrEqual = 5,
    GreaterOrEqual = 6
  }

  public enum DisablingType
  {
    ReadOnly = 2,
    DontDraw = 3
  }

  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
  public class DrawIfAttribute : PropertyAttribute
  {
    public string comparedPropertyName { get; private set; }
    public object comparedValue { get; private set; }
    public ComparisonType comparisonType { get; private set; }
    public DisablingType disablingType { get; private set; }

    public DrawIfAttribute(string comparedPropertyName, object comparedValue, ComparisonType comparisonType, DisablingType disablingType = DisablingType.DontDraw)
    {
      this.comparedPropertyName = comparedPropertyName;
      this.comparedValue = comparedValue;
      this.comparisonType = comparisonType;
      this.disablingType = disablingType;
    }
  }

}
