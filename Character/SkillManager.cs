using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IskillFunctions
{
    // 스킬 함수 호출
    void ExecuteSkill(Vector3 _currentMouseDirection);

    // 스킬 쿨타임
    IEnumerator IeSkillRecovery();
}

public class SkillManager : SingletonGlobal<SkillManager>,IskillFunctions
{
    // ------- 컴포넌트 -------

    // 파티클
    protected ParticleSystem mCurrentSkill;
    // 리지드바디
    protected Rigidbody mCurrentRigidbody;
    // 콜라이더
    protected Collider mCurrentCollider;

    // ------- 스킬 관련 -------

    // 스킬 쿨타임, 속도, 공격력, 범위, 효과 설정
    // 스킬 이펙트, 이미지 설정

    // 마우스 방향
    protected Vector3 mCurrentMouseDirection;
    // 스킬 속도
    [SerializeField]
    private float mSkillSpeed = 0f;
    // 쿨타임
    [SerializeField]
    private float mCoolTime   = 0f;
    // 스킬이 준비됨
    protected bool mIsSkillReady = true;

    [ContextMenu("스킬 쿨타임과 속도 설정")]
    public void TestSkillValues()
    {
        Debug.Log(gameObject);
        Debug.LogFormat("속도 : {0}",mSkillSpeed);
        Debug.LogFormat("쿨타임 : {0}", mCoolTime);
    }

    // 임시 오브젝트
    GameObject obj;


    // 스킬 함수 호출
    public void ExecuteSkill(Vector3 _currentMouseDirection)
    {
        if (mIsSkillReady) // 스킬 움직임
        {
            // 스킬 생성 파트

            // 조건에 맞는 캐릭터의 스킬 프리팹 위치 확인

            // 확인된 위치의 스킬 프리팹 가져오기 (프리팹의 위치를 찾아서 자식 오브젝트를 꺼내 쓰는 방법
                    //GameObject particle_prefaps = Instantiate(Resources.Load("/Skill/Character1_Skill_Prefaps")) as GameObject;
                    //
                    //GameObject particle_child = particle_prefaps.transform.GetChild(0).gameObject;  // 0 대신 위치 프리팹의 자식으로 가져오기
                    //
                    //mCurrentSkill = particle_child.GetComponent<ParticleSystem>(); // 
                    //
                    //Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);
                    //
                    //obj.AddComponent<ParticleSystem>();

            // ExecuteSkill이전에. 즉 스킬 컨테이너를 만들기 전에 미리 처리해야하는 내용. 아래 내용은 적절한 위치로 옮길것.
            // 확인된 위치의 스킬 프리팹 자체를 자식 오브젝트로 추가하는 방법
            GameObject particle_child = Instantiate(Resources.Load("/Skill/Character1_Skill_Prefaps")) as GameObject;
            particle_child.transform.parent = 부모의 위치;

            1. 오브젝트 풀링으로 스킬을 복사해야한다. 이 때 복사할 스킬에 미리 파티클이 포함시켜야 하는가...??? 아니면 복사할 때 마다 파티클을 따로 가져와야 하는가
            particle_child에서 스킬 번호와 같은 파티클을 연결 ?
            

            // 오브젝트 풀링 사용


            // 

            mCurrentRigidbody.AddForce(_currentMouseDirection * mSkillSpeed * Time.deltaTime, ForceMode.Force);
        }
    }

    // 스킬 쿨타임
    public IEnumerator IeSkillRecovery()
    {
        yield return new WaitForSeconds(mCoolTime);
        mIsSkillReady = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌 시 코루틴 실행
        if (collision.gameObject.tag == "Player")
        {
            mIsSkillReady = false;  // 스킬 쿨타임 시작. 여기 말고 키 입력시 쿨타임 시작으로 위치 변경해야함.
            StartCoroutine(IeSkillRecovery());
        }
        // 스킬,몹,엔피씨,오브젝트,필드
    }
}