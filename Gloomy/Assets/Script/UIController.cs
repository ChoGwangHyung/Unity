using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

enum GrowthCardList { Hp, Damage, Def, Dodge, Str, Luck, Condition, Constitution, Power, SkillDamage };

public class UIController : MonoBehaviour
{
    public Text goldText;
    public Image lvImage;
    public int player_Lv;
    public GameObject Store;

    #region 상점 선택
    bool selectedStore;
    bool store_Dec;
    bool store_GrowthCard;
    public bool store_Characteristic;
    int selectStore;
    bool openStore;
    #endregion

    #region 덱
    public GameObject DecUI;
    public List<GameObject> cardDec;
    public GameObject Dec_card;
    public List<GameObject> addDec;
    public GameObject Add_card;
    bool DecSelect;
    int Dec_index;
    int Add_index;
    bool rotateCard;
    int rotationVal;
    public Sprite backCardImage;
    bool mix_addCard;
    bool mix_decCard;
    bool dec_collect;
    int dec_rotationVal;
    #endregion

    #region 성장카드
    public GameObject GrowthCardUI;
    public GameObject growthCard;
    public Text HpCardText, DamageCardText, DefCardText, DodgeCardText, StrCardText, LuckCardText, ConditionCardText, ConstitutionCardText, PowerCardText, SkillDamageCardText;
    public int[,] decCardProperty = new int[10, 2];
    public int[,] addCardProperty = new int[5, 2];
    public int probability1;
    public int probability2;
    public int monstercountermax;
    public int seq;
    #endregion

    public GameObject GameClearUI;

    #region 특성
    public GameObject CharacteristicUI;
    public GameObject[,] CharacteristicList = new GameObject[6, 3];
    public GameObject[,] CharacteristicXList = new GameObject[6, 3];
    public GameObject[] CharacteristicLvImage = new GameObject[6];
    public GameObject Ch_selecter;
    public GameObject[] Ch_selected = new GameObject[6];
    public int[,] LvChSelect = new int[6, 2];
    public int Lv_index;
    int Ch_index;
    public int max_index;
    public GameObject explanation;
    public Text explanationText;
    public bool AniBool;
    #endregion

    #region NPC
    //public GameObject NPCUI;
    //public GameObject[,] NPC_Card = new GameObject[4, 3];
    //public int[,,] NPCCardProperty = new int[4, 2, 2];
    //public int[] NPC_Buy = new int[12];
    //public GameObject N_selecter;
    //int npcCard1_index;
    //int npcCard2_index;
    //int npcbought_index;
    //bool MeetNPC;
    //public Sprite selected;
    #endregion

    #region 환경설정
    public GameObject GameSetUI;
    public GameObject[] GameSetList = new GameObject[4];
    public GameObject GameSetSaveWindow;
    bool OpenGameSet;
    int set_index;
    public Slider Volume;
    public float Vol;
    float SaveWindowTime;
    bool Saving;
    public GameObject GameOverUI;
    public GameObject[] GameOverList = new GameObject[2];
    int over_index;
    bool death;
    bool death_audioPlay;
    #endregion

    #region 튜토리얼
    int SystemInfo_seq;
    //조작법 튜토리얼
    public GameObject GameTutorial;
    public Text GameStartInfo;
    public Text GameSystemInfo;
    public GameObject GameTutorialBackGround;
    bool OnGameTutorial;
    float OnTutorialTime;
    //상점 튜토리얼
    public GameObject StoreTutorial;
    bool OnDecTutorial;
    public GameObject IndecSystemInfo;
    bool OnGrowthCardTutorial;
    bool OnCharacteristicTutorial;
    public Text StoreSystemInfo;
    public GameObject StoreTutorialBackGround;
    public Text StoreNextText;
    bool OnStoreTutorial;
    #endregion

    //HP바, EXP바
    public Slider HpBar;
    public Slider ExpBar;

    //플레이어 카드
    public GameObject playerBackCard;
    int BackCardVal;
    float ActiveTime;
    bool isActive;

    //사운드
    public AudioSource BGM;
    public AudioSource audio;
    public AudioClip audio_ButtomDown;
    public AudioClip audio_CardGet;
    public AudioClip audio_CardShuffle;
    public AudioClip audio_Dead;

