using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----- enum 전용 -----

public enum eCharacterMoveType
{
    FORWARD,
    LEFT,
    BACKWARD,
    RIGHT,
    JUMP,
    MAX
};
public enum eSkillType
{
    FIRST,
    SECOND,
    THIRD,
    FOURTH,
    FIFTH,
    SIXTH,
    LAST,       // 스킬 총 7개 만들고 탈것은 스킬에서 제외
    MAX         // for(int i=0;i<(int)eSkillType.Max;i++)
}