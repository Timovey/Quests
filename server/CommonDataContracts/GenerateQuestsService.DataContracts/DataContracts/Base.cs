using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GenerateQuestsService.DataContracts.DataContracts
{
    //[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    //[JsonDerivedType(derivedType: typeof(Base), typeDiscriminator: 0)]
    //[JsonDerivedType(derivedType: typeof(A), typeDiscriminator: 1)]
    //[JsonDerivedType(derivedType: typeof(B), typeDiscriminator: 2)]
    public abstract class Base
    {
        public int Type { get; set; }

        public string Value { get; set; }

        
    }
}