    //스타트
    void Start()
    {
        #region 상점 선택
        openStore = false;
        selectedStore = false;
        store_Dec = false;
        store_GrowthCard = false;
        store_Characteristic = false;
        selectStore = 1;
        #endregion

        #region 덱 UI
        //덱리스트 10개 만들기
        for (int i = 1; i <= 10; i++)
        {
            Dec_card = GameObject.Find("card" + i);
            cardDec.Add(Dec_card);
        }
        Dec_card = GameObject.Find("DecListSelect");
        Dec_card.SetActive(false);
        Dec_index = 0;
        //추가되는 덱 5개 만들기
        for (int i = 1; i <= 5; i++)
        {
            Add_card = GameObject.Find("addcard" + i);
            addDec.Add(Add_card);
        }
        Add_card = GameObject.Find("AddDecSelect");
        Add_card.SetActive(false);
        DecSelect = false;
        Add_index = 0;
        rotateCard = false;
        rotationVal = 5;
        mix_addCard = false;
        mix_decCard = false;
        dec_collect = false;
        dec_rotationVal = 5;
        #endregion

        #region 카드속성
        if (!LoadBool.uiload)
        {
            for (int i = 0; i < 10; i++)
            {
                decCardProperty[i, 0] = i;
                decCardProperty[i, 1] = 0;

                if (i < 5)
                {
                    int randomProperty = Random.Range(0, 10);
                    addCardProperty[i, 0] = randomProperty;
                    addCardProperty[i, 1] = 0;
                    addDec[i].GetComponent<Image>().sprite = Resources.Load("" + (randomProperty + 1) + "-1", typeof(Sprite)) as Sprite;
                }
            }
        }
        probability1 = 80;
        probability2 = 95;

        seq = 0;
        monstercountermax = 3;
        #endregion

        #region 특성
        for (int i = 1; i <= 6; i++)
        {
            int lv = 1;
            if (i == 2) lv = 3;
            if (i == 3) lv = 8;
            if (i == 4) lv = 13;
            if (i == 5) lv = 17;
            if (i == 6) lv = 20;

            for (int j = 1; j <= 3; j++)
            {
                CharacteristicList[i - 1, j - 1] = GameObject.Find("Level" + lv + " (" + j + ")");
                CharacteristicXList[i - 1, j - 1] = GameObject.Find("Level" + lv + " (" + j + ")x");
            }

            LvChSelect[i - 1, 0] = 0;

            Ch_selected[i - 1].SetActive(false);
        }
        Lv_index = 0;
        Ch_index = 0;
        max_index = 0;
        CharacteristicExplanation();
        AniBool = false;
        #endregion

        #region NPC        
        //for (int i = 0; i < 4; i++)
        //{
        //    for (int j = 1; j <= 3; j++)
        //    {
        //        NPC_Card[i, j - 1] = GameObject.Find("Ncard" + (j + i * 3));
        //    }
        //}

        //npcCard1_index = 0;
        //npcCard2_index = 0;

        //if (LoadBool.uiload == false)
        //{
        //    NPCCardRandomMix();
        //    for (int i = 0; i < 12; i++) NPC_Buy[i] = 0;
        //}
        //else
        //{
        //    int bought_index = 0;
        //    for (int i = 0; i < 4; i++)
        //    {
        //        for (int j = 0; j < 2; j++)
        //        {
        //            if (NPC_Buy[bought_index] == 0)
        //                NPC_Card[i, j].GetComponent<Image>().sprite = Resources.Load("" + (NPCCardProperty[i, j, 0] + 1) + "-" + (NPCCardProperty[i, j, 1] + 1), typeof(Sprite)) as Sprite;
        //            else
        //                NPC_Card[i, j].GetComponent<Image>().sprite = selected;
        //            bought_index++;
        //        }
        //    }

        //    int index = 0;
        //    for (int i = 8; i < 12; i++)
        //    {
        //        if (NPC_Buy[i] == 0) NPC_Card[index, 3].GetComponent<Image>().sprite = selected;
        //        index++;                
        //    }
        //}
        //npcbought_index = 0;
        #endregion

        #region 환경설정
        GameSetList[0] = GameObject.Find("Continue");
        GameSetList[1] = GameObject.Find("Save");
        GameSetList[2] = GameObject.Find("Sound");
        GameSetList[3] = GameObject.Find("Game Over");
        OpenGameSet = false;
        set_index = 0;
        Saving = false;
        SaveWindowTime = 0;
        GameSetSaveWindow.SetActive(false);
        #endregion

        #region 죽음
        GameOverList[0] = GameObject.Find("Back Savepoint");
        GameOverList[1] = GameObject.Find("Back Title");
        over_index = 0;
        death = false;
        death_audioPlay = true;
        #endregion

        #region 사운드
        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.audio.loop = false;
        Vol = 1.0f;
        Volume.value = Vol;
        audio.volume = Volume.value;
        #endregion

        #region 튜토리얼
        SystemInfo_seq = 0;
        //조작법 튜토리얼    
        GameSystemInfo.enabled = false;
        OnGameTutorial = true;
        OnTutorialTime = 10.0f;
        //상점 튜토리얼
        StoreTutorial.SetActive(false);
        IndecSystemInfo.SetActive(false);
        StoreNextText.enabled = false;
        OnStoreTutorial = false;
        OnDecTutorial = true;
        OnGrowthCardTutorial = false;
        OnCharacteristicTutorial = false;
        #endregion

        player_Lv = 2;

        playerBackCard.SetActive(false);
        isActive = false;
        ActiveTime = 3.0f;

        GrowthCardUI.SetActive(false);
        CharacteristicUI.SetActive(false);
        Store.SetActive(false);
        //NPCUI.SetActive(false);
        GameSetUI.SetActive(false);
        GameOverUI.SetActive(false);
        GameClearUI.SetActive(false);

        if (LoadBool.uiload)
        {
            OnGameTutorial = false;
            GameTutorial.SetActive(false);
            GameObject.Find("Data Manager").GetComponent<DataManager>().ULoadData();            
            for (int i = 0; i < 10; i++)
            {
                if(i<seq) cardDec[i].GetComponent<Image>().sprite = backCardImage;
                else cardDec[i].GetComponent<Image>().sprite = Resources.Load("" + (decCardProperty[i, 0] + 1) + "-" + (decCardProperty[i, 1] + 1), typeof(Sprite)) as Sprite;
                if (i < 5) addDec[i].GetComponent<Image>().sprite = Resources.Load("" + (addCardProperty[i, 0] + 1) + "-" + (addCardProperty[i, 1] + 1), typeof(Sprite)) as Sprite;
            }
        }
    }
    //업데이트
    void Update()
    {
        Tutorial();

        SaveWindowOpenAndClose();

        PlayerHpBar();
        PlayerExpBar();

        UpdateText();
        LvImage();

        DecCardDrawing();

        CharacteristicUnLocked();

        if (mix_addCard) RotateCard(addDec, addCardProperty);
        else if (mix_decCard) RotateCard(cardDec, decCardProperty);


        //MeetNPC=GameObject.Find("").GetComponet<>().;        
        death = GameObject.Find("player").GetComponent<playermovement>().death;

        if (death)
        {
            BGM.Stop();
            GameOverUI.SetActive(true);
            GameOverList[over_index].transform.Find("Text").GetComponent<Text>().color = Color.red;
            if (death_audioPlay)
            {
                this.audio.clip = audio_Dead;
                this.audio.Play();
                death_audioPlay = false;
            }
        }

        //if (MeetNPC)
        //{
        //    NPCUI.SetActive(true);
        //    GameObject.Find("player").GetComponent<playermovement>().KeyControl = false;
        //}

        if (openStore)
        {
            WhatSelectStoreList();
            ShowSelectedStoreList();
        }

        #region 키입력
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (/*!MeetNPC &&*/ !OpenGameSet && !death && !GameObject.Find("player").GetComponent<playermovement>().gameset)
            {
                OpenAndCloseStore();
                selectedStore = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PressSpace();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            UIMoveRight();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            UIMoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PressEsc();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            PressA();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            UIMoveUp();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            UIMoveDown();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            PressEnter();
        }
        if (GameObject.Find("player").GetComponent<playermovement>().gameset)
        {
            GameObject.Find("player").GetComponent<playermovement>().KeyControl = false;
            GameClearUI.SetActive(true);
        }


        #endregion
    }

