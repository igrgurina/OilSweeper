using System;
using UnityEngine;

public static partial class ExtensionMethods {

    public static bool AvailableForJoin(this HostData host) {
        return host.connectedPlayers < host.playerLimit;
    }

}
