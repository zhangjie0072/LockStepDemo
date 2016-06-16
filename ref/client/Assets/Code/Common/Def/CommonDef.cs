using UnityEngine;
using System.Collections;
using System.IO;


//特殊物品ID定义
public enum PropertyType
{
    DIAMOND_ID = 1, //钻石
    GOLD_ID = 2, //金币
    HONOR_ID = 3, //荣誉
    HP_ID = 4, //体力
    TEAM_EXP_ID = 5, //球队经验
    ROLE_EXP_ID = 6, //球员经验
}

////球员品质定义
//public enum RoleQuality : byte
//{
//    NONE = 0,
//    D_MINUS = 1,
//    D = 2,
//    D_PLUS = 3,
//    C_MINUS = 4,
//    C = 5,
//    C_PLUS = 6,
//    B_MINUS = 7,
//    B = 8,
//    B_PLUS = 9,
//    A_MINUS = 10,
//    A = 11,
//    A_PLUS = 12,
//    S_MINUS = 13,
//    S = 14,
//    S_PLUS = 15,
//}

////球员职业
//public enum PositionType : byte
//{
//    NONE = 0,
//    PF = 1, //大前锋
//    SF = 2, //小前锋
//    C = 3, //中锋
//    PG = 4, //控卫
//    SG = 5, //分卫
//}

//属性类型
public enum AttributeType : byte
{
    NONE = 0,
    BASIC = 1, //基础属性
    HEDGING = 2, //对冲属性
    HEDGINGLEVEL = 3, //对冲等级
    OTHER = 10, //其它属性
}

public delegate void DelegateLoadComplete(string path, object obj);