﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLP.UWP.Core.Models
{
    public class User
    {
        public string UserId { get; set; }

        public string NickName { get; set; }

        public string Account { get; set; }

        public string SessionId { get; set; }

        public string DeviceId { get; set; }
    }
}
