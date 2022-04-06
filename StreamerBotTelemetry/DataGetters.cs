using Harmony;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

namespace OBSTelemetry
{
    public class DataGetters
    {
        string dataLogFolder = "OBSTelemetry\\";
        public Main DataLogger { get; set; }

        public DataGetters(Main dataLogger)
        {
            this.DataLogger = dataLogger;
        }

        public void GetData()
        {
            Actor playeractor = FlightSceneManager.instance.playerActor;
            GameObject currentVehicle = VTOLAPI.GetPlayersVehicleGameObject();
            string[] lines = { "Alt: XXX", "Speed: XXX", "HDG: XXX", "Vspd: XXX", "Força G: XXX", "Health: XXX", "Fuel: XXX" };
            string temp1;
            string temp2;

            //Get altitude in ft
            try
            {
                temp1 = Math.Round((playeractor.flightInfo.altitudeASL * 3.28), 0).ToString();
                temp2 = $"Alt: {temp1} ft";
                lines[0] = temp2;
            }
            catch (Exception)
            {
                //File.WriteAllText(dataLogFolder + "alt.txt", "Alt: XXX");
                lines[0] = "Alt: XXX";
            }
            //Get Airspeed in kt
            try
            {
                temp1 = Math.Round((playeractor.flightInfo.airspeed * 1.943), 0).ToString();
                temp2 = $"Speed: {temp1} kt";
                lines[1] = temp2;
            }
            catch (Exception)
            {
                //File.WriteAllText(dataLogFolder + "spd.txt", "Speed: XXX");
                lines[1] = "Speed: XXX";
            }
            //Get heading
            try
            {
                temp1 = Math.Round(playeractor.flightInfo.heading, 0).ToString();
                temp2 = $"HDG: {temp1}";
                lines[2] = temp2;
            }
            catch (Exception)
            {
                //File.WriteAllText(dataLogFolder + "hdg.txt", "HDG: XXX");
                lines[2] = "HDG: XXX";
            }
            //Get Vertical speed in ft/min
            try
            {
                temp1 = Math.Round((playeractor.flightInfo.verticalSpeed * 197), 0).ToString();
                temp2 = $"Vspd: {temp1} ft/min";
                lines[3] = temp2;
            }
            catch (Exception)
            {
                //File.WriteAllText(dataLogFolder + "vspd.txt", "Vspd: XXX");
                lines[3] = "Vspd: XXX";
            }
            //Get G force
            try
            {
                temp1 = Math.Round(playeractor.flightInfo.playerGs, 1).ToString();
                temp2 = $"Força G: {temp1}";
                lines[4] = temp2;
            }
            catch (Exception)
            {
                //File.WriteAllText(dataLogFolder + "gs.txt", "Força G: XXX");
                lines[4] = "Força G: XXX";
            }
            //Get Health Level
            try
            {
                Health health = Traverse.Create(playeractor).Field("h").GetValue() as Health;
                temp1 = health.currentHealth.ToString();
                temp2 = $"Health: {temp1}%";
                lines[5] = temp2;
            }
            catch (Exception)
            {
                //File.WriteAllText(dataLogFolder + "health.txt", "Health: XXX");
                lines[5] = "Health: XXX";
            }
            //Write each value to a line in status.txt
            try
            {
                File.WriteAllLines(dataLogFolder + "status.txt", lines);
            }
            catch (Exception ex)
            {
                File.WriteAllText(dataLogFolder + "exceptions.txt", ex.ToString());
            }
        }
        
        public void clearData()
        {
            string[] lines = { "Alt: XXX", "Speed: XXX", "HDG: XXX", "Vspd: XXX", "Força G: XXX", "Health: XXX"};
            try
            {
                File.WriteAllLines(dataLogFolder + "status.txt", lines);
            }
            catch (Exception ex)
            {
                File.WriteAllText(dataLogFolder + "exceptions.txt", ex.ToString());
            }
        }
    }
}