using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Code.Components
{
    /// <summary>
    /// All components that need access to the Game service should inherit from this
    /// </summary>
    public class GameComponent : MonoBehaviour
    {
        Queue<Action> tasks;
        private Controller.Game gameInstance;
        public Controller.Game Game { get { return gameInstance; } }

        // Use this for initialization
        public virtual void Start()
        {
            tasks = new Queue<Action>();
            this.gameInstance = Controller.Game.Instance;
        }

        /// <summary>
        /// Used to execute code that must be run on the main thread from a different thread
        /// </summary>
        /// <param name="task">The code to execution on main thread</param>
        protected void InvokeOnMainThread(Action task)
        {
            lock (tasks)
            {
                tasks.Enqueue(task);
            }
        }

        /// <summary>
        /// Checks every frame if there are any tasks to perform on the main thread
        /// </summary>
        public virtual void Update()
        {
            Action task;
            while (tasks.Count > 0)
            {
                lock (tasks)
                {
                    task = tasks.Dequeue();
                    task();
                }
            }
        }
    }
}