    //text 함수
    void UpdateText()
    {
        goldText.text = "image  " + GameObject.Find("player").GetComponent<Playerstat>().gold + "$";

        //성장카드 텍스트
        HpCardText.text = "" + growthCard.GetComponent<GrowthCard>().normal_hpcardnum + "\n\n" + growthCard.GetComponent<GrowthCard>().rare_hpcardnum + "\n\n" + growthCard.GetComponent<GrowthCard>().epic_hpcardnum;
        DamageCardText.text = "" + growthCard.GetComponent<GrowthCard>().normal_damagecardnum + "\n\n" + growthCard.GetComponent<GrowthCard>().rare_damagecardnum + "\n\n" + growthCard.GetComponent<GrowthCard>().epic_damagecardnum;
        DefCardText.text = "" + growthCard.GetComponent<GrowthCard>().normal_defcardnum + "\n\n" + growthCard.GetComponent<GrowthCard>().rare_defcardnum + "\n\n" + growthCard.GetComponent<GrowthCard>().epic_defcardnum;
        DodgeCardText.text = "" + growthCard.GetComponent<GrowthCard>().normal_dodgecardnum + "\n\n" + growthCard.GetComponent<GrowthCard>().rare_dodgecardnum + "\n\n" + growthCard.GetComponent<GrowthCard>().epic_dodgecardnum;
        StrCardText.text = "" + growthCard.GetComponent<GrowthCard>().normal_strcardnum + "\n\n" + growthCard.GetComponent<GrowthCard>().rare_strcardnum + "\n\n" + growthCard.GetComponent<GrowthCard>().epic_strcardnum;
        LuckCardText.text = "" + growthCard.GetComponent<GrowthCard>().normal_luckcardnum + "\n\n" + growthCard.GetComponent<GrowthCard>().rare_luckcardnum + "\n\n" + growthCard.GetComponent<GrowthCard>().epic_luckcardnum;
        ConditionCardText.text = "" + growthCard.GetComponent<GrowthCard>().normal_conditioncardnum + "\n\n" + growthCard.GetComponent<GrowthCard>().rare_conditioncardnum + "\n\n" + growthCard.GetComponent<GrowthCard>().epic_conditioncardnum;
        ConstitutionCardText.text = "" + growthCard.GetComponent<GrowthCard>().normal_constitutioncardnum + "\n\n" + growthCard.GetComponent<GrowthCard>().rare_constitutioncardnum + "\n\n" + growthCard.GetComponent<GrowthCard>().epic_constitutioncardnum;
        PowerCardText.text = "" + growthCard.GetComponent<GrowthCard>().normal_powercardnum + "\n\n" + growthCard.GetComponent<GrowthCard>().rare_powercardnum + "\n\n" + growthCard.GetComponent<GrowthCard>().epic_powercardnum;
        SkillDamageCardText.text = "" + growthCard.GetComponent<GrowthCard>().normal_skilldamagecardnum + "\n\n" + growthCard.GetComponent<GrowthCard>().rare_skilldamagecardnum + "\n\n" + growthCard.GetComponent<GrowthCard>().epic_skilldamagecardnum;
    }

    //Lv 이미지
    void LvImage()
    {
        if (player_Lv == GameObject.Find("player").GetComponent<Playerstat>().charLv && player_Lv < 21)
        {
            lvImage.sprite = Resources.Load(("" + player_Lv), typeof(Sprite)) as Sprite;
            player_Lv++;
        }
    }

    //상점 열고 닫기 함수
    void OpenAndCloseStore()
    {
        openStore = !openStore;
        if (openStore)
        {
            Store.SetActive(true);
            GameObject.Find("player").GetComponent<playermovement>().KeyControl = false;
        }
        else
        {
            Store.SetActive(false);
            GameObject.Find("player").GetComponent<playermovement>().KeyControl = true;
        }
    }

    //키입력 함수들
    #region 키입력 합수들
    void UIMoveRight()
    {
        //상점선택
        if (openStore)
        {
            if (!selectedStore)
            {
                if (selectStore < 3) selectStore++;
                else selectStore = 1;
            }
            //덱
            if (store_Dec && selectedStore)
            {
                if (!DecSelect)
                {
                    if (Dec_index < 9) Dec_index++;
                    else Dec_index = 0;
                    Dec_card.transform.position = cardDec[Dec_index].gameObject.transform.position;
                }
                else
                {
                    if (Add_index < 4) Add_index++;
                    else Add_index = 0;
                    Add_card.transform.position = addDec[Add_index].gameObject.transform.position;
                }
            }
            //특성
            if (store_Characteristic && selectedStore)
            {
                if (Lv_index < max_index)
                {
                    if (LvChSelect[Lv_index + 1, 0] == 0)
                    {
                        Lv_index++;
                    }
                    else
                    {
                        Lv_index++;
                        Ch_index = LvChSelect[Lv_index, 1];
                    }
                }
                else
                {
                    if (LvChSelect[0, 0] == 0)
                    {
                        Lv_index = 0;
                    }
                    else
                    {
                        Lv_index = 0;
                        Ch_index = LvChSelect[Lv_index, 1];
                    }
                }
                Ch_selecter.transform.position = CharacteristicList[Lv_index, Ch_index].gameObject.transform.position;
                CharacteristicExplanation();
            }
        }

        //NPC
        //if (MeetNPC)
        //{
        //    if (npcCard1_index < 3) npcCard1_index++;
        //    else npcCard1_index = 0;
        //    N_selecter.transform.position = NPC_Card[npcCard1_index, npcCard2_index].gameObject.transform.position;
        //}

        //환경설정
        if (OpenGameSet)
        {
            if (set_index == 2)
            {
                if (Volume.value < 1f) Volume.value += 0.1f;

                audio.volume = Volume.value;
                Vol = Volume.value;
                BGM.volume = Vol;
            }
        }
    }

    void UIMoveLeft()
    {
        //상점선택
        if (openStore)
        {
            if (!selectedStore)
            {
                if (selectStore > 1) selectStore--;
                else selectStore = 3;
            }
            //덱
            if (store_Dec && selectedStore)
            {
                if (!DecSelect)
                {
                    if (Dec_index > 0) Dec_index--;
                    else Dec_index = 9;
                    Dec_card.transform.position = cardDec[Dec_index].gameObject.transform.position;
                }
                else
                {
                    if (Add_index > 0) Add_index--;
                    else Add_index = 4;
                    Add_card.transform.position = addDec[Add_index].gameObject.transform.position;
                }
            }
            //특성
            if (store_Characteristic && selectedStore)
            {
                if (Lv_index > 0)
                {
                    if (LvChSelect[Lv_index - 1, 0] == 0)
                    {
                        Lv_index--;
                    }
                    else
                    {
                        Lv_index--;
                        Ch_index = LvChSelect[Lv_index, 1];
                    }
                }
                else
                {
                    Lv_index = max_index;
                    if (LvChSelect[Lv_index, 0] == 1) Ch_index = LvChSelect[Lv_index, 1];
                }
                Ch_selecter.transform.position = CharacteristicList[Lv_index, Ch_index].gameObject.transform.position;
                CharacteristicExplanation();
            }
        }

        //NPC
        //if (MeetNPC)
        //{
        //    if (npcCard1_index > 0) npcCard1_index--;
        //    else npcCard1_index = 3;
        //    N_selecter.transform.position = NPC_Card[npcCard1_index, npcCard2_index].gameObject.transform.position;
        //}

        //환경설정
        if (OpenGameSet)
        {
            if (set_index == 2)
            {
                if (Volume.value > 0f) Volume.value -= 0.1f;

                audio.volume = Volume.value;
                Vol = Volume.value;
                BGM.volume = Vol;
            }
        }
    }

