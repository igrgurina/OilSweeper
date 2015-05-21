using UnityEngine;

public class DrillController : LoggingMonoBehaviour
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
    /// Last amount harvested
    /// </summary>
    public int LastHarvest;

    private int UpgradeSpeedPrice { get { return Price.Speed * Speed; } }

    private int UpgradeRadiusPrice { get { return (int)Price.Radius * Radius; } }

    private bool UpgradeSpeedPossible { get { return Speed < Config.SPEED_MAX && UpgradeSpeedPrice <= PlayerCash; } }

    private bool UpgradeRadiusPossible { get { return Radius < Config.RADIUS_MAX && UpgradeRadiusPrice <= PlayerCash; } }

    private bool UpgradePossible { get { return UpgradeSpeedPossible || UpgradeRadiusPossible; } }

    private bool CreationPossible { get { return !ShowUpgradePanel && PlayerCash >= Price.NewDrill; } }

    private GameBoardController MyGameBoardController { get { return User.GetComponent<UserController>().GameBoard.GetComponent<GameBoardController>(); } }

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

    private bool _showUpgradePanel;

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
        get { return User.GetComponent<UserController>().Gold; }
        set { User.GetComponent<UserController>().Gold = value; }
    }

    private Color UserColor { get { return User.GetComponent<UserController>().Color; } }

    public bool ShowUpgradePanel
    {
        get { return _showUpgradePanel; }
        set
        {
            _showUpgradePanel = value && UpgradePossible;
        }
    }

    // klik
    // na klik mu se pojave 2 gumba - trenutno nebitno gdje
    // 1. gumb poveća radijus
    // 2. gumb poveća transparency

    // Use this for initialization
    void Start()
    {
        if (CreationPossible)
        {

            Log("[DRILL.Start] kreiram bušotinu.");
            //showUpgradePanel = false;

            Config = GetComponent<DrillConfig>();
            Radius = Config.RADIUS_INITIAL;
            Speed = Config.SPEED_INITIAL;
            Log("[DRILL.Start] dohvatio drill config: Speed: " + Speed + " | Radius: " + Radius);

            // set initial transparency level
            Color = new Color(Color.r, Color.g, Color.b, Config.SPEED_INCREMENT);

            // set initial radius
            SetScale(Config.RADIUS_INITIAL);

            // Pay for the platform
            if (User != null) PlayerCash -= Price.NewDrill;
        }
        else
        {
            Log("[DRILL.Start] uništavam drill");
            PhotonNetwork.Destroy(gameObject);
        }
    }

    void OnMouseDown()
    {
        if (GetComponent<PhotonView>().isMine && !MyGameBoardController.AllowNewDrills)
        {
            Log("User clicked on the drill at (" + transform.position.x + ", " + transform.position.y + ").");

            ShowUpgradePanel = true; // this will show the upgrade pane on the next frame render
            // and should disable further drill adding
        }
    }

    private void OnGUI()
    {
        if (GetComponent<PhotonView>().isMine)
        {
            // Show last harvested gold
            DrawHarvestLabel();

            if (ShowUpgradePanel)
            {
                var height = Screen.height/6;

                if (UpgradeSpeedPossible && GUI.Button(new Rect(0, Screen.height - height, (float) Screen.width/3, height), "Speed"))
                {
                    ShowUpgradePanel = false; // hide the upgrade panel
                    Log("Button speed clicked");
                    UpgradeSpeed();
                    Event.current.Use();
                }

                if (UpgradeRadiusPossible && GUI.Button(new Rect((float) Screen.width/3, Screen.height - height, (float) Screen.width/3, height), "Size"))
                {
                    ShowUpgradePanel = false; // hide the upgrade panel
                    Log("Button size clicked");
                    UpgradeSize();
                    Event.current.Use();
                }

                if (GUI.Button(new Rect((float) Screen.width/3*2, Screen.height - height, (float) Screen.width/3, height), "Cancel"))
                {
                    ShowUpgradePanel = false;
                }
            }
        }
    }

    private void DrawHarvestLabel()
    {
        var p = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        var labelStyle = new GUIStyle { alignment = TextAnchor.MiddleCenter, clipping = TextClipping.Overflow, fontStyle = FontStyle.Bold };
        var color = GUI.contentColor;
        GUI.contentColor = Color.black;
        GUI.Label(new Rect(p.x - 10, Screen.height - p.y - 10, 20, 20), LastHarvest.ToString(), labelStyle);
        GUI.contentColor = color;
    }

    private void UpgradeSize()
    {
        if (UpgradeRadiusPossible)
        {
            if (User != null) PlayerCash -= UpgradeRadiusPrice;
            Radius++;

            EnlargeObject(Config.RADIUS_INCREMENT);
            Log("[UPGRADE] Drill radius increased to: " + Radius);
        }
    }

    private void UpgradeSpeed()
    {
        if (UpgradeSpeedPossible)
        {
            if (User != null) PlayerCash -= UpgradeSpeedPrice;
            Speed++;

            Color = new Color(Color.r, Color.g, Color.b, Mathf.Min(Color.a + Config.SPEED_INCREMENT, 1.0f));
            Log("[UPGRADE] Drill speed increased to: " + Speed);
        }
    }

    [RPC]
    private void SetColor(float r, float g, float b, float a) 
    {
        GetComponent<Renderer>().material.color = new Color(r, g, b, a);
        if (GetComponent<PhotonView>().isMine)
        {
            GetComponent<PhotonView>().RPC("SetColor", PhotonTargets.OthersBuffered, r, g, b, a);
        }
    }

    [RPC]
    private void SetScale(float value)
    {
        gameObject.transform.localScale = new Vector3(value, value);
        if (GetComponent<PhotonView>().isMine)
        {
            GetComponent<PhotonView>().RPC("SetScale", PhotonTargets.OthersBuffered, value);
        }       
    }

    [RPC]
    private void EnlargeObject(float increment) 
    {
        gameObject.transform.localScale += new Vector3(increment, increment);
        if (GetComponent<PhotonView>().isMine)
        {
            GetComponent<PhotonView>().RPC("EnlargeObject", PhotonTargets.OthersBuffered, increment);
        }
    }
}