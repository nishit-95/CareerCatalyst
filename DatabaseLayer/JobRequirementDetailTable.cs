//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class JobRequirementDetailTable
    {
        public int JobRequirementDetailID { get; set; }
        public int JobRequirementID { get; set; }
        public string JobRequirementDetails { get; set; }
    
        public virtual JobRequirementTable JobRequirementTable { get; set; }
    }
}
