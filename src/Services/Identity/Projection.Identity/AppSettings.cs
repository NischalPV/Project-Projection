using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projection.Identity;

public class AppSettings
    {
        public string IsClusterEnv { get; set; }
        public string WebApiUrl { get; set; }
        public string AngularUrl { get; set; }
        public string SigningCertificate { get; set; }
        public int UseCustomizationData { get; set; }
    }