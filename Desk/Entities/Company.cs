﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Desk.Entities
{
    /// <summary>
    /// This class represents the object that is received when
    /// requesting companies from the desk.com API.
    /// </summary>
    /// <remarks>
    /// Author:     Miguel Zakharia [miguel@zakharia.me]
    /// Link:       http://github.com/danielsaidi/desk-csharp-sdk
    /// </remarks>
    public class Company : EntityBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> Domains { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public IDictionary<string, string> CustomFields { get; set; }

        public Company() { }

        public Company(string json)
        {
            dynamic deserialized = JsonConvert.DeserializeObject(json);
            if (deserialized != null)
            {
                Id = int.Parse(deserialized["id"].ToString());
                Name = deserialized["name"].ToString();
                Domains = ((JArray) deserialized["domains"]).ToObject<List<string>>();
                CreatedAt = DateTime.Parse(deserialized["created_at"].ToString());
                UpdatedAt = DateTime.Parse(deserialized["updated_at"].ToString());
                CustomFields = ((JObject) deserialized["custom_fields"]).ToObject<Dictionary<string, string>>();
            }
        }

        //Was not successful in using GetDynamicProperty because of issues with JArray not converting properly to List.
        private Company(object propertyContainer)
            : base(propertyContainer)
        {
            Id = GetDynamicProperty<int>("id");
            Name = GetDynamicProperty<string>("name");
            Domains = GetDynamicProperty<string[]>("domains");
            CreatedAt = GetDynamicProperty<DateTime>("created_at");
            UpdatedAt = GetDynamicProperty<DateTime>("updated_at");
            //CustomFields = GetDynamicProperty<NameValueCollection>("custom_fields");
        }
    }
}