    void UIMoveUp()
    {
        //특성
        if (store_Characteristic && selectedStore)
        {
            if (Ch_index > 0) Ch_index--;
            else Ch_index = 2;
            Ch_selecter.transform.position = CharacteristicList[Lv_index, Ch_index].gameObject.transform.position;
            CharacteristicExplanation();
        }

        //NPC
        //if (MeetNPC)
        //{
        //    if (npcCard2_index > 0) npcCard2_index--;
        //    else npcCard2_index = 2;
        //    N_selecter.transform.position = NPC_Card[npcCard1_index, npcCard2_index].gameObject.transform.position;
        //}

        //환경설정
        if (OpenGameSet)
        {
            if (set_index > 0) GameSetList[set_index--].transform.Find("Text").GetComponent<Text>().color = Color.black;
            else
            {
                GameSetList[set_index].transform.Find("Text").GetComponent<Text>().color = Color.black;
                set_index = 3;
            }
            GameSetList[set_index].transform.Find("Text").GetComponent<Text>().color = Color.red;
        }

        //죽음
        if (death)
        {
            if (over_index > 0) GameOverList[over_index--].transform.Find("Text").GetComponent<Text>().color = Color.black;
            else
            {
                GameOverList[over_index].transform.Find("Text").GetComponent<Text>().color = Color.black;
                over_index = 1;
            }
            GameOverList[over_index].transform.Find("Text").GetComponent<Text>().color = Color.red;
        }
    }

    void UIMoveDown()
    {
        //특성
        if (store_Characteristic && selectedStore)
        {
            if (Ch_index < 2) Ch_index++;
            else Ch_index = 0;
            Ch_selecter.transform.position = CharacteristicList[Lv_index, Ch_index].gameObject.transform.position;
            CharacteristicExplanation();
        }

        //NPC
        //if (MeetNPC)
        //{
        //    if (npcCard2_index < 2) npcCard2_index++;
        //    else npcCard2_index = 0;
        //    N_selecter.transform.position = NPC_Card[npcCard1_index, npcCard2_index].gameObject.transform.position;
        //}

        //환경설정
        if (OpenGameSet)
        {
            if (set_index < 3) GameSetList[set_index++].transform.Find("Text").GetComponent<Text>().color = Color.black;
            else
            {
                GameSetList[set_index].transform.Find("Text").GetComponent<Text>().color = Color.black;
                set_index = 0;
            }
            GameSetList[set_index].transform.Find("Text").GetComponent<Text>().color = Color.red;
        }

        //죽음
        if (death)
        {
            if (over_index < 1) GameOverList[over_index++].transform.Find("Text").GetComponent<Text>().color = Color.black;
            else
            {
                GameOverList[over_index].transform.Find("Text").GetComponent<Text>().color = Color.black;
                over_index = 0;
            }
            GameOverList[over_index].transform.Find("Text").GetComponent<Text>().color = Color.red;
        }
    }

    void PressSpace()
    {
        //상점선택
        if (openStore && !selectedStore)
        {
            selectedStore = true;
        }
        //덱
        else if (store_Dec)
        {
            if (!DecSelect)
            {
                if (seq > Dec_index) return;
                Add_card.SetActive(true);
                DecSelect = true;
                Add_index = 0;
                Add_card.transform.position = addDec[Add_index].gameObject.transform.position;
            }
            else
            {
                Vector3 pos = addDec[Add_index].gameObject.transform.position;
                cardDec.Insert(Dec_index + 1, addDec[Add_index]);
                cardDec[Dec_index + 1].gameObject.transform.position = cardDec[Dec_index].gameObject.transform.position;
                addDec.Insert(Add_index + 1, cardDec[Dec_index]);
                addDec[Add_index + 1].gameObject.transform.position = pos;
                cardDec.RemoveAt(Dec_index);
                addDec.RemoveAt(Add_index);

                for (int i = 0; i < 2; i++)
                {
                    int value = decCardProperty[Dec_index, i];
                    decCardProperty[Dec_index, i] = addCardProperty[Add_index, i];
                    addCardProperty[Add_index, i] = value;
                }

                Add_card.SetActive(false);
                DecSelect = false;
            }


        }
        //특성
        else if (store_Characteristic)
        {
            if (LvChSelect[Lv_index, 0] == 0)
            {
                for (int i = 0; i < 3; i++) if (i != Ch_index) CharacteristicXList[Lv_index, i].SetActive(true);

                Ch_selected[Lv_index].SetActive(true);
                Ch_selected[Lv_index].gameObject.transform.position = CharacteristicList[Lv_index, Ch_index].gameObject.transform.position;

                LvChSelect[Lv_index, 1] = Ch_index;
                LvChSelect[Lv_index, 0] = 1;
                InsertExplanation();
                AniBool = true;
            }
        }

        //NPC
        //if (MeetNPC)
        //{
        //    if (NPC_Card[npcCard1_index, npcCard2_index].GetComponent<Image>().sprite != selected)
        //    {
        //        int curGold = GameObject.Find("player").GetComponent<Playerstat>().gold;

        //        if (npcCard2_index < 2)
        //        {
        //            growthCard.GetComponent<GrowthCard>().whatcard = NPCCardProperty[npcCard1_index, npcCard2_index, 1];
        //            NPCCardBoughtPos();
        //            if (NPCCardProperty[npcCard1_index, npcCard2_index, 1] == 0 && curGold >= 100)
        //            {
        //                GameObject.Find("player").GetComponent<Playerstat>().gold -= 100;
        //                growthCard.GetComponent<GrowthCard>().PickCard(NPCCardProperty[npcCard1_index, npcCard2_index, 0]);
        //                NPC_Card[npcCard1_index, npcCard2_index].GetComponent<Image>().sprite = selected;
        //                NPC_Buy[npcbought_index] = 1;
        //            }
        //            if (NPCCardProperty[npcCard1_index, npcCard2_index, 1] == 1 && curGold >= 150)
        //            {
        //                GameObject.Find("player").GetComponent<Playerstat>().gold -= 150;
        //                growthCard.GetComponent<GrowthCard>().PickCard(NPCCardProperty[npcCard1_index, npcCard2_index, 0]);
        //                NPC_Card[npcCard1_index, npcCard2_index].GetComponent<Image>().sprite = selected;
        //                NPC_Buy[npcbought_index] = 1;
        //            }
        //            if (NPCCardProperty[npcCard1_index, npcCard2_index, 1] == 2 && curGold >= 200)
        //            {
        //                GameObject.Find("player").GetComponent<Playerstat>().gold -= 200;
        //                growthCard.GetComponent<GrowthCard>().PickCard(NPCCardProperty[npcCard1_index, npcCard2_index, 0]);
        //                NPC_Card[npcCard1_index, npcCard2_index].GetComponent<Image>().sprite = selected;
        //                NPC_Buy[npcbought_index] = 1;
        //            }
        //        }
        //        else
        //        {
        //            if (curGold >= 100 && npcCard1_index == 0)
        //            {
        //                probability1 -= 3;
        //                GameObject.Find("player").GetComponent<Playerstat>().gold -= 100;
        //                NPC_Card[npcCard1_index, npcCard2_index].GetComponent<Image>().sprite = selected;
        //                NPC_Buy[8] = 1;
        //            }
        //            if (curGold >= 150 && npcCard1_index == 1)
        //            {
        //                probability1 -= 3;
        //                probability2 -= 1;
        //                GameObject.Find("player").GetComponent<Playerstat>().gold -= 150;
        //                NPC_Card[npcCard1_index, npcCard2_index].GetComponent<Image>().sprite = selected;
        //                NPC_Buy[9] = 1;
        //            }
        //            if (curGold >= 200 && npcCard1_index == 2)
        //            {
        //                probability1 -= 2;
        //                probability2 -= 2;
        //                GameObject.Find("player").GetComponent<Playerstat>().gold -= 200;
        //                NPC_Card[npcCard1_index, npcCard2_index].GetComponent<Image>().sprite = selected;
        //                NPC_Buy[10] = 1;
        //            }
        //            if (curGold >= 250 && npcCard1_index == 3)
        //            {
        //                probability1 -= 1;
        //                probability2 -= 2;
        //                GameObject.Find("player").GetComponent<Playerstat>().gold -= 250;
        //                NPC_Card[npcCard1_index, npcCard2_index].GetComponent<Image>().sprite = selected;
        //                NPC_Buy[11] = 1;
        //            }
        //        }
        //    }
        //}

        //환경설정
        if (OpenGameSet)
        {
            if (set_index == 0)
            {
                GameSetUI.SetActive(false);
                OpenGameSet = false;
                GameObject.Find("player").GetComponent<playermovement>().KeyControl = true;
            }

            if (set_index == 1)
            {
                GameObject.Find("Data Manager").GetComponent<DataManager>().SaveData();
                SaveWindowTime = 0;
                Saving = true;
                GameSetSaveWindow.SetActive(true);
            }

            if (set_index == 3)
            {
                Application.Quit();
            }

            this.audio.clip = audio_ButtomDown;
            this.audio.Play();
        }

        //죽음
        if (death)
        {
            if (over_index == 0)
            {
                SceneManager.LoadScene("Main");
            }

            if (over_index == 1)
            {
                SceneManager.LoadScene("Title");
            }
        }

        if (GameObject.Find("player").GetComponent<playermovement>().gameset)
        {
            SceneManager.LoadScene("Title");
        }
    }

