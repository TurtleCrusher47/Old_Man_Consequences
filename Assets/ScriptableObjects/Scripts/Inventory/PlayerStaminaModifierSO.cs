using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStaminaModifierSO : CharacterStatModifierSO
{
    [SerializeField] private PlayerData playerData;

    //[SerializeField] private GameObject particleEffect;

    public override void AffectCharacter(GameObject character, float val)
    {
        playerData.CurrentStamina += val;
        //GameObject effect = Instantiate(particleEffect, character.transform).gameObject;

        //Destroy(effect, 3.0f);

    }
}
