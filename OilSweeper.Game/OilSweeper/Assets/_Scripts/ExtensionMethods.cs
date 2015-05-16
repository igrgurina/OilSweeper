using System;
using UnityEngine;

public static partial class ExtensionMethods {

    public static string ToColorString(this Color color) {
        return String.Format("({0}, {1}, {2}, {3})", color.r, color.g, color.b, color.a);
    }

    public static bool AvailableForJoin(this HostData host) {
        return host.connectedPlayers < host.playerLimit;
    }

}
