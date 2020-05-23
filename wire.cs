using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wire : MonoBehaviour
{
    public Camera mainCam;

    LineRenderer lr;
    public GameObject wireStorage;
    bool isWireShoot;
    bool doWireBack;
    Vector3 wire_dir;
    Rigidbody r;



    float wireDistance;

    KeyCode shoot_wire_key;
    int spacebar_rotation;

    bool isLineRender;

    public static Vector3 rayHitPoint;

    float grabTime;
    bool hitWall;
    public static bool isGrabWall;

    void Start()
    {
        r = GetComponent<Rigidbody>();
        //오른쪽 왼쪽 판별해서 키코드 받아오기
        if (this.gameObject.name == "LeftWire")
        {
            shoot_wire_key = KeyCode.Q;
            spacebar_rotation = -45;
        }
        else
        {
            shoot_wire_key = KeyCode.E;
            spacebar_rotation = 45;
        }

        //라인 랜더러 초기화
        lr = wireStorage.GetComponent<LineRenderer>();
        lr.SetWidth(0.1f, 0.1f);
        //lr.SetColors(Color.green, Color.green);                    
        lr.enabled = false;


        isWireShoot = false;
        doWireBack = false;
        isLineRender = false;


        wireDistance = 20.0f;

        grabTime = 0.0f;
        hitWall = false;
        isGrabWall = false;
    }

    void Update()
    {
        //와이어 오브젝트 hit 판정
        CheckWireHit();

        if (!doWireBack)
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    //wire 쏘는 방향
            //    if (hitWall)
            //        wire_dir = mainCam.transform.position + (mainCam.transform.forward * Vector3.Distance(mainCam.transform.position, rayHitPoint));
            //    else
            //        wire_dir = mainCam.transform.position + (mainCam.transform.forward * wireDistance);

            //    isWireShoot = true;
            //    doWireBack = false;
            //}
            if (Input.GetKeyDown(shoot_wire_key) && !isGrabWall)
            {
                r.velocity = Vector3.zero;
                this.transform.position = wireStorage.transform.position;
                //wire 쏘는 방향
                if (hitWall)
                    wire_dir = rayHitPoint - wireStorage.transform.position;
                else
                    wire_dir = mainCam.transform.position + (mainCam.transform.forward * wireDistance) - wireStorage.transform.position;

                isLineRender = true;
                isWireShoot = true;
                doWireBack = false;
            }
        }
        ShootWire();
        GrabWall();
        BackWire();

        LineRender(wireStorage,this.gameObject);
        lr.enabled = isLineRender;

    }
    
    //와이어 쏘는 함수
    void ShootWire()
    {
        // 와이어 박혔을 때 잡고 있는 시간 측정
        if (!isWireShoot) return;
        
        //와이어 이동
        r.AddForce(wire_dir * 500.0f);
        isWireShoot = false;
        //와이어 목표지점 도착
    }

    //와이어 돌아오는 함수
    void BackWire()
    {
        if (!doWireBack) return;


        r.velocity = Vector3.zero;

        this.transform.position = wireStorage.transform.position;
        grabTime = 0.0f;
        isGrabWall = false;
        isLineRender = false;
        doWireBack = false;
    }

    //라인 그리는 함수
    void LineRender(GameObject start, GameObject end)
    {
        if (!isLineRender) return;
        lr.SetPosition(0, start.transform.position);
        lr.SetPosition(1, end.transform.position);

        if (Vector3.Distance(this.transform.position, wireStorage.transform.position) >= wireDistance - 0.5f)
        {
            doWireBack = true;
        }
    }
    //와이어 쐈을 때 wall에 맞았는지 안 맞았는지 판별 함수
    void CheckWireHit()
    {
        hitWall = false;

        Ray ray;
        RaycastHit rayHit;

        //메인카메라가 보는 중앙으로 ray 쏘기
        ray = mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

        if (Physics.Raycast(ray, out rayHit, wireDistance))
        {
            //쏜 ray가 'wall' tag를 가진 오브젝트에 맞았을 때
            if (rayHit.transform.gameObject.tag == "wall")
            {
                rayHitPoint = rayHit.point;
                hitWall = true;
            }
            Debug.DrawRay(ray.origin, ray.direction * wireDistance, Color.black);
        }
    }

    void GrabWall()
    {
        if (!isGrabWall) return;

        if (Input.GetKey(shoot_wire_key))
            grabTime = 2.0f;

        Debug.Log("GrabTime : " + grabTime);
        grabTime -= Time.deltaTime;
        if (grabTime < 0.0f)
        {
            Debug.Log("GrabTime 00000000000000000000");
            isGrabWall = false;
            doWireBack = true;
        }

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (doWireBack) return;
        if (collision.gameObject.tag == "wall")
        {
            r.velocity = Vector3.zero;
            this.transform.position = rayHitPoint;
            grabTime = 2.0f;
            isGrabWall = true;
        }
    }
}

