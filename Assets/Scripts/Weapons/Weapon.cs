using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int weaponLevel;
    public List<WeaponStats> stats;
    [System.Serializable]
    public class WeaponStats
    {
        public int damage;
        public float speed;
        public float size;
        public float amount;
        public float range;
    }
}
