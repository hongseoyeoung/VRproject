using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    public bool mIsActive { set; get; }

    //생성할 상자의 수
    public int mCount;
    //곡률
    public float mCurveValue;
    //중력
    public float mGravity;
    public Vector3 mVelocity;
    public Vector3 mGroundPos;
    //리스트 생성
    public List<Transform> mRenderList = new List<Transform>();

    void Start()
    {
        CreateRender();
    }

    //박스 생성
    void CreateRender()
    {
        for (int i = 0; i < mCount; i++)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //Raycast도 받지 않음
            obj.layer = LayerMask.NameToLayer("Ignore Raycast");
            //상자의 부모를 컨트롤러로 지정
            obj.transform.parent = transform;
            //크기
            obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            //색은 초록색
            obj.GetComponent<MeshRenderer>().material.color = Color.green;
            //콜라이더 제거
            Destroy(obj.GetComponent<BoxCollider>());

            //리스트에 상자 추가
            mRenderList.Add(obj.transform);
            //비활성화
            mRenderList[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (mIsActive == true)
            //상자 보여주기
            ShowPath();
        else
            //상자 안보이게
            hidePath();
    }

    //상자 비활성화
    void hidePath()
    {
        for (int i = 0; i < mCount; i++)
        {
            if (mRenderList[i].gameObject.activeSelf == false) continue;

            mRenderList[i].gameObject.SetActive(false);
        }
    }

    //포물선을 그려서 보여주는 함수
    void ShowPath()
    {
        if (mRenderList.Count == 0)
            CreateRender();

        Vector3 pos = transform.position;
        Vector3 g = new Vector3(0, mGravity, 0);
        mVelocity = transform.forward * mCurveValue;

        for (int i = 0; i < mCount; i++)
        {
            float t = i * 0.001f;

            pos = pos + (mVelocity * t) + (0.5f * g * t * t);
            mVelocity += g;
            //위치 지정
            mRenderList[i].position = pos;
            //상자 활성화
            mRenderList[i].gameObject.SetActive(true);
        }
        checkGround();
    }

    //땅에 가장 근접한 상자의 바닥으로 컨트롤러 위치 변경
    //각각의 상자마다 바닥으로 Raycast를 쏴서 가장 거리가 가까운 
    //상자의 바닥위치로 이동함
    void checkGround()
    {
        int closeIdx = 0;
        float dist = 100;
        RaycastHit hit;
        mGroundPos = Vector3.zero;

        for (int i = 0; i < mCount; i++)
        {
            if (mRenderList[i].gameObject.activeSelf == false)
                continue;

            //각 상자의 위치에서 바닥으로 Raycast 발사
            if (Physics.Raycast(mRenderList[i].position, Vector3.down, out hit, Mathf.Infinity))
            {
                //Raycast가 Layer이름이 Ground인것에 충돌시 
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground") == false)
                    continue;

                float curDist = Vector3.Distance(mRenderList[i].position, hit.point);

                if (dist < curDist)
                    continue;
                //바닥에 가장 가까운 상자의 인덱스
                closeIdx = i;
                //바닥에 가장 가까운 상자의 포지션 저장
                mGroundPos = hit.point;
            }
        }

        //바닥에 가장 가까운 상자부터 안보이도록
        //상태 비활성화
        for (int i = closeIdx; i < mCount; i++)
        {
            mRenderList[i].gameObject.SetActive(false);
        }
    }
}
