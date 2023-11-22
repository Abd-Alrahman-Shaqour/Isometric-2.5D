using System;
using Shiro.Weapons;

[Serializable]
public class PlayerStats
{
        public int health = 100;
        public string name = "Shiro";
        public Weapons currentWeapon;
        public int coins = 0;
}
