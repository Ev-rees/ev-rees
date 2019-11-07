using System;
using System.IO.Ports;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalActivation : MonoBehaviour
{
    // Variables contenant les joueurs (affectés dans Unity)
    /* public GameObject player1;
    public GameObject player2;

    // Positions de départ en x des joueurs
    private float posXPlayer1;
    private float posXPlayer2;

    private bool areSwitched = false;

    private SerialPort  stream = new SerialPort("COM3", 9600);
    private string[] sensorsValues;

    private void Start() {
        // Afectation des positions en X
        posXPlayer1 = player1.transform.position.x;
        posXPlayer2 = player2.transform.position.x;

        stream.ReadTimeout = 50;
        stream.Open();
    }

    private void Update() {
        string result = ReadFromArduino();

        if (result != null) {
            sensorsValues = result.Split(',');
            //int num1 = Int32.Parse(sensorsValues[0]);
            //int num2 = Int32.Parse(sensorsValues[1]);

            //Debug.Log(num1 + ", " + num2);

            if (Int32.Parse(sensorsValues[0]) < 100 || Int32.Parse(sensorsValues[0]) > 1000)

            if ((Int32.Parse(sensorsValues[0]) < 100 || Int32.Parse(sensorsValues[0]) > 1000) && (Int32.Parse(sensorsValues[1]) < 100 || Int32.Parse(sensorsValues[1]) > 1000) && areSwitched == false) {
                // On échange la place des deux joueurs
                player1.transform.position = new Vector3(posXPlayer2, player1.transform.position.y, player2.transform.position.z);
                player2.transform.position = new Vector3(posXPlayer1, player2.transform.position.y, player1.transform.position.z);
                areSwitched = true;

                Debug.Log("Oui");
            }
            
            if ((Int32.Parse(sensorsValues[0]) > 500 && Int32.Parse(sensorsValues[0]) < 600) || (Int32.Parse(sensorsValues[1]) > 500 && Int32.Parse(sensorsValues[1]) < 600) && areSwitched) {
                // On remet les joueurs à leur place
                player1.transform.position = new Vector3(posXPlayer1, player1.transform.position.y, player1.transform.position.z);
                player2.transform.position = new Vector3(posXPlayer2, player2.transform.position.y, player2.transform.position.z);
                areSwitched = false;

                Debug.Log("Non");
            }
        }


    }

    public string ReadFromArduino()
    {
        try
        {
            return stream.ReadLine();
        }
        catch (TimeoutException)
        {
            return null;
        }
    }

    public void Close()
    {
        stream.Close();
    }*/
}
