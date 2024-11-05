using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Soldier {

    protected override void Start() {
        base.Start();
        GameManager.ModifyGold(-15);
    }
    public override void Attack(Enemy enemy) {
       
        Debug.Log("Archer attacks");
    }
}
