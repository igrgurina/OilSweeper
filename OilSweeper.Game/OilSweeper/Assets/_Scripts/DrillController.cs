using UnityEngine;
using System.Collections;

public class DrillController : MonoBehaviour
{                            
    private DrillConfig Config;

    public Color Color
    {
        get { return GetComponent<Renderer>().material.color; }
        set
        {
            SetColor(value.r, value.g, value.b, value.a);
        }
    }

    /// <summary>
    /// Radius is represented as game object scale.
    /// </summary>
    public int Radius;
    /// <summary>
    /// Speed is represented as game object transparency.
    /// </summary>
    public int Speed;

    /// <summary>
    /// User prefab.
    /// </summary>
    public GameObject User;

    /// <summary>
    /// Defines the prices for everything drill related.
    /// </summary>
    private static class Price
    {
        public static int NewDrill = global::Price.PRICE_DRILL_NEW;
        public static int Radius = global::Price.PRICE_DRILL_RADIUS;
        public static int Speed = global::Price.PRICE_DRILL_SPEED;
    }

    /// <summary>
    /// Helper property to manage player' cash
    /// </summary>
    private int PlayerCash
    {
        get
        {
            // TODO: make this real
            return User.GetComponent<UserController>().Gold; 
        }
        set { User.GetComponent<UserController>().Gold = value; }
    }


    private Color UserColor
    {
        get { return User.GetComponent<UserController>().Color; }
    }

    public bool showUpgradePanel;

    // klik
    // na klik mu se pojave 2 gumba - trenutno nebitno gdje
    // 1. gumb poveća radijus
    // 2. gumb poveća transparency

    // Use this for initialization
    void Start()
    {
        if (!showUpgradePanel)
        {

            Debug.Log("[DRILL.Start] kreiram bušotinu.");
            //showUpgradePanel = false;

            Config = GetComponent<DrillConfig>();
            Debug.Log("[DRILL.Start] dohvatio drill config.");

            Radius = Config.RADIUS_INITIAL;
            Speed = Config.SPEED_INITIAL;
            Debug.Log("[DRILL.Start] dohvatio drill config: Speed: " + Speed + " | Radius: " + Radius);

            // set initial transparency level
            Color = new Color(Color.r, Color.g, Color.b, Config.SPEED_INCREMENT);

            // set initial radius
            SetScale(Config.RADIUS_INITIAL);

        }
        else
        {
            Debug.Log("[DRILL.Start] uništavam drill");
            Network.Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (GetComponent<NetworkView>().isMine)
        {
            Debug.Log("User clicked on the drill at (" + transform.position.x + ", " + transform.position.y + ").");

            showUpgradePanel = true; // this will show the upgrade pane on the next frame render
            // and should disable further drill adding
        }
    }

    private void OnGUI()
    {
        if (GetComponent<NetworkView>().isMine && showUpgradePanel)
        {
            if (GUI.Button(new Rect(0, Screen.height - 20, Screen.width, 20), "Speed"))
            {
                showUpgradePanel = false; // hide the upgrade panel

                Debug.Log("Button speed clicked");
                // TODO: check if user has money for this
                UpgradeSpeed();
                Event.current.Use();

            }


            if (GUI.Button(new Rect(0, Screen.height - 40, Screen.width, 20), "Size"))
            {
                showUpgradePanel = false; // hide the upgrade panel

                Debug.Log("Button size clicked");
                // TODO: check if user has money for this
                UpgradeSize();
                Event.current.Use();
            }

        }
    }

    public void UpgradeSize()
    {
        if (Radius < Config.RADIUS_MAX && PlayerCash > (Price.Radius * Radius)) // total price = basic radius price * radius level
        {
            EnlargeObject(Config.RADIUS_INCREMENT);
            Radius++;
            Debug.Log("[UPGRADE] Drill radius increased to: " + Radius);
        }
    }

    public void UpgradeSpeed()
    {
        if (Speed < Config.SPEED_MAX && PlayerCash > (Price.Speed * Speed)) // total price = basic speed price * speed level
        {
            Color = new Color(Color.r, Color.g, Color.b, Mathf.Min(Color.a + Config.SPEED_INCREMENT, 1.0f));
            Speed++;
            Debug.Log("[UPGRADE] Drill speed increased to: " + Speed);
        }
    }

    [RPC]
    private void SetColor(float r, float g, float b, float a) {
        GetComponent<Renderer>().material.color = new Color(r, g, b, a);
        if (GetComponent<NetworkView>().isMine)
        {
            GetComponent<NetworkView>().RPC("SetColor", RPCMode.OthersBuffered, r, g, b, a);
        }
    }

    [RPC]
    public void SetScale(float value) {
        gameObject.transform.localScale = new Vector3(value, value);
        if (GetComponent<NetworkView>().isMine) {
            GetComponent<NetworkView>().RPC("SetScale", RPCMode.OthersBuffered, value);
        }       
    }

    [RPC]
    public void EnlargeObject(float increment) {
        gameObject.transform.localScale += new Vector3(increment, increment);
        if (GetComponent<NetworkView>().isMine) {
            GetComponent<NetworkView>().RPC("EnlargeObject", RPCMode.OthersBuffered, increment);
        }
    }
}