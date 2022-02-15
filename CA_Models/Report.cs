using System;

namespace Chocoholics_Anonymous.CA_Models
{
    public class Report
    {
        private String header;
        private String body;
        private String footer;

        public Report()
        {
            this.header = "";
            this.body = "";
            this.footer = "";
        }

        public string Header
        {
            get => header;
            set => header = value;
        }

        public string Body
        {
            get => body;
            set => body = value;
        }

        public string Footer
        {
            get => footer;
            set => footer = value;
        }
    }
}