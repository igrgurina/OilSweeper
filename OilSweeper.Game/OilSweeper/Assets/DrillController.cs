using UnityEngine;
using System.Collections;

public class DrillController : MonoBehaviour
{
    private new Renderer renderer;
    // klik
    // na klik mu se pojave 2 gumba - trenutno nebitno gdje
    // 1. gumb poveća radijus
    // 2. gumb poveća transparency

    // Use this for initialization
    void Start()
    {
        // assign renderer
        renderer = gameObject.GetComponent<Renderer>();

        // set initial transparency level
        renderer.material.SetAlpha(0.25f);

        

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        var tranf = gameObject.GetComponent<Transform>();
        Debug.Log("Kliknuo na njega" + tranf.position.x + " " + tranf.position.y);
        renderer.material.IncrementAlpha();
        gameObject.EnlargeObject();
    }
}

public static class ExtensionMethods
{
    /// <summary>
    /// Adjust transparency level of the material.
    /// </summary>
    public static void SetAlpha(this Material material, float value) { Color color = material.color; color.a = value; material.color = color; }

    public static void IncrementAlpha(this Material material)
    {
        Color color = material.color;
        color.a = material.color.a + 0.25f;
        material.color = color;
    }

    public static void EnlargeObject(this GameObject gameObject)
    {
        gameObject.transform.localScale += new Vector3(1f,1f);
    }

}