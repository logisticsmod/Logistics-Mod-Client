using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ets.Telemetry.Server.Data;
using Ets.Telemetry.Server;
using LogisticsMod.Api;
using LogisticsMod.Api.Helpers;
using Codeplex.Data;

namespace Logistics_Mod_Client
{
    public partial class Form1 : Form
    {
        //store the id of the trailer when it last changed for detecting changes in deliery
        private string lastTrailerId = "";
        private int deliveryId;
        private user currentUser;
        private LogisticsModApi api;
        private dynamic deliverydata;
        private deliveryLog deliverylog;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            api = new LogisticsModApi();
        }

        private void dataRefreshTimer_Tick(object sender, EventArgs e)
        {
            var data = Ets2TelemetryDataReader.Instance.Read();
            
            if(data.Connected){
                connectionLabel.Text = "Connected";
            }
            else
            {
                connectionLabel.Text = "Disconnected";
            }

            if (currentUser != null)
            {
                //check if the trailer id has changed since it last changed and if so add a new delivery
                if (data.TrailerId != lastTrailerId && data.HasJob)
                {
                    Dictionary<string, string> deliveryData = new Dictionary<string, string>()
                    {
                        {"trailerId", data.TrailerId},
                        {"jobSourceCity", data.SourceCity},
                        {"jobSourceCompany", data.SourceCompany},
                        {"jobDestinationCity", data.DestinationCity},
                        {"jobDestinationCompany", data.DestinationCompany},
                        {"truckFuelCapacity", data.FuelCapacity.ToString()},
                        {"truckRpmLimit", data.EngineRpmMax.ToString()},
                        {"jobIncome", data.JobIncome.ToString()},
                        {"jobDeliveryTime", data.JobDeadlineTime.ToString()},
                        {"user_id", currentUser.userId().ToString()}
                    };

                    lastTrailerId = data.TrailerId;
                    deliverydata = new LogisticsMod.Api.delivery(currentUser, deliveryData);
                    Console.WriteLine(deliverydata.getId().ToString());
                }

                if(deliverydata != null){
                    string trailerAttached;

                    if(data.TrailerAttached == true){
                        trailerAttached = "1";
                    }else{
                        trailerAttached = "2";
                    }

                    Dictionary<string, string> logData = new Dictionary<string, string>()
                    {
                        {"deliveryId", deliverydata.getId().ToString()},
                        {"trailerConnected", trailerAttached},
                        {"truckSpeed", Convert.ToInt32(data.TruckSpeed).ToString()},
                        {"truckEngineRpm", Convert.ToInt32(data.EngineRpm).ToString()},
                        {"truckElectricEnabled", Convert.ToInt32(data.ElectricOn).ToString()},
                        {"truckEngineEnabled", Convert.ToInt32(data.EngineOn).ToString()},
                        {"truckCruiseControl", Convert.ToInt32(data.CruiseControlOn).ToString()},
                        {"truckFuelAmount", Convert.ToInt32(data.Fuel).ToString()},
                        {"truckEngineGear", data.Gear.ToString()},
                        {"truckInputBreak", Convert.ToInt32(data.GameBrake).ToString()},
                        {"truckInputThrottle", Convert.ToInt32(data.GameThrottle).ToString()}
                    };

                    deliverylog = new LogisticsMod.Api.deliveryLog(logData);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentUser = new LogisticsMod.Api.user();
        }
    }


    
}
