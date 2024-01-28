using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferCharacterController : MonoBehaviour
{
    Rigidbody2D rg;
    PlayerMovement pm;
    Attack attack;

    bool power = false;
    bool immunity = false;
    bool speed = false;
    bool invert = false;

    private void Awake()
    {
        rg = GetComponent<Rigidbody2D>();   
        pm = GetComponent<PlayerMovement>();
        attack = GetComponent<Attack>();
    }



    public void itemManager(ItemsManager item)
    {
        switch (item.ItemName) {
            case "power":
                //Makes you powerful in general
                if (!power)
                {
                    StartCoroutine(createPower(item.powerIncreasaed,item.powerSizeIncreasaed,item.timeOfEffect));
                }
                break;
            case "immunity":
                //You are inmune during some seconds
                if (!immunity)
                {

                }
                break;
            case "happinessLow":
                //Incresse the bar of the princess making it more angry
                //Affects general manager
                break;
            case "happinessHigh":
                //Drecrease the bar of the princess making it more happy
                //Affects general manager
                break;
            case "speed":
                //You are faster
                if (!speed)
                {
                    StartCoroutine(modifySpeed(item.speedModifier, item.timeOfEffect));
                }
                break;
            case "invert":
                //the controllers are inverted
                if (!invert)
                {
                    StartCoroutine(invertControls(item.timeOfEffect));
                }
                break;
            case "slow":
                //You become slower
                if (!speed)
                {
                    StartCoroutine(modifySpeed(-item.speedModifier,item.timeOfEffect));
                }
                break;
        }
    }

    IEnumerator createPower(int value,float valueSize,float duration)
    {
        transform.localScale += new Vector3(valueSize, valueSize, 0);
        attack.increaseAttack(value);
        power = true;

        yield return new WaitForSeconds(duration);

        power = false;
        attack.decreaseAttack();
        transform.localScale -= new Vector3(valueSize, valueSize, 0);


    }

    IEnumerator invertControls(float duration)
    {   

        pm.invertControls(true);
        invert = true;

        yield return new WaitForSeconds(duration);

        invert = false;
        pm.invertControls(false);

    }

    IEnumerator modifySpeed(float speedVelocity,float duration)
    {
        pm.modifySpeed(speedVelocity);
        speed = true;

        yield return new WaitForSeconds(duration);

        speed = false;
        pm.ResetSpeed();

    }
}
