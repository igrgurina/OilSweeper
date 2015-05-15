using UnityEngine;
using System.Collections;

public class DrillController : MonoBehaviour
{
    private new Renderer renderer;

    private DrillConfig Config;

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

    private bool UserTurn
    {
        get
        {
            return User.GetComponent<UserController>().Turn != 0;
        }
    }

    private Color UserColor
    {
        get { return User.GetComponent<UserController>().Color; }
    }


    // klik
    // na klik mu se pojave 2 gumba - trenutno nebitno gdje
    // 1. gumb poveća radijus
    // 2. gumb poveća transparency

    // Use this for initialization
    void Start()
    {
        Config = GetComponent<DrillConfig>();

        Radius = Config.RADIUS_INITIAL;
        Speed = Config.SPEED_INITIAL;

        // assign renderer
        renderer = gameObject.GetComponent<Renderer>();

        // set color based on user
        // TODO: Remove this lines when multiplayer over network is done
        renderer.material.color = UserColor;

        // set initial transparency level
        renderer.material.SetAlpha(Config.SPEED_INCREMENT);

        // set initial radius
        gameObject.SetScale(Config.RADIUS_INITIAL);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        // TODO: comment this line when done with development
        var tranf = gameObject.GetComponent<Transform>();
        Debug.Log("User clicked on the drill at (" + tranf.position.x + ", " + tranf.position.y + ").");

        if (Speed < Config.SPEED_MAX)
        {
            renderer.material.IncrementAlpha(Config.SPEED_INCREMENT);
            Speed++;
        }

        if (Radius < Config.RADIUS_MAX)
        {
            gameObject.EnlargeObject(Config.RADIUS_INCREMENT);
            Radius++;
        }
    }
}

public static partial class ExtensionMethods
{
    /// <summary>
    /// Adjust transparency level of the material.
    /// </summary>
    public static void SetAlpha(this Material material, float value) { Color color = material.color; color.a = value; material.color = color; }

    public static void IncrementAlpha(this Material material, float increment)
    {
        Color color = material.color;
        color.a = material.color.a + increment;
        material.color = color;
    }

    public static void SetScale(this GameObject gameObject, float value)
    {
        gameObject.transform.localScale = new Vector3(value, value);
    }

    public static void EnlargeObject(this GameObject gameObject, float increment)
    {
        gameObject.transform.localScale += new Vector3(increment, increment);
    }

}