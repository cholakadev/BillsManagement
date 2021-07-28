using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.DAL.Models
{
    public partial class NotificationSetting
    {
        public int? SettingsKey { get; set; }
        public string BusinessEmail { get; set; }
        public string BusinessEmailPassword { get; set; }
    }
}
