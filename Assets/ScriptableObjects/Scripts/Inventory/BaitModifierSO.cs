using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BaitModifierSO : CharacterStatModifierSO
{
   //create a diff bait modifier for each type of bait

   //add corresponding baitModSO to bait item mod list
   //set value in baitSO mod list to this baits index in the list of baits

   public override void AffectCharacter(GameObject player, float val)
   {
      if (player.GetComponent<FishingController>())
      {
         int indexInList = (int)Math.Round(val);
         player.GetComponent<FishingController>().selectedBait =  player.GetComponent<FishingController>().BaitItems[indexInList];
         Debug.Log(player.GetComponent<FishingController>().selectedBait.Name);
      }

      else
      {
         return;
      }
      
   }
}
