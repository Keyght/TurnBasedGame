using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerOfCharacters
{
    public static void FillCharManager()
    {
        var allPrefs = Resources.LoadAll<GameObject>("Characters");
        foreach (var pref in allPrefs)
        {
            CharManager.AllChars.Add(pref);
        }
    }
}

public static class CharManager
{
    public static List<GameObject> AllChars = new List<GameObject>();
}