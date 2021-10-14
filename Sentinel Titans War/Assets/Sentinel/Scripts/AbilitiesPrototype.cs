using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*  Author: Brendan Cranfield
 *  Creation Date: 12/10/2021
 *  Last Edit: 12/10/2021
 *  
 *  Description: This script handles how the abilities will be used in the game with the new Unity input system. 
 *  This script should also be easily adaptable.
 *  
 *  -- This script was made for use in Sentinels -- */


[System.Serializable]
public struct Abilities
{
    //Used for each ability
    public string abilityName;
    public enum AbilityType { Projectile, Calldown }
    public AbilityType abilityType;

    public float damageAmount;
    public float cooldownDuration;

    [Tooltip("This is the object that will be spawned. It should have the effect you want to use as well as any scripts to damage enemies.")]
    public GameObject effectObject;

    [Space(10f)]
    public bool isSelected;
}

public class AbilitiesPrototype : MonoBehaviour
{
    public enum Actions { Ability, Ranged, Normal }     //List of all available actions
    public Actions currentAction;       //Stores current action
    public Abilities[] ability;       //Stores ability types
    Camera mainCamera;      //Stores camera. helps with performance slightly

    public GameObject normalProjectile;

    [HideInInspector]
    public bool aiming, shooting;     //Input flags
    int currentAbility;
    RaycastHit hit;
    
    //  0 = No Ability, 1 = first ability, 2 = second ability, 3rd = third ability
    public int CurrentAbility { get { return currentAbility; } set { if (currentAbility > 3) { currentAbility = 3; } else if (currentAbility < 0) { currentAbility = 0; } else { currentAbility = value; } } }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        //Configures actions based on inputs
        if (aiming)
            currentAction = Actions.Ranged;
        else if (CurrentAbility != 0)
            currentAction = Actions.Ability;
        else
            currentAction = Actions.Normal;

        Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Mathf.Infinity);
        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward);

        switch (currentAction)
        {
            case Actions.Normal:    //Normal Attacks. Eg swinging sword
                
                break;

            case Actions.Ability:   //Ability Attacks. Eg shooting lighting

                //Player is using their ability
                if (shooting && CurrentAbility != 0)
                    HandleAbilities(() => 
                    { 
                        //Add anything in here if you want anything specific to happen once an ability is used
                        Debug.Log("Ability used"); 
                    });
                break;

            case Actions.Ranged:    //Ranged Attacks. Eg shooting projectiles
                if(shooting)
                    HandleRangedAttack();
                break;
        }

        //  Handles cooldown timer for all abilities
        if (ability[0].cooldownDuration > 0)
            ability[0].cooldownDuration -= 1 * Time.deltaTime;
        else
            ability[0].cooldownDuration = 0;

        if (ability[1].cooldownDuration > 0)
            ability[1].cooldownDuration -= 1 * Time.deltaTime;
        else
            ability[1].cooldownDuration = 0;

        if (ability[2].cooldownDuration > 0)
            ability[2].cooldownDuration -= 1 * Time.deltaTime;
        else
            ability[2].cooldownDuration = 0;
    }

    private void LateUpdate()
    {
        shooting = false;   //Resets shooting bool after input turns true
    }

    void HandleRangedAttack()
    {
        GameObject projectile = Instantiate(normalProjectile, transform);   //Instantiates projectile

    }

    void HandleAbilities(Action OnAbilityCompleted)
    {
        if (ability[CurrentAbility - 1].cooldownDuration > 0)
            return;

        switch(ability[CurrentAbility - 1].abilityType)
        {
            case Abilities.AbilityType.Calldown:
                GameObject calldownAbility = Instantiate(ability[CurrentAbility - 1].effectObject);   //Spawn Ability
                calldownAbility.transform.position = hit.point;      //Set ability's position to raycast hit point


                ability[CurrentAbility - 1].cooldownDuration += 5f;
                break;

            case Abilities.AbilityType.Projectile:
                GameObject projectileAbility = Instantiate(ability[CurrentAbility - 1].effectObject);    //Spawn Ability


                ability[CurrentAbility - 1].cooldownDuration += 0.25f;
                break;
        }

        OnAbilityCompleted();   //This is a callback, it sends an action out.
    }
}
