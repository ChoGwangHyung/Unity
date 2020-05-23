using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataManager : MonoBehaviour
{
    [Serializable]
    public class PlayerData
    {
        public float maxhp;
        public float hp;
        public float plushp;
        public float damage;
        public float plusdamage;
        public float golddamage;
        public float def;
        public float plusdef;
        public float dodge;
        public float plusdodge;
        public float str;
        public float plusstr;
        public float luck;
        public float plusluck;
        public float condition;
        public float pluscondition;
        public float constitution;
        public float plusconstitution;
        public float power;
        public float pluspower;
        public float skilldamage;
        public float plusskilldamage;
        public int exp;
        public int charLv;
        public int needExp;
        public int gold;        

        public int monstercounter;

        public bool golddamageExecution;

        public float characteristic;
        public float skillcharacteristic;

        public int characteristicCounter;
        public int monstercountermax;
        public int charateristicHit;
        public int charateristicObtain;
    }

    [Serializable]
    public class UiData
    {
        public int player_Lv;

        //성장카드
        public int normal_hpcardnum, normal_damagecardnum, normal_defcardnum, normal_dodgecardnum, normal_strcardnum, normal_luckcardnum, normal_conditioncardnum, normal_constitutioncardnum, normal_powercardnum, normal_skilldamagecardnum;
        public int rare_hpcardnum, rare_damagecardnum, rare_defcardnum, rare_dodgecardnum, rare_strcardnum, rare_luckcardnum, rare_conditioncardnum, rare_constitutioncardnum, rare_powercardnum, rare_skilldamagecardnum;
        public int epic_hpcardnum, epic_damagecardnum, epic_defcardnum, epic_dodgecardnum, epic_strcardnum, epic_luckcardnum, epic_conditioncardnum, epic_constitutioncardnum, epic_powercardnum, epic_skilldamagecardnum;
        public int probability1;
        public int probability2;
        public int seq;

        //덱
        public int[,] decCardProperty = new int[10, 2];
        public int[,] addCardProperty = new int[5, 2];

        //특성       
        public int[,] LvChSelect = new int[6, 2];
        public int max_index;

        //npc
        //public int[,,] NPCCardProperty = new int[4, 2, 2];
        //public int[] NPC_Buy = new int[12];
    }

    public void SaveData()
    {
        BinaryFormatter pbf = new BinaryFormatter();
        BinaryFormatter ubf = new BinaryFormatter();
        FileStream pfile = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        FileStream ufile = File.Create(Application.persistentDataPath + "/uiInfo.dat");

        #region 플레이어
        PlayerData Pdata = new PlayerData();

        Pdata.maxhp = GameObject.Find("player").GetComponent<Playerstat>().maxhp;
        Pdata.hp = GameObject.Find("player").GetComponent<Playerstat>().hp;
        Pdata.plushp = GameObject.Find("player").GetComponent<Playerstat>().plushp;
        Pdata.damage = GameObject.Find("player").GetComponent<Playerstat>().damage;
        Pdata.plusdamage = GameObject.Find("player").GetComponent<Playerstat>().plusdamage;
        Pdata.def = GameObject.Find("player").GetComponent<Playerstat>().def;
        Pdata.plusdef = GameObject.Find("player").GetComponent<Playerstat>().plusdef;
        Pdata.dodge = GameObject.Find("player").GetComponent<Playerstat>().dodge;
        Pdata.plusdodge = GameObject.Find("player").GetComponent<Playerstat>().plusdodge;
        Pdata.str = GameObject.Find("player").GetComponent<Playerstat>().str;
        Pdata.plusstr = GameObject.Find("player").GetComponent<Playerstat>().plusstr;
        Pdata.luck = GameObject.Find("player").GetComponent<Playerstat>().luck;
        Pdata.plusluck = GameObject.Find("player").GetComponent<Playerstat>().plusluck;
        Pdata.condition = GameObject.Find("player").GetComponent<Playerstat>().condition;
        Pdata.pluscondition = GameObject.Find("player").GetComponent<Playerstat>().pluscondition;
        Pdata.constitution = GameObject.Find("player").GetComponent<Playerstat>().constitution;
        Pdata.plusconstitution = GameObject.Find("player").GetComponent<Playerstat>().plusconstitution;
        Pdata.power = GameObject.Find("player").GetComponent<Playerstat>().power;
        Pdata.pluspower = GameObject.Find("player").GetComponent<Playerstat>().pluspower;
        Pdata.skilldamage = GameObject.Find("player").GetComponent<Playerstat>().skilldamage;
        Pdata.plusskilldamage = GameObject.Find("player").GetComponent<Playerstat>().plusskilldamage;
        Pdata.exp = GameObject.Find("player").GetComponent<Playerstat>().exp;
        Pdata.charLv = GameObject.Find("player").GetComponent<Playerstat>().charLv;
        Pdata.needExp = GameObject.Find("player").GetComponent<Playerstat>().needExp;
        Pdata.gold = GameObject.Find("player").GetComponent<Playerstat>().gold;
        Pdata.monstercounter = GameObject.Find("player").GetComponent<Playerstat>().monstercounter;
        Pdata.golddamageExecution = GameObject.Find("player").GetComponent<Playerstat>().golddamageExecution;
        Pdata.characteristic = GameObject.Find("player").GetComponent<Playerstat>().characteristic;
        Pdata.skillcharacteristic = GameObject.Find("player").GetComponent<Playerstat>().skillcharacteristic;
        Pdata.characteristicCounter = GameObject.Find("player").GetComponent<Playerstat>().characteristicCounter;
        Pdata.monstercountermax = GameObject.Find("player").GetComponent<Playerstat>().monstercountermax;
        Pdata.charateristicHit = GameObject.Find("player").GetComponent<Playerstat>().charateristicHit;
        Pdata.charateristicObtain = GameObject.Find("player").GetComponent<Playerstat>().charateristicObtain;
        #endregion

        pbf.Serialize(pfile, Pdata);
        pfile.Close();

        #region UI
        UiData Udata = new UiData();

        Udata.player_Lv = GameObject.Find("UIController").GetComponent<UIController>().player_Lv;

        Udata.normal_hpcardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().normal_hpcardnum;
        Udata.normal_damagecardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().normal_damagecardnum;
        Udata.normal_defcardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().normal_defcardnum;
        Udata.normal_dodgecardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().normal_dodgecardnum;
        Udata.normal_strcardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().normal_strcardnum;
        Udata.normal_luckcardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().normal_luckcardnum;
        Udata.normal_conditioncardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().normal_conditioncardnum;
        Udata.normal_constitutioncardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().normal_constitutioncardnum;
        Udata.normal_powercardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().normal_powercardnum;

        Udata.rare_skilldamagecardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().rare_skilldamagecardnum;
        Udata.rare_hpcardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().rare_hpcardnum;
        Udata.rare_damagecardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().rare_damagecardnum;
        Udata.rare_defcardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().rare_defcardnum;
        Udata.rare_dodgecardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().rare_dodgecardnum;
        Udata.rare_strcardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().rare_strcardnum;
        Udata.rare_luckcardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().rare_luckcardnum;
        Udata.rare_conditioncardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().rare_conditioncardnum;
        Udata.rare_constitutioncardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().rare_constitutioncardnum;
        Udata.rare_powercardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().rare_powercardnum;
        Udata.rare_skilldamagecardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().rare_skilldamagecardnum;

        Udata.epic_skilldamagecardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().epic_skilldamagecardnum;
        Udata.epic_hpcardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().epic_hpcardnum;
        Udata.epic_damagecardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().epic_damagecardnum;
        Udata.epic_defcardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().epic_defcardnum;
        Udata.epic_dodgecardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().epic_dodgecardnum;
        Udata.epic_strcardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().epic_strcardnum;
        Udata.epic_luckcardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().epic_luckcardnum;
        Udata.epic_conditioncardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().epic_conditioncardnum;
        Udata.epic_constitutioncardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().epic_constitutioncardnum;
        Udata.epic_powercardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().epic_powercardnum;
        Udata.epic_skilldamagecardnum = GameObject.Find("UIController").GetComponent<GrowthCard>().epic_skilldamagecardnum;

        Udata.probability1 = GameObject.Find("UIController").GetComponent<UIController>().probability1;
        Udata.probability2 = GameObject.Find("UIController").GetComponent<UIController>().probability2;
        Udata.seq = GameObject.Find("UIController").GetComponent<UIController>().seq;

        Udata.decCardProperty = GameObject.Find("UIController").GetComponent<UIController>().decCardProperty;
        Udata.addCardProperty = GameObject.Find("UIController").GetComponent<UIController>().addCardProperty;
        //Array.Copy(GameObject.Find("UIController").GetComponent<UIController>().decCardProperty, Udata.decCardProperty, 20);
        //Array.Copy(GameObject.Find("UIController").GetComponent<UIController>().addCardProperty, Udata.addCardProperty, 10);

        Udata.LvChSelect = GameObject.Find("UIController").GetComponent<UIController>().LvChSelect;
        //Array.Copy(GameObject.Find("UIController").GetComponent<UIController>().LvChSelect, Udata.LvChSelect, 12);
        Udata.max_index = GameObject.Find("UIController").GetComponent<UIController>().max_index;

        //Udata.NPCCardProperty = GameObject.Find("UIController").GetComponent<UIController>().NPCCardProperty;
        //// Array.Copy(GameObject.Find("UIController").GetComponent<UIController>().NPCCardProperty, Udata.NPCCardProperty, 16);
        //Udata.NPC_Buy = GameObject.Find("UIController").GetComponent<UIController>().NPC_Buy;
        #endregion

        ubf.Serialize(ufile, Udata);        
        ufile.Close();
    }

    public void PLoadData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

        if (file != null && file.Length > 0)
        {
            #region 플레이어
            PlayerData Pdata = (PlayerData)bf.Deserialize(file);

            GameObject.Find("player").GetComponent<Playerstat>().maxhp = Pdata.maxhp;
            GameObject.Find("player").GetComponent<Playerstat>().hp = Pdata.hp;
            GameObject.Find("player").GetComponent<Playerstat>().plushp = Pdata.plushp;
            GameObject.Find("player").GetComponent<Playerstat>().damage = Pdata.damage;
            GameObject.Find("player").GetComponent<Playerstat>().plusdamage = Pdata.plusdamage;
            GameObject.Find("player").GetComponent<Playerstat>().def = Pdata.def;
            GameObject.Find("player").GetComponent<Playerstat>().plusdef = Pdata.plusdef;
            GameObject.Find("player").GetComponent<Playerstat>().dodge = Pdata.dodge;
            GameObject.Find("player").GetComponent<Playerstat>().plusdodge = Pdata.plusdodge;
            GameObject.Find("player").GetComponent<Playerstat>().str = Pdata.str;
            GameObject.Find("player").GetComponent<Playerstat>().plusstr = Pdata.plusstr;
            GameObject.Find("player").GetComponent<Playerstat>().luck = Pdata.luck;
            GameObject.Find("player").GetComponent<Playerstat>().plusluck = Pdata.plusluck;
            GameObject.Find("player").GetComponent<Playerstat>().condition = Pdata.condition;
            GameObject.Find("player").GetComponent<Playerstat>().pluscondition = Pdata.pluscondition;
            GameObject.Find("player").GetComponent<Playerstat>().constitution = Pdata.constitution;
            GameObject.Find("player").GetComponent<Playerstat>().plusconstitution = Pdata.plusconstitution;
            GameObject.Find("player").GetComponent<Playerstat>().power = Pdata.power;
            GameObject.Find("player").GetComponent<Playerstat>().pluspower = Pdata.pluspower;
            GameObject.Find("player").GetComponent<Playerstat>().skilldamage = Pdata.skilldamage;
            GameObject.Find("player").GetComponent<Playerstat>().plusskilldamage = Pdata.plusskilldamage;
            GameObject.Find("player").GetComponent<Playerstat>().exp = Pdata.exp;
            GameObject.Find("player").GetComponent<Playerstat>().charLv = Pdata.charLv;
            GameObject.Find("player").GetComponent<Playerstat>().needExp = Pdata.needExp;
            GameObject.Find("player").GetComponent<Playerstat>().gold = Pdata.gold;
            GameObject.Find("player").GetComponent<Playerstat>().monstercounter = Pdata.monstercounter;
            GameObject.Find("player").GetComponent<Playerstat>().golddamageExecution = Pdata.golddamageExecution;
            GameObject.Find("player").GetComponent<Playerstat>().characteristic = Pdata.characteristic;
            GameObject.Find("player").GetComponent<Playerstat>().skillcharacteristic = Pdata.skillcharacteristic;
            GameObject.Find("player").GetComponent<Playerstat>().characteristicCounter = Pdata.characteristicCounter;
            GameObject.Find("player").GetComponent<Playerstat>().monstercountermax = Pdata.monstercountermax;
            GameObject.Find("player").GetComponent<Playerstat>().charateristicHit = Pdata.charateristicHit;
            GameObject.Find("player").GetComponent<Playerstat>().charateristicObtain = Pdata.charateristicObtain;
            #endregion            
        }
        file.Close();
    }

    public void ULoadData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/uiInfo.dat", FileMode.Open);

        if (file != null && file.Length > 0)
        {           
            #region UI
            UiData Udata = (UiData)bf.Deserialize(file);

            GameObject.Find("UIController").GetComponent<UIController>().player_Lv = Udata.player_Lv - 1;
            GameObject.Find("UIController").GetComponent<UIController>().seq = Udata.seq;

            GameObject.Find("UIController").GetComponent<GrowthCard>().normal_hpcardnum = Udata.normal_hpcardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().normal_damagecardnum = Udata.normal_damagecardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().normal_defcardnum = Udata.normal_defcardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().normal_dodgecardnum = Udata.normal_dodgecardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().normal_strcardnum = Udata.normal_strcardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().normal_luckcardnum = Udata.normal_luckcardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().normal_conditioncardnum = Udata.normal_conditioncardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().normal_constitutioncardnum = Udata.normal_constitutioncardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().normal_powercardnum = Udata.normal_powercardnum;

            GameObject.Find("UIController").GetComponent<GrowthCard>().rare_skilldamagecardnum = Udata.rare_skilldamagecardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().rare_hpcardnum = Udata.rare_hpcardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().rare_damagecardnum = Udata.rare_damagecardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().rare_defcardnum = Udata.rare_defcardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().rare_dodgecardnum = Udata.rare_dodgecardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().rare_strcardnum = Udata.rare_strcardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().rare_luckcardnum = Udata.rare_luckcardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().rare_conditioncardnum = Udata.rare_conditioncardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().rare_constitutioncardnum = Udata.rare_constitutioncardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().rare_powercardnum = Udata.rare_powercardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().rare_skilldamagecardnum = Udata.rare_skilldamagecardnum;

            GameObject.Find("UIController").GetComponent<GrowthCard>().epic_skilldamagecardnum = Udata.epic_skilldamagecardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().epic_hpcardnum = Udata.epic_hpcardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().epic_damagecardnum = Udata.epic_damagecardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().epic_defcardnum = Udata.epic_defcardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().epic_dodgecardnum = Udata.epic_dodgecardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().epic_strcardnum = Udata.epic_strcardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().epic_luckcardnum = Udata.epic_luckcardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().epic_conditioncardnum = Udata.epic_conditioncardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().epic_constitutioncardnum = Udata.epic_constitutioncardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().epic_powercardnum = Udata.epic_powercardnum;
            GameObject.Find("UIController").GetComponent<GrowthCard>().epic_skilldamagecardnum = Udata.epic_skilldamagecardnum;

            GameObject.Find("UIController").GetComponent<UIController>().probability1 = Udata.probability1;
            GameObject.Find("UIController").GetComponent<UIController>().probability2 = Udata.probability2;

            GameObject.Find("UIController").GetComponent<UIController>().decCardProperty = Udata.decCardProperty;
            GameObject.Find("UIController").GetComponent<UIController>().addCardProperty = Udata.addCardProperty;
            //Array.Copy(Udata.decCardProperty, GameObject.Find("UIController").GetComponent<UIController>().decCardProperty, 20);
            //Array.Copy(Udata.addCardProperty, GameObject.Find("UIController").GetComponent<UIController>().addCardProperty, 10);

            GameObject.Find("UIController").GetComponent<UIController>().LvChSelect = Udata.LvChSelect;
            //Array.Copy(Udata.LvChSelect, GameObject.Find("UIController").GetComponent<UIController>().LvChSelect, 12);
            GameObject.Find("UIController").GetComponent<UIController>().max_index = Udata.max_index;

            //GameObject.Find("UIController").GetComponent<UIController>().NPCCardProperty = Udata.NPCCardProperty;
            ////Array.Copy(Udata.NPCCardProperty, GameObject.Find("UIController").GetComponent<UIController>().NPCCardProperty, 16);
            //GameObject.Find("UIController").GetComponent<UIController>().NPC_Buy = Udata.NPC_Buy;
            #endregion        
        }
        file.Close();
    }
}



public static class LoadBool
{
    public static bool playerload = false;
    public static bool uiload = false;
}