    void PressEsc()
    {
        //환경설정
        if ((OpenGameSet || !OpenGameSet) && !openStore && /*!MeetNPC &&*/ !death && !GameObject.Find("player").GetComponent<playermovement>().gameset)

        {
            if (OpenGameSet)
            {
                GameSetUI.SetActive(false);
                GameObject.Find("player").GetComponent<playermovement>().KeyControl = true;
            }
            else
            {
                GameSetUI.SetActive(true);
                GameObject.Find("player").GetComponent<playermovement>().KeyControl = false;
                set_index = 0;
                GameSetList[set_index].transform.Find("Text").GetComponent<Text>().color = Color.red;
                for (int i = 1; i < 3; i++) GameSetList[i].transform.Find("Text").GetComponent<Text>().color = Color.black;
            }
            OpenGameSet = !OpenGameSet;
        }

        //상점선택
        if (!selectedStore && openStore)
        {
            openStore = false;
            Store.SetActive(false);
        }

        //덱
        if (store_Dec && !DecSelect)
        {
            Dec_card.SetActive(false);
            store_Dec = false;
            selectedStore = false;
            Dec_index = 0;
            Dec_card.transform.position = cardDec[Dec_index].gameObject.transform.position;
        }

        if (DecSelect)
        {
            Add_card.SetActive(false);
            DecSelect = false;
            Add_index = 0;
            Add_card.transform.position = addDec[Add_index].gameObject.transform.position;
        }

        //성장카드
        if (store_GrowthCard)
        {
            store_GrowthCard = false;
            selectedStore = false;
        }

        //특성
        if (store_Characteristic)
        {
            store_Characteristic = false;
            selectedStore = false;
        }

        ////NPC
        //if (MeetNPC)
        //{
        //    NPCUI.SetActive(false);
        //    MeetNPC = false;
        //    GameObject.Find("player").GetComponent<playermovement>().KeyControl = true;
        //}
    }

    void PressA()
    {
        //덱 랜덤섞기
        if (store_Dec)
        {
            if (GameObject.Find("player").GetComponent<Playerstat>().gold >= 10)
            {
                rotateCard = true;
                mix_addCard = true;
                GameObject.Find("player").GetComponent<Playerstat>().gold -= 10;

                this.audio.clip = audio_CardShuffle;
                this.audio.Play();
            }
        }
    }

    void PressEnter()
    {
        if (OpenGameSet) return;
        //조작법 튜토리얼
        if (OnGameTutorial)
        {
            if (SystemInfo_seq == 0)
            {
                GameStartInfo.enabled = false;
                GameTutorialBackGround.SetActive(false);
                GameSystemInfo.enabled = true;
                GameObject.Find("player").GetComponent<playermovement>().KeyControl = true;
            }
            if (SystemInfo_seq > 0 && OnTutorialTime == 10.0f)
            {
                OnGameTutorial = false;
                GameTutorial.SetActive(false);
                StoreTutorial.SetActive(true);
                OnStoreTutorial = true;
                SystemInfo_seq = 0;
                OpenAndCloseStore();
            }
        }
        //상점 튜토리얼
        if (OnStoreTutorial)
        {
            selectedStore = true;
            //상점 튜토리얼 종료
            if (!OnDecTutorial && !OnGrowthCardTutorial && !OnCharacteristicTutorial)
            {
                StoreTutorial.SetActive(false);
                OnStoreTutorial = false;
                OpenAndCloseStore();
            }
            //덱 튜토리얼
            if (OnDecTutorial)
            {
                SystemInfo_seq++;
                if (SystemInfo_seq == 1)
                    StoreSystemInfo.text = "덱 시스템\n\n덱과 추가 덱의 카드는 Space바 키를 이용해 무상으로 바꿀 수 있으며\n이미 적용된 덱의 카드는 바꿀 수 없습니다.";
                if (SystemInfo_seq == 2)
                    StoreSystemInfo.text = "덱 시스템\n\n추가 덱은 10골드를 사용하면 랜덤으로 다른 카드가 나옵니다.\n이제 덱 시스템을 사용해 보세요.";
                if (SystemInfo_seq == 3)
                {
                    StoreSystemInfo.enabled = false;
                    IndecSystemInfo.SetActive(true);
                    StoreTutorialBackGround.SetActive(false);
                    StoreNextText.enabled = true;
                }
                //성장카드로 넘어감
                if (SystemInfo_seq == 4)
                {
                    selectStore++;
                    IndecSystemInfo.SetActive(false);
                    OnDecTutorial = false;
                    SystemInfo_seq = 0;

                    StoreSystemInfo.enabled = true;
                    StoreSystemInfo.text = "성장카드 시스템\n\n현재까지 적용된 덱의 카드 속성의 개수를 보여주는 시스템입니다.\n이제 성장카드 시스템을 확인해 보세요.";
                    StoreTutorialBackGround.SetActive(true);
                    StoreNextText.enabled = false;
                    OnGrowthCardTutorial = true;
                }
            }
            //성장카드 튜토리얼
            else if (OnGrowthCardTutorial)
            {
                SystemInfo_seq++;
                if (SystemInfo_seq == 1)
                {
                    StoreSystemInfo.enabled = false;
                    StoreTutorialBackGround.SetActive(false);
                    StoreNextText.enabled = true;
                }
                //특성으로 넘어감
                if (SystemInfo_seq == 2)
                {
                    selectStore++;
                    OnGrowthCardTutorial = false;
                    SystemInfo_seq = 0;

                    StoreSystemInfo.enabled = true;
                    StoreSystemInfo.text = "특성 시스템\n\n레벨에 따라 특성 3가지 중 1가지를 고를 수 있으며\n선택한 특성을 다시 되돌릴 수는 없습니다.";
                    StoreTutorialBackGround.SetActive(true);
                    StoreNextText.enabled = false;
                    OnCharacteristicTutorial = true;
                }
            }
            //특성 튜토리얼
            else if (OnCharacteristicTutorial)
            {
                SystemInfo_seq++;
                if (SystemInfo_seq == 1)
                    StoreSystemInfo.text = "특성 시스템\n\n특성의 설명을 읽고 원하는 특성을 선택하시면 됩니다.\n이제 특성 시스템을 사용해 보세요.";
                if (SystemInfo_seq == 2)
                {
                    StoreSystemInfo.enabled = false;
                    StoreTutorialBackGround.SetActive(false);
                    StoreNextText.enabled = true;
                }
                //상점 튜토리얼 끝
                if (SystemInfo_seq == 3)
                {
                    selectStore = 0;
                    OnCharacteristicTutorial = false;
                    SystemInfo_seq = 0;

                    StoreSystemInfo.enabled = true;
                    StoreSystemInfo.text = "상점 시스템\n\n\n설명은 이제 끝났습니다. 행운을 빕니다.";
                    StoreTutorialBackGround.SetActive(true);
                    StoreNextText.enabled = false;
                }
            }
        }
    }
    #endregion

