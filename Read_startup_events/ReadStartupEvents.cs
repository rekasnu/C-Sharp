using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MM.Monitor.Client;
using System.Windows.Forms;
using System.Diagnostics.Eventing.Reader;
using System.Collections;

namespace Read_startup_events
{
    public class ReadStartupEvents : ClientPlugin
    {
        private Nullable <int> eventId;
        private Nullable <int> timeInMinutes;
        private string messageL;

        public ReadStartupEvents()
        {
            this.eventId = 0;
            this.timeInMinutes = 0;
        }
        public override string GetPluginName()
        {
            return "Read Startup Event";
        }
        public override string GetPluginDescription()
        {
            return "This Plugin will check if the specific event was logged at startup.";
        }


        //this mettod is called after the Plugin is loaded
        public override void PluginLoaded()
        {
            eventId = ConvertStringToInt(GetValueForKey("Event Id"));
            timeInMinutes = ConvertStringToInt(GetValueForKey("Time in minutes"));

            if(eventId != null && timeInMinutes != null)
            {

                var startTime = System.DateTime.Now.AddMinutes(-(int)timeInMinutes);
                var endTime = System.DateTime.Now;

                var query = string.Format(@"*[System/EventID={0}] and *[System[TimeCreated[@SystemTime >= '{1}']]] and *[System[TimeCreated[@SystemTime <= '{2}']]]",
                    eventId,
                    startTime.ToUniversalTime().ToString("o"),
                    endTime.ToUniversalTime().ToString("o"));
                EventLogQuery eventsQuery = new EventLogQuery("System", PathType.LogName, query);

                try
                {
                    EventLogReader logReader = new EventLogReader(eventsQuery);

                    for (EventRecord eventdetail = logReader.ReadEvent(); eventdetail != null; eventdetail = logReader.ReadEvent())
                    {
                        //Console.Write(eventdetail.ToString());
                        int message = eventdetail.Id;
                        string texst = eventdetail.FormatDescription();
                        string time = eventdetail.TimeCreated.ToString();
                        //     Console.WriteLine("MY " + message + " " + texst+" "+time);
                        ComputerInfo localComputer = GetComputerInfo();

                        messageL = "The event "+eventId+" was logged on the computer "+localComputer.Name+" into the group "+ localComputer.Group+"\n\n" + texst + " " + time;
                    }
                }
                catch (EventLogNotFoundException e)
                {
                  //  Console.WriteLine("Error while reading the event logs");
                    return;
                }
            }
        }

        private int? ConvertStringToInt(string intString)
        {
            int i = 0;
            return (Int32.TryParse(intString, out i) ? i : (int?)null);
        }

        //this mettod is called when user clicks on configure

        public override Form GetConfigurationForm()
        {
            ReadStartupEvents Plugin = this;
            return (Form) new ConfigurationForm(ref Plugin);
        }

        public override void PluginDataCheck()
        {
            if (messageL != null)
                SendNotificationToAllDevices(messageL, NotificationPriority.CRITICAL);
            messageL = null;

        }

    }
}
