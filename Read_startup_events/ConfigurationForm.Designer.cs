
using System.Windows.Forms;
using MM.Monitor.Client;

namespace Read_startup_events
{
    public partial class ConfigurationForm : Form
    {
        private readonly IDataStore dataStore;

        public ConfigurationForm(IDataStore dataStore)
        {
            InitializeComponent();
            this.dataStore = dataStore;
        }
    }
}
