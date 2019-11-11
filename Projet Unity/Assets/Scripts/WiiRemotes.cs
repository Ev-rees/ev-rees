using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using WiimoteApi;
using WiimoteApi.Internal;

public class WiiRemotes : MonoBehaviour
{
    public List<Wiimote> wiiRemotes = new List<Wiimote>();

    // Initialise les wii remotes
    public void InitWiimotes()
    {
        // Trouve les wiimotes connectées
        WiimoteManager.FindWiimotes();

        // Parcoure toutes les wii remotes
        foreach (Wiimote wiiRemote in WiimoteManager.Wiimotes)
        {
            wiiRemotes.Add(wiiRemote);
            wiiRemotes[wiiRemotes.Count-1].SetupIRCamera(IRDataType.EXTENDED);

            Debug.Log("Wii Remote trouvée !");
        }
    }

    private void OnApplicationQuit()
    {
        // Parcoure toutes les wii remotes
        foreach (Wiimote remote in WiimoteManager.Wiimotes)
        {
            WiimoteManager.Cleanup(remote);
        }
    }
}


