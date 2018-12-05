using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Trebuchet.Settings
{
    public class Resize : SettingsBase
    {
        private int choice;
        private int percent;
        private bool boundWidthEnabled;
        private int boundWidth;
        private bool boundHeightEnabled;
        private int boundHeight;
        private bool createDir;

        public int Choice
        {
            get { return this.choice; }
            set { this.choice = value; }
        }

        public int Percent
        {
            get { return this.percent; }
            set { this.percent = value; }
        }

        public bool BoundWidthEnabled
        {
            get { return this.boundWidthEnabled; }
            set { this.boundWidthEnabled = value; }
        }

        public int BoundWidth
        {
            get { return this.boundWidth; }
            set { this.boundWidth = value; }
        }

        public bool BoundHeightEnabled
        {
            get { return this.boundHeightEnabled; }
            set { this.boundHeightEnabled = value; }
        }

        public int BoundHeight
        {
            get { return this.boundHeight; }
            set { this.boundHeight = value; }
        }

        public bool CreateDir
        {
            get { return this.createDir; }
            set { this.createDir = value; }
        }

        public override void LoadSettings(object obj)
        {
            Resize settings = obj as Resize;
            if (settings != null)
            {
                this.Choice = settings.Choice;
                this.Percent = settings.Percent;
                this.BoundWidthEnabled = settings.BoundWidthEnabled;
                this.BoundWidth = settings.BoundWidth;
                this.BoundHeightEnabled = settings.BoundHeightEnabled;
                this.BoundHeight = settings.BoundHeight;
                this.CreateDir = settings.CreateDir;
            }
        }

        public override void LoadDefaults()
        {
            this.Choice = 0;
            this.Percent = 25;
            this.BoundWidthEnabled = false;
            this.BoundWidth = 500;
            this.BoundHeightEnabled = false;
            this.BoundHeight = 500;
            this.CreateDir = false;
        }
    }
}
