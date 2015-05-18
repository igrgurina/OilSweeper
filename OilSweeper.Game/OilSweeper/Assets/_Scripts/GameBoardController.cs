using UnityEngine;
using System.Collections;
using System.Linq;

public class GameBoardController : MonoBehaviour
{

    public const int OIL_FIELDS_COUNT = 10;

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
            Debug.Log(User.GetComponent<UserController>().Color);
            Debug.Log("User's colour is " + User.GetComponent<UserController>().Color);
        }
    }

    public bool AllowNewDrills
    {
        get { return Resources.FindObjectsOfTypeAll<DrillController>().Any(d => d.GetComponent<NetworkView>().isMine && d.showUpgradePanel); }
    }


    private void OnMouseDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f);
        if (hit.collider != null)
        {
            Debug.Log("KLIKNUO NA BUŠOTINU " + Input.mousePosition.x + " " + Input.mousePosition.y);
        }
        else
        {
            Debug.Log("KLIKNUO IZVAN BUŠOTINE");
            Debug.Log("NAKON KLIKA IZVAN BUŠOTINE AllowNewDrills: " + AllowNewDrills);

            // this is needed to keep it on the screen
            var pom = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pom.z = 0;
            var drill = (GameObject) Network.Instantiate(Drill, pom, Quaternion.identity, 2);
            drill.GetComponent<DrillController>().User = User;
            drill.GetComponent<DrillController>().Color = User.GetComponent<UserController>().Color;
        }
    }
}
