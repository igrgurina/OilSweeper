using UnityEngine;
using UnityEngine.UI;

public class UserController : MonoBehaviour
{
    public Text MyGoldText;
    public Text NemesisGoldText;
    public GameObject GamBoardInstance;

    public int Gold
    {
        get { return _gold; }
        set
        {
            _gold = value; 
            UpdatePoints(value);
        }
    }

    public GameObject GameBoard;

    public int Turn;
    public Color Color;
    private int _gold;

    void Start()
    {
        // TODO: Set a resaonable gold amount
        Gold = 10000;
    }

    [RPC]
    private void UpdatePoints(int points)
    {
        if (GetComponent<PhotonView>().isMine)
        {
            if (MyGoldText != null)
            {
                MyGoldText.text = "My Gold: " + points;
                Debug.Log("My Gold: " + points);
            }
            //GetComponent<PhotonView>().RPC("UpdatePoints", PhotonTargets.OthersBuffered, (object)points);
        }
        //else
        //{
        //    if (NemesisGoldText != null)
        //    {
        //        NemesisGoldText.text = "Adversary's Gold: " + points;
        //        Debug.Log("Adversary's Gold: " + points);
        //    }
        //}
        
    }
}
