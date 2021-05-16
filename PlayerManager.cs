using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


[System.Serializable]
public class PlayerKey
{
    [Header("계속 누르는 키를 입력받는 KeyCode")]
    public KeyCode continuousKey;

    // 한번 누르는 키를 입력받는 KeyCode

    // 눌렀다가 뗐을 시 입력받는 KeyCode
}


[System.Serializable]
public class PlayerInfo
{
    public float hp = 25f;
    public float mp;

    public string name;
    public int gold;

    public bool isDead = false;

    public float speed = 4f;

}

public class PlayerManager : SingletonGlobal<PlayerManager>
{
    // ------- 키 입력 -------
    [Header("플레이어 키 클래스")]
    public PlayerKey playerKey;

    // 연속 키 입력 함수 컨테이너
    public Dictionary<KeyCode, Action> playerContinuousKeyDictionary = new Dictionary<KeyCode, Action>();

    // 부모 오브젝트
    public Transform firstSkillContainer;

    // 플레이어 정보
    public PlayerInfo playerInfo;

    // 플레이어 오브젝트
    public Transform playerTransform;

    // 카메라 컴포넌트
    [SerializeField]
    CinemachineVirtualCamera mVirtualCamera;
    // 카메라 축 오프셋
    Vector3 cameraOffset;

    // 스킬 오브젝트

    // 스킬 타입
    public eSkillType skillType;


    private void Awake()
    {
        cameraOffset = mVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset;
        InitPlayerContinuousKeyDictionary();
    }

    private void Start()
    {
        StartCoroutine(LoseHp());
    }

    private void Update()
    {
        if (playerInfo.hp <= 0)
        {
            playerInfo.isDead = true;
            StopCoroutine(LoseHp());
        }

        if (!playerInfo.isDead)
        {
            CheckIfPlayerContinuousKey();
            Look_at_mouse_position();
        }
    }

    IEnumerator LoseHp()        // 체력 감소 실험용
    {
        while (playerInfo.hp > 0f)
        {
            playerInfo.hp -= 5f;
            yield return new WaitForSeconds(1f);
        }
    }

    // 계속 입력받는 키 컨테이너 초기화
    void InitPlayerContinuousKeyDictionary()
    {
        playerContinuousKeyDictionary[KeyCode.UpArrow] = PlayerMovedUp;
        playerContinuousKeyDictionary[KeyCode.DownArrow] = PlayerMovedDown;
        playerContinuousKeyDictionary[KeyCode.LeftArrow] = PlayerMovedLeft;
        playerContinuousKeyDictionary[KeyCode.RightArrow] = PlayerMovedRight;
    }

    // 키를 입력했는지 체크
    void CheckIfPlayerContinuousKey()
    {
        foreach (var item in playerContinuousKeyDictionary)
        {
            if (Input.GetKey(item.Key))
            {
                playerKey.continuousKey = item.Key;

                if (playerContinuousKeyDictionary.ContainsKey(playerKey.continuousKey))     // 함수 있는지 확인
                    playerContinuousKeyDictionary[playerKey.continuousKey].Invoke();        // 강제 실행
            }
        }
    }

    // 마우스 방향으로 플레이어 회전
    void Look_at_mouse_position()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groupPlane = new Plane(Vector3.up, cameraOffset);
        float rayLength;

        if (groupPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            playerTransform.LookAt(new Vector3(pointToLook.x, playerTransform.position.y, pointToLook.z));
        }
    }

    // 플레이어가 위로 갔음
    public void PlayerMovedUp()
    {
        Debug.Log("위로 움직임");
        playerTransform.position += Vector3.forward * playerInfo.speed * Time.deltaTime;
    }

    // 플레이어가 아래로 갔음
    public void PlayerMovedDown()
    {
        Debug.Log("아래로 움직임");
        playerTransform.position += Vector3.back * playerInfo.speed * Time.deltaTime;
    }

    // 플레이어가 왼쪽으로 갔음
    public void PlayerMovedLeft()
    {
        Debug.Log("왼쪽으로 움직임");
        playerTransform.position += Vector3.left * playerInfo.speed * Time.deltaTime;
    }

    // 플레이어가 오른쪽으로 갔음
    public void PlayerMovedRight()
    {
        Debug.Log("오른쪽으로 움직임");
        playerTransform.position += Vector3.right * playerInfo.speed * Time.deltaTime;
    }

}
