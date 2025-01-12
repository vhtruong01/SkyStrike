using UnityEngine;

public class Entity : MonoBehaviour
{
    public int hp { get; private set; }
    public string type { get; private set; }
    public bool isDie { get; private set; }
    public IBullet bullet { get; private set; }

    public void Awake()
    {
        isDie = false;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            Die();
        }
    }
    public void Die()
    {
        isDie = true;
    }
}