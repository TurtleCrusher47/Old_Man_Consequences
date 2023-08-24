using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVectorPosition : MonoBehaviour
{
   public VectorValue vectorValue;
   public Vector2 newVectorValue;

   void Awake()
   {
        vectorValue.initialValue = newVectorValue;
   }
}
