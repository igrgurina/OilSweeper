using System.Linq;
using UnityEngine;

public class GameBoardController : LoggingMonoBehaviour
{
    /// <summary>
    /// Drill prefab.
    /// </summary>
    public GameObject Drill;
    /// <summary>
    /// OilField prefab.
    /// </summary>
    public GameObject Field;
    /// <summary>
    /// User prefab.
    /// </summary>
    public GameObject User;

    // Use this for initialization
    void Start()
    {
        if (User != null)
        {
            Log("User's colour is " + User.GetComponent<UserController>().Color);
        }
    }

    public bool AllowNewDrills
    {
        get { return Resources.FindObjectsOfTypeAll<DrillController>().Any(d => d.GetComponent<PhotonView>().isMine && d.ShowUpgradePanel); }
    }


    private void OnMouseDown()
    {
        if (GetComponent<PhotonView>().isMine)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f);
            if (hit.collider != null)
            {
                Log("KLIKNUO NA BUŠOTINU " + Input.mousePosition.x + " " + Input.mousePosition.y);
            }
            else
            {
                Log("KLIKNUO IZVAN BUŠOTINE");
                Log("NAKON KLIKA IZVAN BUŠOTINE AllowNewDrills: " + AllowNewDrills);

                // this is needed to keep it on the screen
                var pom = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pom.z = 0;
                var drill = PhotonNetwork.Instantiate("Drill", pom, Quaternion.identity, 0);
                drill.GetComponent<DrillController>().User = User;
                drill.GetComponent<DrillController>().LogOutputText = LogOutputText;
                drill.GetComponent<DrillController>().Color = User.GetComponent<UserController>().Color;
            }
        }
    }
}
