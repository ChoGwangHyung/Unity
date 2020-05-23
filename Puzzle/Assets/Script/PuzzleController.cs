using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuzzleController : MonoBehaviour
{
    //block prefab
    public Sprite block0;
    public Sprite block1;
    public Sprite block2;
    public Sprite default_block;

    //block gameobject array
    public GameObject[,] post_block_array = new GameObject[3, 3];
    private Sprite[,] front_block_array = new Sprite[3, 3];
    //block click check
    private bool[,] click_block = new bool[3, 3];
    //block rotation y value
    private float[,] rotation_y = new float[3, 3];
    //check of click correct block num
    private Sprite check_block_gameObject;
    private int[,] check_block_array_num = new int[3, 2];
    private int check_block;

    //score
    private int score;
    public Text countText;

    //time
    private float time;
    public Text TimeText;

    //particle prefab
    public GameObject particle_success;

    //clear
    private float clear_time;

    //start
    private bool start;
    private bool do_rotate;

    void Start()
    {
        //initialize
        int[] block_kind = new int[3];
        for (int i = 0; i < 3; i++)
            block_kind[i] = 0;

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                click_block[x, y] = false;
                rotation_y[x, y] = 0.0f;

                post_block_array[x, y] = GameObject.Find((x + 1) + "-" + (y + 1));

                //insert block to random     
                int block_kind_num = 0;
                while (true)
                {
                    block_kind_num = Random.Range(0, 3);

                    if (block_kind[block_kind_num] < 3)
                    {
                        block_kind[block_kind_num]++;
                        break;
                    }
                }

                switch (block_kind_num)
                {
                    case 0:
                        front_block_array[x, y] = block0;
                        break;
                    case 1:
                        front_block_array[x, y] = block1;
                        break;
                    case 2:
                        front_block_array[x, y] = block2;
                        break;
                }
            }
        }

        check_block = 0;
        score = 0;
        time = 30.0f;
        clear_time = 3.0f;
        start = false;
        do_rotate = false;
    }

    void Update()
    {
        ShowBlock();
        if (!start) return;

        Collider2D clickColl = null;
        Vector3 worldPos;
        Vector2 clickPos;

        //mouse event
        if (Input.GetMouseButtonDown(0))
        {
            worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPos = new Vector2(worldPos.x, worldPos.y);
            clickColl = Physics2D.OverlapPoint(clickPos);

            RaycastHit2D hit = Physics2D.Raycast(worldPos, transform.forward, 100);
            Debug.DrawRay(worldPos, transform.forward * 10, Color.red, 0.3f);

            if (hit)
            {
                ClickBlock(hit.transform.gameObject);
                Instantiate(particle_success, new Vector2(clickPos.x, clickPos.y), Quaternion.identity);
            }

            try
            {
                if (clickColl.gameObject != null)
                {
                    Debug.Log("click object");
                }
                else
                {
                    Debug.Log("no object!");
                }
            }
            catch (System.NullReferenceException ex)
            {
                Debug.Log(ex);
            }
        }

        if (CheckClickedBlock())
        {
            clear_time -= Time.deltaTime;

            if (clear_time <= 0.0f)
                Clear();

            return;
        }

        TimeCheck();
    }

    void ClickBlock(GameObject block)
    {
        if (block == null) return;

        //find block x, y
        int block_x = 0;
        int block_y = 0;

        for (int x = 0; x < 3; x++)
        {
            bool find = false;
            for (int y = 0; y < 3; y++)
            {
                if (block == post_block_array[x, y])
                {
                    find = true;
                    block_x = x;
                    block_y = y;
                    break;
                }
            }

            if (find) break;
        }

        //if block clicked return
        if (click_block[block_x, block_y]) return;

        if (check_block == 0)
        {
            if (front_block_array[block_x, block_y] == block0)
                check_block_gameObject = block0;
            else if (front_block_array[block_x, block_y] == block1)
                check_block_gameObject = block1;
            else if (front_block_array[block_x, block_y] == block2)
                check_block_gameObject = block2;
        }
        else
        {
            //if click block not correct
            if (front_block_array[block_x, block_y] != check_block_gameObject)
            {
                for (int i = 0; i < check_block; i++)
                {
                    click_block[check_block_array_num[i, 0], check_block_array_num[i, 1]] = false;
                }

                check_block = 0;
                return;
            }
        }

        check_block_array_num[check_block, 0] = block_x;
        check_block_array_num[check_block, 1] = block_y;
        check_block++;
        click_block[block_x, block_y] = true;

        if (check_block == 3)
        {
            score += 100;
            countText.text = "Count : " + score.ToString();
            for (int i = 0; i < 3; i++)
            {
                check_block_array_num[i, 0] = -1;
                check_block_array_num[i, 1] = -1;
            }
            check_block = 0;
        }
    }

    bool CheckClickedBlock()
    {
        bool clear = true;
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (click_block[x, y])
                    RotateBlock(x, y);
                else
                {
                    ReturnRotateBlock(x, y);
                    clear = false;
                }
            }
        }

        return clear;
    }

    void RotateBlock(int x, int y)
    {
        if (rotation_y[x, y] >= 180.0f) return;

        rotation_y[x, y] += 2;

        if (rotation_y[x, y] >= 90.0f)
            post_block_array[x, y].GetComponent<SpriteRenderer>().sprite = front_block_array[x, y];

        if (rotation_y[x, y] >= 180.0f)
            rotation_y[x, y] = 180.0f;

        post_block_array[x, y].transform.rotation = Quaternion.Euler(0, rotation_y[x, y], 0);
    }

    void ReturnRotateBlock(int x, int y)
    {
        if (rotation_y[x, y] <= 0.0f) return;

        rotation_y[x, y] -= 2;

        if (rotation_y[x, y] <= 90.0f)
            post_block_array[x, y].GetComponent<SpriteRenderer>().sprite = default_block;

        if (rotation_y[x, y] <= 0.0f)
        {
            rotation_y[x, y] = 0.0f;
            click_block[x, y] = false;
        }

        post_block_array[x, y].transform.rotation = Quaternion.Euler(0, rotation_y[x, y], 0);
    }

    void Clear()
    {
        SceneManager.LoadScene("Clear");
    }

    void TimeCheck()
    {
        if (time >= 1)
        {
            time -= Time.deltaTime;
            TimeText.text = "Time : " + time.ToString("N1");
        }
        else
        {
            SceneManager.LoadScene("End");
        }
    }

    void ShowBlock()
    {
        if (start) return;

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (!do_rotate)
                {
                    RotateBlock(x, y);
                    if (rotation_y[x, y] >= 180.0f)
                        do_rotate = true;
                }
                else
                {
                    ReturnRotateBlock(x, y);
                    if (rotation_y[x, y] <= 0.0f)
                        start = true;
                }
            }
        }
    }
}
