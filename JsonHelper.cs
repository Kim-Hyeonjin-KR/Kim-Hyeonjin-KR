﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----- EXAMPLE -----
// SAVE 
// string json = JsonUtility.ToJson(player_data,true);
// File.WriteAllText(Application.persistentDataPath + "/player_data.json", json);
//
// LOAD -> 
// string save_string = File.ReadAllText(Application.persistentDataPath + "/player_data.json");
// player_data = JsonUtility.FromJson<Player_data>(save_string);

//  JSON PARSER
//   배열 형식
[System.Serializable]
public class JsonHelper
{
    [System.Serializable]
    private class Wrapper<T>
    {   
        public T[] items_arr;
    }
    public static T[] FromJson<T>(string _json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(_json);
        return wrapper.items_arr;
    }

    public static string ToJson<T>(T[] _array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items_arr = _array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] _array, bool _prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items_arr = _array;
        return JsonUtility.ToJson(wrapper, _prettyPrint);
    }
}