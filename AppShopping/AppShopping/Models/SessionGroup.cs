using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppShopping.Models
{

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class SessionGroup : List<string>
    {

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        string[] Items
        {
            get
            {
                return this.ToArray();
            }
            set 
            {
                if (value != null)
                {
                    this.AddRange(value);
                }
            }
        }

        public SessionGroup()
        {

        }

        public SessionGroup(string name, List<String> list) : base(list)
        {
            this.Name = name;
        }
    }
}
