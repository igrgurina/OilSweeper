using UnityEngine;

public class UserController : MonoBehaviour
{
    public int Gold;
    public GameObject GameBoard;

    public int Turn;
    public Color Color;

    void Start()
    {
        // TODO: Set a resaonable gold amount
        Gold = 10000;
        var gameBoard = Instantiate(GameBoard);
        // This player gets his instance of the game board
        gameBoard.GetComponent<GameBoardController>().User = gameObject;
    }

    void Update()
    {

    }
}