    //덱, 성장카드, 특성 UI 활성화 비활성화
    void WhatSelectStoreList()
    {
        if (selectStore == 1)
        {
            store_Dec = true;
            store_GrowthCard = false;
            store_Characteristic = false;
        }
        else if (selectStore == 2)
        {
            store_GrowthCard = true;
            store_Dec = false;
            store_Characteristic = false;
        }
        else if (selectStore == 3)
        {
            store_Characteristic = true;
            store_GrowthCard = false;
            store_Dec = false;
        }
    }

    void ShowSelectedStoreList()
    {
        if (store_Dec)
        {
            DecUI.SetActive(true);
            Dec_card.SetActive(true);
        }
        else if (!store_Dec)
        {
            DecUI.SetActive(false);
        }
        if (store_GrowthCard)
        {
            GrowthCardUI.SetActive(true);
        }
        else if (!store_GrowthCard)
        {
            GrowthCardUI.SetActive(false);
        }
        if (store_Characteristic)
        {
            CharacteristicUI.SetActive(true);
        }
        else if (!store_Characteristic)
        {
            CharacteristicUI.SetActive(false);
        }
    }

    //랜덤으로 카드, 값 섞기
    void CardRandomMix(List<GameObject> card, int[,] property)
    {
        int size = card.Count;
        List<GameObject> temp = new List<GameObject>();
        Vector3[] pos = new Vector3[size];

        int[,] propertytemp = new int[size, 2];

        for (int i = 0; i < size; i++)
        {
            temp.Add(card[i]);
            pos[i] = card[i].gameObject.transform.position;

            propertytemp[i, 0] = property[i, 0];
            propertytemp[i, 1] = property[i, 1];
        }

        int[] ranarr = new int[size];
        bool isSame;

        for (int i = 0; i < size; ++i)
        {
            //랜덤 중복 index값 처리
            while (true)
            {
                ranarr[i] = Random.Range(0, size);
                isSame = false;

                for (int j = 0; j < i; ++j)
                {
                    if (ranarr[j] == ranarr[i])
                    {
                        isSame = true;
                        break;
                    }
                }
                if (!isSame) break;
            }
            //중복되지 않는 랜덤 변수 넣기
            int index = ranarr[i];
            card[i] = temp[index];
            card[i].gameObject.transform.position = pos[i];

            if (size == 10)
            {
                property[i, 0] = propertytemp[index, 0];
                property[i, 1] = propertytemp[index, 1];
            }
            else
            {
                int rank = Random.Range(0, 10);
                property[i, 0] = rank;

                int kinds = Random.Range(0, 100);
                if (kinds < probability1) { property[i, 1] = 0; };
                if (kinds < probability2 && kinds >= probability1) { property[i, 1] = 1; };
                if (kinds < 100 && kinds >= probability2) { property[i, 1] = 2; };
            }

            card[i].GetComponent<Image>().sprite = Resources.Load("" + (property[i, 0] + 1) + "-" + (property[i, 1] + 1), typeof(Sprite)) as Sprite;
        }

    }

    //카드 회전 이미지
    void RotateCard(List<GameObject> card, int[,] property)
    {
        if (!rotateCard) return;

        for (int i = 0; i < card.Count; i++)
        {
            card[i].transform.rotation = Quaternion.Euler(0, rotationVal, 0);

            if (rotationVal == 90)
                card[i].GetComponent<Image>().sprite = backCardImage;
            else if (rotationVal == 270)
                CardRandomMix(card, property);
        }

        rotationVal += 5;

        if (rotationVal == 365)
        {
            rotateCard = false;
            rotationVal = 5;
            mix_addCard = false;
            mix_decCard = false;
        }
    }

    //덱에서 카드뽑기
    void DecCardDrawing()
    {
        int draw = GameObject.Find("player").GetComponent<Playerstat>().monstercounter;

        if (dec_collect)
        {
            cardDec[BackCardVal].transform.rotation = Quaternion.Euler(0, dec_rotationVal, 0);

            if (dec_rotationVal == 90)
                cardDec[BackCardVal].GetComponent<Image>().sprite = backCardImage;

            dec_rotationVal += 5;

            if (dec_rotationVal == 185)
                dec_collect = false;
        }

        if (isActive)
        {
            ActiveTime -= Time.deltaTime;
            if (ActiveTime <= 0)
            {
                playerBackCard.SetActive(false);
                isActive = false;
            }
        }

        if (draw == monstercountermax)
        {
            GameObject.Find("player").GetComponent<Playerstat>().monstercounter = 0;

            this.audio.clip = audio_CardGet;
            this.audio.Play();

            growthCard.GetComponent<GrowthCard>().whatcard = decCardProperty[seq, 1];
            growthCard.GetComponent<GrowthCard>().PickCard(decCardProperty[seq, 0]);

            playerBackCard.SetActive(true);
            BackCardVal = seq;
            playerBackCard.GetComponent<Image>().sprite = Resources.Load("" + (decCardProperty[seq, 0] + 1) + "-" + (decCardProperty[seq, 1] + 1), typeof(Sprite)) as Sprite;
            ActiveTime = 3.0f;
            isActive = true;

            dec_collect = true;
            dec_rotationVal = 5;

            if (seq < 9) seq++;
            else if (seq >= 9)
            {
                seq = 0;
                rotateCard = true;
                mix_decCard = true;
                this.audio.clip = audio_CardShuffle;
                this.audio.Play();
            }
        }
    }

