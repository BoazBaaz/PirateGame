using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageble
{
    int hitpoints { get; set; }

    void ModifyHealth(int amount);
}
