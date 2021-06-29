using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DoNotPatchAttribute : Attribute
    { }
}