    //HP바, EXP바
    void PlayerHpBar()
    {
        float Hp = GameObject.Find("player").GetComponent<Playerstat>().hp;
        float max = GameObject.Find("player").GetComponent<Playerstat>().maxhp;
        HpBar.value = Hp / max;
    }

    void PlayerExpBar()
    {
        float Exp = GameObject.Find("player").GetComponent<Playerstat>().exp;
        float needExp = GameObject.Find("player").GetComponent<Playerstat>().needExp;
        ExpBar.value = Exp / needExp;
    }

    //특성 해제
    void CharacteristicUnLocked()
    {
        int cur_lv = GameObject.Find("player").GetComponent<Playerstat>().charLv;

        if (cur_lv >= 1)
        {
            if (LvChSelect[0, 0] == 0) for (int i = 0; i < 3; i++) CharacteristicXList[0, i].SetActive(false);
            if (LvChSelect[0, 0] == 1)
            {
                CharacteristicXList[0, LvChSelect[0, 1]].SetActive(false);
                Ch_selected[0].SetActive(true);
                Ch_selected[0].gameObject.transform.position = CharacteristicList[0, LvChSelect[0, 1]].gameObject.transform.position;
            }
            CharacteristicLvImage[0].GetComponent<Image>().sprite = Resources.Load("lv1" + "_활성", typeof(Sprite)) as Sprite;
        }

        if (cur_lv >= 3)
        {
            if (LvChSelect[1, 0] == 0) for (int i = 0; i < 3; i++) CharacteristicXList[1, i].SetActive(false);
            if (LvChSelect[1, 0] == 1)
            {
                CharacteristicXList[1, LvChSelect[1, 1]].SetActive(false);
                Ch_selected[1].SetActive(true);
                Ch_selected[1].gameObject.transform.position = CharacteristicList[1, LvChSelect[1, 1]].gameObject.transform.position;
            }
            CharacteristicLvImage[1].GetComponent<Image>().sprite = Resources.Load("lv3" + "_활성", typeof(Sprite)) as Sprite;
            max_index = 1;
        }

        if (cur_lv >= 8)
        {
            if (LvChSelect[2, 0] == 0) for (int i = 0; i < 3; i++) CharacteristicXList[2, i].SetActive(false);
            if (LvChSelect[2, 0] == 1)
            {
                CharacteristicXList[2, LvChSelect[2, 1]].SetActive(false);
                Ch_selected[2].SetActive(true);
                Ch_selected[2].gameObject.transform.position = CharacteristicList[2, LvChSelect[2, 1]].gameObject.transform.position;
            }
            CharacteristicLvImage[2].GetComponent<Image>().sprite = Resources.Load("lv8" + "_활성", typeof(Sprite)) as Sprite;
            max_index = 2;
        }

        if (cur_lv >= 13)
        {
            if (LvChSelect[3, 0] == 0) for (int i = 0; i < 3; i++) CharacteristicXList[3, i].SetActive(false);
            if (LvChSelect[3, 0] == 1)
            {
                CharacteristicXList[3, LvChSelect[3, 1]].SetActive(false);
                Ch_selected[3].SetActive(true);
                Ch_selected[3].gameObject.transform.position = CharacteristicList[3, LvChSelect[3, 1]].gameObject.transform.position;
            }
            CharacteristicLvImage[3].GetComponent<Image>().sprite = Resources.Load("lv13" + "_활성", typeof(Sprite)) as Sprite;
            max_index = 3;
        }

        if (cur_lv >= 17)
        {
            if (LvChSelect[4, 0] == 0) for (int i = 0; i < 3; i++) CharacteristicXList[4, i].SetActive(false);
            if (LvChSelect[4, 0] == 1)
            {
                CharacteristicXList[4, LvChSelect[4, 1]].SetActive(false);
                Ch_selected[4].SetActive(true);
                Ch_selected[4].gameObject.transform.position = CharacteristicList[4, LvChSelect[4, 1]].gameObject.transform.position;
            }
            CharacteristicLvImage[4].GetComponent<Image>().sprite = Resources.Load("lv17" + "_활성", typeof(Sprite)) as Sprite;
            max_index = 4;
        }

        if (cur_lv >= 20)
        {
            if (LvChSelect[5, 0] == 0) for (int i = 0; i < 3; i++) CharacteristicXList[5, i].SetActive(false);
            if (LvChSelect[5, 0] == 1)
            {
                CharacteristicXList[5, LvChSelect[5, 1]].SetActive(false);
                Ch_selected[5].SetActive(true);
                Ch_selected[5].gameObject.transform.position = CharacteristicList[5, LvChSelect[5, 1]].gameObject.transform.position;
            }
            CharacteristicLvImage[5].GetComponent<Image>().sprite = Resources.Load("lv20" + "_활성", typeof(Sprite)) as Sprite;
            max_index = 5;
        }
    }

    //NPC 카드
    //void NPCCardRandomMix()
    //{
    //    for (int i = 0; i < 4; i++)
    //    {
    //        for (int j = 0; j < 2; j++)
    //        {
    //            int rank = Random.Range(0, 10);
    //            NPCCardProperty[i, j, 0] = rank;

    //            int kinds = Random.Range(0, 100);
    //            if (kinds < probability1) { NPCCardProperty[i, j, 1] = 0; };
    //            if (kinds < probability2 && kinds >= probability1) { NPCCardProperty[i, j, 1] = 1; };
    //            if (kinds < 100 && kinds >= probability2) { NPCCardProperty[i, j, 1] = 2; };

    //            if (i == 3 && j == 1) NPCCardProperty[i, j, 1] = 1;

    //            NPC_Card[i, j].GetComponent<Image>().sprite = Resources.Load("" + (NPCCardProperty[i, j, 0] + 1) + "-" + (NPCCardProperty[i, j, 1] + 1), typeof(Sprite)) as Sprite;
    //        }
    //    }
    //}

    //void NPCCardBoughtPos()
    //{
    //    if (npcCard1_index == 0 && npcCard2_index == 0) npcbought_index = 0;
    //    if (npcCard1_index == 0 && npcCard2_index == 1) npcbought_index = 1;
    //    if (npcCard1_index == 1 && npcCard2_index == 0) npcbought_index = 2;
    //    if (npcCard1_index == 1 && npcCard2_index == 1) npcbought_index = 3;
    //    if (npcCard1_index == 2 && npcCard2_index == 0) npcbought_index = 4;
    //    if (npcCard1_index == 2 && npcCard2_index == 1) npcbought_index = 5;
    //    if (npcCard1_index == 3 && npcCard2_index == 0) npcbought_index = 6;
    //    if (npcCard1_index == 3 && npcCard2_index == 1) npcbought_index = 7;
    //}

