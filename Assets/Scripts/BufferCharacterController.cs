using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferCharacterController : MonoBehaviour
{
    Rigidbody2D rg;
    PlayerMovement pm;
    Attack attack;

    float timeRemainingEffect = 0;
    string actualEffect = "";

    bool power = false;
    bool immunity = false;
    bool slipery = false;
    bool speed = false;
    bool invert = false;
    bool slow = false;

    private void Awake()
    {
        rg = GetComponent<Rigidbody2D>();   
        pm = GetComponent<PlayerMovement>();
        attack = GetComponent<Attack>();
    }



    public void itemManager(ItemsManager item)
    {
        switch (item.ItemName) {
            case "slipery":
                //You become slipery and keep moving even if you stop
                if (!slipery)
                {

                }
                break;
            case "power":
                //Makes you powerful in general
                if (!power)
                {
                    StartCoroutine(createPower(item.powerIncreasaed,item.timeOfEffect));
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

    IEnumerator createPower(int value,float duration)
    {
        transform.localScale += new Vector3(value, value, 0);
        attack.increaseAttack(value);
        power = true;

        yield return new WaitForSeconds(duration);

        power = false;
        attack.decreaseAttack();

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
