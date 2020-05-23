using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthCard : MonoBehaviour
{
    public int normal_hpcardnum, normal_damagecardnum, normal_defcardnum, normal_dodgecardnum, normal_strcardnum, normal_luckcardnum, normal_conditioncardnum, normal_constitutioncardnum, normal_powercardnum, normal_skilldamagecardnum;
    public int rare_hpcardnum, rare_damagecardnum, rare_defcardnum, rare_dodgecardnum, rare_strcardnum, rare_luckcardnum, rare_conditioncardnum, rare_constitutioncardnum, rare_powercardnum, rare_skilldamagecardnum;
    public int epic_hpcardnum, epic_damagecardnum, epic_defcardnum, epic_dodgecardnum, epic_strcardnum, epic_luckcardnum, epic_conditioncardnum, epic_constitutioncardnum, epic_powercardnum, epic_skilldamagecardnum;
    public int whatcard;

    void Start()
    {
        normal_hpcardnum = 0; normal_damagecardnum = 0; normal_defcardnum = 0; normal_dodgecardnum = 0; normal_strcardnum = 0;
        normal_luckcardnum = 0; normal_conditioncardnum = 0; normal_constitutioncardnum = 0; normal_powercardnum = 0; normal_skilldamagecardnum = 0;
        rare_hpcardnum = 0; rare_damagecardnum = 0; rare_defcardnum = 0; rare_dodgecardnum = 0; rare_strcardnum = 0;
        rare_luckcardnum = 0; rare_conditioncardnum = 0; rare_constitutioncardnum = 0; rare_powercardnum = 0; rare_skilldamagecardnum = 0;
        epic_hpcardnum = 0; epic_damagecardnum = 0; epic_defcardnum = 0; epic_dodgecardnum = 0; epic_strcardnum = 0;
        epic_luckcardnum = 0; epic_conditioncardnum = 0; epic_constitutioncardnum = 0; epic_powercardnum = 0; epic_skilldamagecardnum = 0;
        whatcard = -1;
    }

    public void PickCard(int card)
    {
        if (card == 0) PickHpCard();
        if (card == 1) PickDamageCard();
        if (card == 2) PickDefCard();
        if (card == 3) PickDodgeCard();
        if (card == 4) PickStrCard();
        if (card == 5) PickLuckCard();
        if (card == 6) PickConditionCard();
        if (card == 7) PickConstitutionCard();
        if (card == 8) PickPowerCard();
        if (card == 9) PickSkillDamageCard();
    }

    public void PickHpCard()
    {
        if (whatcard == 0)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plushp += 5;
            normal_hpcardnum++;
        }
        else if (whatcard == 1)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plushp += 10;
            rare_hpcardnum++;
        }
        else if (whatcard == 2)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plushp += 15;
            epic_hpcardnum++;
        }
    }
    public void PickDamageCard()
    {
        if (whatcard == 0)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plusdamage += 5;
            normal_damagecardnum++;
        }
        else if (whatcard == 1)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plusdamage += 10;
            rare_damagecardnum++;
        }
        else if (whatcard == 2)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plusdamage += 15;
            epic_damagecardnum++;
        }
    }
    public void PickDefCard()
    {
        if (whatcard == 0)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plusdef += 3;
            normal_defcardnum++;
        }
        else if (whatcard == 1)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plusdef += 5;
            rare_defcardnum++;
        }
        else if (whatcard == 2)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plusdef += 7;
            epic_defcardnum++;
        }
    }
    public void PickDodgeCard()
    {
        if (whatcard == 0)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plusdodge += 0.5f;
            normal_dodgecardnum++;
        }
        else if (whatcard == 1)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plusdodge += 1;
            rare_dodgecardnum++;
        }
        else if (whatcard == 2)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plusdodge += 1.5f;
            epic_dodgecardnum++;
        }
    }
    public void PickStrCard()
    {
        if (whatcard == 0)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plusstr += 3;
            normal_strcardnum++;
        }
        else if (whatcard == 1)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plusstr += 5;
            rare_strcardnum++;
        }
        else if (whatcard == 2)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plusstr += 7;
            epic_strcardnum++;
        }
    }
    public void PickLuckCard()
    {
        if (whatcard == 0)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plusluck += 3;
            normal_luckcardnum++;
        }
        else if (whatcard == 1)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plusluck += 5;
            rare_luckcardnum++;
        }
        else if (whatcard == 2)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plusluck += 7;
            epic_luckcardnum++;
        }
    }
    public void PickConditionCard()
    {
        if (whatcard == 0)
        {
            GameObject.Find("player").GetComponent<Playerstat>().pluscondition += 5;
            normal_conditioncardnum++;
        }
        else if (whatcard == 1)
        {
            GameObject.Find("player").GetComponent<Playerstat>().pluscondition += 10;
            rare_conditioncardnum++;
        }
        else if (whatcard == 2)
        {
            GameObject.Find("player").GetComponent<Playerstat>().pluscondition += 15;
            epic_conditioncardnum++;
        }
    }
    public void PickConstitutionCard()
    {
        if (whatcard == 0)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plusconstitution += 5;
            normal_constitutioncardnum++;
        }
        else if (whatcard == 1)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plusconstitution += 10;
            rare_constitutioncardnum++;
        }
        else if (whatcard == 2)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plusconstitution += 15;
            epic_constitutioncardnum++;
        }
    }
    public void PickPowerCard()
    {
        if (whatcard == 0)
        {
            GameObject.Find("player").GetComponent<Playerstat>().pluspower += 5;
            normal_powercardnum++;
        }
        else if (whatcard == 1)
        {
            GameObject.Find("player").GetComponent<Playerstat>().pluspower += 10;
            rare_powercardnum++;
        }
        else if (whatcard == 2)
        {
            GameObject.Find("player").GetComponent<Playerstat>().pluspower += 15;
            epic_powercardnum++;
        }
    }
    public void PickSkillDamageCard()
    {
        if (whatcard == 0)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plusskilldamage += 5;
            normal_skilldamagecardnum++;
        }
        else if (whatcard == 1)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plusskilldamage += 10;
            rare_skilldamagecardnum++;
        }
        else if (whatcard == 2)
        {
            GameObject.Find("player").GetComponent<Playerstat>().plusskilldamage += 15;
            epic_skilldamagecardnum++;
        }
    }
}
