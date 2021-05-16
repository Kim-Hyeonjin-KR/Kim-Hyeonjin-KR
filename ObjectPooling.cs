using System;
using System.Collections.Generic;
using UnityEngine;

// ------ EXAMPLE ------

// 플레이어 총알 관련
//[Header("플레이어 무기")]
//public Player_bullet_pooling player_bullet_pooling;
//public Player_bullet_pooling_data player_bullet_pooling_data = new Player_bullet_pooling_data();

//[Serializable]
//public class Player_bullet_pooling_data // 플레이어 총알 클래스
//{
//    // 플레이어 총알 관련
//    [Header("플레이어 총알")]
//    public int max_bullet_count = 100;
//    public List<GameObject> player_bullet_obj_list = new List<GameObject>();
//    public GameObject player_bullet_prefab;
//    public Transform player_bullet_container;
//}

// START
// player_power_up_pooling = GetComponent<Player_power_up_pooling>();

// 로컬 싱글톤 사용 -> 전역 싱글톤 사용 시 문제

// 스킬 컨테이너

public class ObjectPooling : SingletonLocal<ObjectPooling>
{
    Dictionary<Transform, Dictionary<eSkillType, SkillManager[]>> skill_container = new Dictionary<Transform, Dictionary<eSkillType, SkillManager[]>>();


    private void Start()
    {
        
    }
}