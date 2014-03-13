using UnityEngine;
using System;
using System.Collections.Generic;


namespace Assets.Code.Components.Actions
{
    public class GameActionManager : MonoBehaviour, IEnumerable<string>
    {
        private Dictionary<string, GameAction> actions;
        
        public int Count { get { return actions.Count; } }

        public void Awake()
        {
            actions = new Dictionary<string, GameAction>();
        }

        public void Start()
        {
            var myActions = GetComponents<GameAction>(); //Get our own actions
	        foreach (GameAction action in myActions)
	        {
	            AddAction(action);
	        }
        }

        //TODO: How will we group actions?
        public bool Perform(string name)
        {
            bool success = false;
            GameAction action;
            if (actions.TryGetValue(name, out action))
            {
                success = action.Perform();
            }
            return success;
        }

        //TODO: What if we get a name conflict?
        public bool AddAction(GameAction action)
        {
            //TODO: Just ignore duplicate names?
            if(actions.ContainsKey(action.name))
            {
                return false;
            }

            action.Source = this.gameObject;
            actions.Add(action.Name, action);
            return true;
        }

        public bool RemoveAction(GameAction action)
        {
            action.Source = null;
            return actions.Remove(action.Name);
        }

        //TODO: Return success?
        public void AddActions(IEnumerable<GameAction> actions)
        {
            foreach (GameAction action in actions)
            {
                AddAction(action);
            }
        }

        //TODO: Return success?
        public void RemoveActions(IEnumerable<GameAction> actions)
        {
            foreach (GameAction action in actions)
            {
                RemoveAction(action);
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return actions.Keys.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
