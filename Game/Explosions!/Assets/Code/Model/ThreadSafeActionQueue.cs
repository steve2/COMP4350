using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//TODO: Namespace
namespace Assets.Code.Model
{
    /// <summary>
    /// A thread safe action queue
    /// </summary>
    public class ThreadSafeActionQueue : Queue<Action>
    {
        // Use this for initialization
        public ThreadSafeActionQueue()
        {
        }

        /// <summary>
        /// Used to execute code that must be run on the main thread from a different thread
        /// </summary>
        /// <param name="task">The code to execution on main thread</param>
        public void InvokeOnMainThread(Action task)
        {
            lock (this)
            {
                Enqueue(task);
            }
        }

        /// <summary>
        /// Checks every frame if there are any tasks to perform on the main thread
        /// </summary>
        public virtual void Update()
        {
            Action task;
            while (Count > 0)
            {
                lock (this)
                {
                    task = Dequeue();
                    task();
                }
            }
        }
    }
}
