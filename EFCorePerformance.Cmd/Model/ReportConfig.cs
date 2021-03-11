﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFCorePerformance.Cmd.Model
{
    public class ReportConfig
    {
        public int ConfigId { get; set; }

        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Description { get; set; }

        public string VeryUsefulInformation { get; set; }

        public List<Report> Reports { get; set; }
    }
}
