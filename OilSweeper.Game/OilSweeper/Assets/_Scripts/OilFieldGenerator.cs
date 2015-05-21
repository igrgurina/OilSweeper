using System.Linq;
using UnityEngine;
using Random = System.Random;

/// <summary>
/// Controls oil field generation and harvesting
/// </summary>
public class OilFieldGenerator : LoggingMonoBehaviour
{
    public const int OIL_FIELDS_COUNT = 20;
    public const int OIL_FIELD_MIN_CAPACITY = 3000;
    public const int OIL_FIELD_MAX_CAPACITY = 10000;
    public const int HARVEST_MULTIPLYER = 50;

	void Start () {
        // Generate oil fields only on the server, so that all players get the same
	    if (PhotonNetwork.isMasterClient)
	    {
            GenerateOilFields();
	    }	
        // Begin harvesting the oil fields
	    Log("Initiating the harvesting...");
        InvokeRepeating("Harvest", 1, 1f);
	}

    private void GenerateOilFields()
    {
        Log("Creating oil fields...");
        var seedRandom = new Random();
        for (var i = 0; i < OIL_FIELDS_COUNT; i++) {
            var x = (float)((seedRandom.NextDouble() * 10 - 5) / 16 * 9);
            var y = (float)(seedRandom.NextDouble() * 10 - 5);
            
            var newField = PhotonNetwork.Instantiate("OilField", new Vector3(x, y), Quaternion.identity, 0);
            newField.GetComponent<OilFieldController>().Capacity = OIL_FIELD_MIN_CAPACITY + seedRandom.Next(OIL_FIELD_MAX_CAPACITY - OIL_FIELD_MIN_CAPACITY);
            newField.GetComponent<OilFieldController>().LogOutputText = LogOutputText;

            Log("New oil field created at (" + x + ", " + y + ") containing " + newField.GetComponent<OilFieldController>().Capacity + " black gold.");
        }	                
    } 

    /// <summary>
    /// Harvests all oil fields in range of the specified drill
    /// </summary>
    /// <param name="drill"></param>
    private int Harvest(GameObject drill)
    {
        int harvested = 0;
        var drillController = drill.GetComponent<DrillController>();
        if (drillController != null)
        {
            var width = drill.GetComponent<SpriteRenderer>().sprite.rect.width;
            var radiusMultiplyer = 2.0f / width * 40 / 28;
            var radius = drillController.Radius * radiusMultiplyer; // Convert to internal radius
            var speed = drillController.Speed * HARVEST_MULTIPLYER;
            var fieldsInRange = GameObject.FindGameObjectsWithTag("OilField").Where(f => Vector3.Distance(f.transform.position, drillController.transform.position) < radius).ToList();

            // If a field is in range, harvest it
            foreach (var field in fieldsInRange)
            {
                var fieldController = field.GetComponent<OilFieldController>();
                var amount = Mathf.Min(fieldController.Capacity, speed);
                harvested += amount;
                fieldController.Harvest(amount);
            }
            drillController.LastHarvest = harvested;
        }
        return harvested;
    }

    /// <summary>
    /// Harvest all drills on the field and add to player's gold
    /// </summary>
    private void Harvest()
    {
        var myDrills = GameObject.FindGameObjectsWithTag("Drill").Where(drill => drill.GetComponent<PhotonView>().isMine).ToList();
        var harvested = myDrills.Sum(drill => Harvest(drill));
        GetComponent<GameBoardController>().User.GetComponent<UserController>().Gold += harvested;

        if (harvested > 0) {
            Log("Amount harvested: " + harvested);
        }
    }

}
