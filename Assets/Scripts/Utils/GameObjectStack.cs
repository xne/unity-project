using System.Collections.Generic;
using UnityEngine;

public class GameObjectStack
{
    private readonly Stack<GameObject> stack = new();

    public int Count => stack.Count;

    public void Clear()
    {
        while (stack.Count > 0)
        {
            var prev = stack.Pop();
            if (prev)
                prev.SetActive(false);
        }
    }

    public bool Contains(GameObject go) => stack.Contains(go);

    public GameObject Peek()
    {
        if (stack.Count == 0)
            return null;

        return stack.Peek();
    }

    public GameObject Pop()
    {
        if (stack.Count == 0)
            return null;

        var prev = stack.Pop();
        if (prev)
            prev.SetActive(false);

        if (TryPeek(out var go))
            go.SetActive(true);

        return prev;
    }

    public void Push(GameObject go)
    {
        if (go == null)
            return;

        if (TryPeek(out var prev))
            prev.SetActive(false);

        go.SetActive(true);
        stack.Push(go);
    }

    public bool TryPeek(out GameObject go) => stack.TryPeek(out go);

    public bool TryPop(out GameObject prev)
    {
        if (stack.Count == 0)
        {
            prev = null;
            return false;
        }

        prev = stack.Pop();
        if (prev)
            prev.SetActive(false);

        if (TryPeek(out var go))
            go.SetActive(true);

        return true;
    }
}
