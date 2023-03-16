using AccountChecker.Models;
using System;
using System.Collections.Generic;

namespace AccountChecker;

// AccountQueue class represents a static class used for handling a queue of AccountCombo objects.

public static class AccountQueue
{
    // Queue to store AccountCombo objects.
    private static Queue<AccountCombo> _queue = new();

    // Enqueues an AccountCombo object.
    public static void Enqueue(AccountCombo account)
    {
        lock (_queue)
        {
            _queue.Enqueue(account);
        }
    }

    // Dequeues an AccountCombo object.
    public static AccountCombo Dequeue()
    {
        lock (_queue)
        {
            return _queue.Dequeue();
        }
    }

    // Checks if the queue is empty.
    public static bool IsEmpty()
    {
        lock (_queue)
        {
            return _queue.Count == 0;
        }
    }

    // Returns the number of elements in the queue.
    public static int Count()
    {
        lock (_queue)
        {
            return _queue.Count;
        }
    }
}
