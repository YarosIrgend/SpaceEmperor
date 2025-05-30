namespace SpaceEmperor;

// public class Ship(int hp, int at, int maxHp)
// {
//     public int HP { get; set; } = hp;
//     public int Attack { get; set; } = at;
//     public int MaxHp { get; set; } = maxHp;
// }

public class Ship
{
    public int HP { get; set; }
    public int Attack { get; set; }
    public int MaxHp { get; set; }
    public IDefenseStrategy DefenseStrategy { get; set; }

    public Ship(int hp, int attack, int maxHp)
    {
        HP = hp;
        Attack = attack;
        MaxHp = maxHp;
    }

    // public void ExecuteAttack(Ship defender)
    // {
    //     AttackStrategy.Attack(this, defender);
    // }
}
