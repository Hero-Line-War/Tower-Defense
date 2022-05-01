using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Range(1f, 999f)]
    int money = 300;

    [SerializeField, Range(1f, 30f)]
    int lives = 15;
    public static int Money { get; set; }
    public static int Lives { get; set; }


    public static int rounds;

    public void Start()
    {
        rounds = 0;
        Money = money;
        Lives = lives;
    }

}
