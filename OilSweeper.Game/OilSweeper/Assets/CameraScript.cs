using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraScript : MonoBehaviour
{

    public const int OIL_FIELDS_COUNT = 10;

    /// <summary>
    /// Drill prefab.
    /// </summary>
    public GameObject Pickup;
    /// <summary>
    /// OilField prefab.
    /// </summary>
    public GameObject Field;
    /// <summary>
    /// User prefab.
    /// </summary>
    public GameObject User;

    public interface ILocation
    {
        /// <summary>
        /// Longitude, x-coordinate.
        /// </summary>
        double Longitude { get; set; }

        /// <summary>
        /// Latitude, y-coordinate.
        /// </summary>
        double Latitude { get; set; }
    }

    // Use this for initialization
    void Start()
    {
        // TODO: Remove this lines when multiplayer over network is done
        var userOne = Instantiate(User);
        userOne.transform.parent = GameObject.Find("Users").transform;
        Debug.Log("User 1 created.");
        var userTwo = Instantiate(User);
        userTwo.transform.parent = GameObject.Find("Users").transform;
        Debug.Log("User 2 created.");
        

        System.Random seedRandom = new System.Random();
        for (int i = 0; i < OIL_FIELDS_COUNT; i++)
        {

            double Longitude = (seedRandom.NextDouble() * 87) - 87;
            double Latitude = (seedRandom.NextDouble() * 49) - 49;

            var newField = Instantiate(Field);
            newField.transform.position = new Vector3((float)Longitude, (float)Latitude);
            newField.transform.parent = GameObject.Find("OilFields").transform;

            Debug.Log("New oil field created at (" + Longitude + ", " + Latitude + ").");

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            //foreach (ILocation one in koordinate)
            //{
            //    Debug.Log("Koordinate svih tocaka: " + one.Longitude + " y " + one.Latitude + " ");
            //}

            Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Mouse position: " + MousePosition.x + " y " + MousePosition.y + " ");
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f);
            if (hit.collider != null)
            {
                Debug.Log("Clicked! " + MousePosition.x + " " + MousePosition.y);
                //Debug.DrawLine(ray.origin, hit.point);
            }
            else
            {
                var newPickup = Instantiate(Pickup);
                newPickup.transform.position = MousePosition;
                newPickup.transform.parent = GameObject.Find("Drills").transform;
            }
            //newPickup.transform.localScale += new Vector3(1F, 1F, 0F);
            /*Circle(tex, Convert.ToInt32(MousePosition.x), Convert.ToInt32(MousePosition.y), 1,
                nova);
            tex.Apply();
            test.material.mainTexture = tex;*/
        }
    }
}
