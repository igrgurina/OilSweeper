using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

public class UserController : MonoBehaviour
{
    private const int PLAYER_ONE = 0;
    private const int PLAYER_TWO = 1;

    public int Turn;
    public Color Color;

    private List<Color> colors = new List<Color>()
    {
        UnityEngine.Color.blue,
        UnityEngine.Color.red
    };

    // Use this for initialization
    void Start()
    {
        // TODO: comment this line when multiplayer over network works
        Turn = new Random().Next(0, 1);

        if (Turn == PLAYER_ONE)
        {
            Color = colors.First();
        }
        else if (Turn == PLAYER_TWO)
        {
            Color = colors.Last();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
