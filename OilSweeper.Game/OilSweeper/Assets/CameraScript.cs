using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraScript : MonoBehaviour {

    private Renderer test;
    public IEnumerable<ILocation> koordinate;
    public GameObject Pickup;

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

    public class Coordinates : ILocation
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }

	// Use this for initialization
	void Start () {
        test = GetComponent<Renderer>();
        System.Random seedRandom = new System.Random();
        List<ILocation> coordinateList = new List<ILocation>();
        for (int i = 1; i < 2; i++)
        {
            Coordinates randomCoordinates = new Coordinates();
            randomCoordinates.Longitude = (seedRandom.NextDouble() * 87) - 87;
            randomCoordinates.Latitude = (seedRandom.NextDouble() * 49) - 49;
            coordinateList.Add(randomCoordinates);
        }

        koordinate = coordinateList;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {

            foreach (ILocation one in koordinate)
            {
                Debug.Log("Koordinate svih tocaka: " + one.Longitude + " y " + one.Latitude + " ");
            }

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
            }
            //newPickup.transform.localScale += new Vector3(1F, 1F, 0F);
            /*Circle(tex, Convert.ToInt32(MousePosition.x), Convert.ToInt32(MousePosition.y), 1,
                nova);
            tex.Apply();
            test.material.mainTexture = tex;*/
        }
	}
}
