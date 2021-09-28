using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ CreateAssetMenu(fileName = "ScriptableObject",  menuName = "ScriptableObjects/CannonBallData", order = 0)]
public class CannonballData : ScriptableObject
{
    public enum layers : int
    {
        Player = 8,
        Enemy = 9
    }
    public layers ownerLayer;
    public int damageDealt;
    public float speed;
    public float lifeTime;
}