    //특성 설명
    void CharacteristicExplanation()
    {
        char ch = ' ';
        if (Ch_index == 0) ch = 'A';
        if (Ch_index == 1) ch = 'B';
        if (Ch_index == 2) ch = 'C';

        int lv = 1;
        if (Lv_index == 0) lv = 1;
        if (Lv_index == 1) lv = 3;
        if (Lv_index == 2) lv = 8;
        if (Lv_index == 3) lv = 13;
        if (Lv_index == 4) lv = 17;
        if (Lv_index == 5) lv = 20;

        explanation.GetComponent<Image>().sprite = Resources.Load("" + lv + ch, typeof(Sprite)) as Sprite;

        Explanation();
    }

    void Explanation()
    {
        if (Lv_index == 0 && Ch_index == 0)
        {
            explanationText.text = "적을 처치시 공격 능력이 증가합니다.";
        }
        if (Lv_index == 0 && Ch_index == 1)
        {
            explanationText.text = "적을 처치시 방어 능력이 증가합니다.";
        }
        if (Lv_index == 0 && Ch_index == 2)
        {
            explanationText.text = "적을 처치시 행운이 증가합니다.";
        }

        if (Lv_index == 1 && Ch_index == 0)
        {
            explanationText.text = "최대 회피 횟수가 증가하고 회피 이동거리가 감소합니다.";
        }
        if (Lv_index == 1 && Ch_index == 1)
        {
            explanationText.text = "카드의 효과가 증가합니다.";
        }
        if (Lv_index == 1 && Ch_index == 2)
        {
            explanationText.text = "회피에 무적능력이 추가됩니다.";
        }

        if (Lv_index == 2 && Ch_index == 0)
        {
            explanationText.text = "최대 점프 횟수가 증가합니다.";
        }
        if (Lv_index == 2 && Ch_index == 1)
        {
            explanationText.text = "점프의 높이가 증가합니다.";
        }
        if (Lv_index == 2 && Ch_index == 2)
        {
            explanationText.text = "공중에서 회피가 가능해 집니다.";
        }

        if (Lv_index == 3 && Ch_index == 0)
        {
            explanationText.text = "경험치 획득 시 골드를 추가로 얻습니다.";
        }
        if (Lv_index == 3 && Ch_index == 1)
        {
            explanationText.text = "케릭터의 스킬이 강화됩니다.";
        }
        if (Lv_index == 3 && Ch_index == 2)
        {
            explanationText.text = "몬스터 처치시 나오는 카드의 빈도가 증가합니다.";
        }

        if (Lv_index == 4 && Ch_index == 0)
        {
            explanationText.text = "공격이 강화 됩니다.";
        }
        if (Lv_index == 4 && Ch_index == 1)
        {
            explanationText.text = "마지막 공격시 스킬이 발동됩니다.";
        }
        if (Lv_index == 4 && Ch_index == 2)
        {
            explanationText.text = "스킬 데미지가 비약적으로 상승합니다.";
        }

        if (Lv_index == 5 && Ch_index == 0)
        {
            explanationText.text = "타격수가 2배 증가 합니다.";
        }
        if (Lv_index == 5 && Ch_index == 1)
        {
            explanationText.text = "소유 골드에 비레해 공격능력이 증가합니다.";
        }
        if (Lv_index == 5 && Ch_index == 2)
        {
            explanationText.text = "전체 능력이 상승합니다.";
        }
    }

    //환경설정 저장화면
    void SaveWindowOpenAndClose()
    {
        if (Saving) SaveWindowTime += Time.deltaTime;
        if (SaveWindowTime > 1.5)
        {
            GameSetSaveWindow.SetActive(false);
            Saving = false;
        }
    }

    //튜토리얼
    void Tutorial()
    {
        if (!OnGameTutorial) return;

        if (!GameStartInfo.enabled)
            OnTutorialTime -= Time.deltaTime;

        if (OnTutorialTime == 10.0f) GameObject.Find("player").GetComponent<playermovement>().KeyControl = false;

        if ((int)OnTutorialTime <= 0)
        {
            GameObject.Find("player").GetComponent<playermovement>().KeyControl = false;
            SystemInfo_seq++;

            GameTutorialBackGround.SetActive(true);
            GameStartInfo.enabled = true;
            GameStartInfo.color = Color.white;
            GameStartInfo.text = "조작법을 어느 정도 확인하셨나요?\n지금부터는 상점 시스템에 대해 알려드리겠습니다.";
            OnTutorialTime = 10.0f;
        }
    }

    void InsertExplanation()
    {
        if (Lv_index == 0)
        {
            if (Ch_index == 0) GameObject.Find("player").GetComponent<characteristic_Manager>().firststic = true;
            if (Ch_index == 1) GameObject.Find("player").GetComponent<characteristic_Manager>().secondstic = true;
            if (Ch_index == 2) GameObject.Find("player").GetComponent<characteristic_Manager>().thirdstic = true;
        }
        else if (Lv_index == 1)
        {
            if (Ch_index == 0) GameObject.Find("player").GetComponent<characteristic_Manager>().b_a();
            if (Ch_index == 1) GameObject.Find("player").GetComponent<characteristic_Manager>().b_b();
            if (Ch_index == 2) GameObject.Find("player").GetComponent<characteristic_Manager>().b_c();
        }
        else if (Lv_index == 2)
        {
            if (Ch_index == 0) GameObject.Find("player").GetComponent<characteristic_Manager>().c_a();
            if (Ch_index == 1) GameObject.Find("player").GetComponent<characteristic_Manager>().c_b();
            if (Ch_index == 2) GameObject.Find("player").GetComponent<characteristic_Manager>().c_c();
        }
        else if (Lv_index == 3)
        {
            if (Ch_index == 0) GameObject.Find("player").GetComponent<characteristic_Manager>().d_a();
            if (Ch_index == 1) GameObject.Find("player").GetComponent<characteristic_Manager>().d_b();
            if (Ch_index == 2) GameObject.Find("player").GetComponent<characteristic_Manager>().d_c();
        }
        else if (Lv_index == 4)
        {
            if (Ch_index == 0) GameObject.Find("player").GetComponent<characteristic_Manager>().e_a();
            if (Ch_index == 1) GameObject.Find("player").GetComponent<characteristic_Manager>().e_b();
            if (Ch_index == 2) GameObject.Find("player").GetComponent<characteristic_Manager>().e_c();
        }
        else if (Lv_index == 5)
        {
            if (Ch_index == 0) GameObject.Find("player").GetComponent<characteristic_Manager>().f_a();
            if (Ch_index == 1) GameObject.Find("player").GetComponent<characteristic_Manager>().f_b();
            if (Ch_index == 2) GameObject.Find("player").GetComponent<characteristic_Manager>().f_c();
        }
    }
}
