using System;
using System.Collections.Generic;

namespace AccountChecker;

public class Queue
{
    private List<Tuple<string,string>> _comboList;

    public Queue() { }

    public Queue(List<Tuple<string, string>> comboList)
    {
        _comboList = comboList;
    }

    public void Add(Tuple<string, string> combo)
    {
        _comboList.Add(combo);
    }

    public void RemoveFirst()
    {
        if (_comboList.Count > 0)
            _comboList.RemoveAt(0);
    }

    public void Clear()
    {
        _comboList.Clear();
    }

    public Tuple<string, string> Pop()
    {
        if (_comboList.Count == 0) return null;

        var next = _comboList[0];
        _comboList.RemoveAt(0);

        return next;
    }

}
