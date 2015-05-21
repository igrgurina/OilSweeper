using UnityEngine;
using System.Collections;

public class OilFieldController : LoggingMonoBehaviour {
    private int _capacity;

    public int Capacity
    {
        get { return _capacity; }
        set { SetCapacity(value); }
    }

    [RPC]
    private void SetCapacity(int capacity)
    {
        _capacity = capacity;
        if (GetComponent<PhotonView>().isMine)
        {
            GetComponent<PhotonView>().RPC("SetCapacity", PhotonTargets.OthersBuffered, capacity);            
        }
    }

    [RPC]
    public void Harvest(int amount)
    {
        _capacity -= amount;
        if (_capacity < 0) _capacity = 0;

        if (GetComponent<PhotonView>().isMine) {
            GetComponent<PhotonView>().RPC("Harvest", PhotonTargets.OthersBuffered, amount);
        }

        if (_capacity == 0 && PhotonNetwork.isMasterClient)
        {
            // Erase the field once it gets empty
            Log("Oil field has gone empty.");
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
