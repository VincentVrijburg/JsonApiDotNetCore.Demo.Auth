using System;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;

namespace JsonApiDotNetCore.Demo.Auth.Data.Entities
{
    public class Car : Identifiable
    {
        [Attr]
        public string Model { get; set; }
        
        [Attr]
        public string Brand { get; set; }
        
        [Attr]
        public DateTime Released { get; set; }
    }
}