using UnityEngine;
using System.Collections;

namespace Assets.Code.Components
{
    public class Character : MonoBehaviour
    {
        #region Editor Fields
        [SerializeField]
        private int health;
        [SerializeField]
        private int maxHealth;
        [SerializeField]
        private int exp;
        [SerializeField]
        private int level;
        #endregion

        #region Properties
        #region PercentHealth
        public float PercentHealth
        {
            get
            {
                return (float)health / (float)maxHealth;
            }
        }
        #endregion

        public int Exp { get { return exp; } }

        public int Level { get { return level; } }
        #endregion

        // Use this for initialization
        public void Start()
        {
            health = maxHealth;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}