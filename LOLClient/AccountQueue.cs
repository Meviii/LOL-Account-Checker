using AccountChecker.Models;
using System;
using System.Collections.Generic;

namespace AccountChecker;

public static class AccountQueue
{
    private static Queue<AccountCombo> _queue = new();

    public static void Enqueue(AccountCombo account)
    {
        lock (_queue)
        {
            _queue.Enqueue(account);
        }
    }

    public static AccountCombo Dequeue()
    {
        lock (_queue)
        {
            return _queue.Dequeue();
        }
    }

    public static bool IsEmpty()
    {
        lock (_queue)
        {
            return _queue.Count == 0;
        }
    }

    public static int Count()
    {
        lock (_queue)
        {
            return _queue.Count;
        }
    }
}
