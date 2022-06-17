using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int maxHp = 1000;
    public int currentHp = 1000;
    [SerializeField] StatusBar hpBar;
    public void TakeDamage(int damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            GameManager.instance.gameOver = true;
        }
        hpBar.SetState(currentHp, maxHp);
    }

    public void Heal(int heal)
    {
        if (currentHp <= 0) { return; }

        currentHp = Mathf.Clamp(currentHp + heal, 0 , maxHp);
        hpBar.SetState(currentHp, maxHp);
        
    }
}
