﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YLP.UWP.Core.Common;

namespace YLP.UWP.Core.Models
{
    public class Author : BindableBase
    {
        public string userid { get; set; }

        public string sid { get; set; }

        public string username { get; set; }

        public string usericon { get; set; }

        public string avatar
        {
            get
            {
                if (string.IsNullOrEmpty(usericon))
                {
                    return "Assets/StoreLogo.png";
                }

                return usericon;
            }
        }

        public int userlevel { get; set; }

        public int userscore { get; set; }
    }
}
