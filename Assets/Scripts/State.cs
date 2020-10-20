using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class State
{
    public static Stats stats;

    public static Stats GetStats()
    {
        return stats;
    }
}

public class Stats
{
    public string Winner { get; set; }
    public string Score { get; set; }
    public string Ricochets { get; set; }
    public string Rallies { get; set; }
}
