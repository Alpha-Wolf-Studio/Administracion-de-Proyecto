using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
public class Utils
{
    public static bool LayerEquals(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}

public static class ListExtensions 
{
    public static void RemoveAllIndices<T>(this List<T> list, IEnumerable<int> indices)
    {
        //do not remove Distinct() call here, it's important
        var indicesOrdered = indices.Distinct().ToArray();
        if(indicesOrdered.Length == 0)
            return;

        Array.Sort(indicesOrdered);

        if (indicesOrdered[0] < 0 || indicesOrdered[indicesOrdered.Length - 1] >= list.Count)
            throw new ArgumentOutOfRangeException();

        int indexToRemove = 0;
        int newIdx = 0;

        for (int originalIdx = 0; originalIdx < list.Count; originalIdx++)
        {
            if(indexToRemove < indicesOrdered.Length && indicesOrdered[indexToRemove] == originalIdx)
            {
                indexToRemove++;
            }
            else
            {
                list[newIdx++] = list[originalIdx];
            }
        }

        list.RemoveRange(newIdx, list.Count - newIdx);
    }
}
