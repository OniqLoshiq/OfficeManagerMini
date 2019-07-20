using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMM.Domain
{
    public class Asset
    {
        public string Id { get; set; }

        public string InventoryNumber { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string ReferenceNumber { get; set; }

        public DateTime DateOfAquire { get; set; }

        [ForeignKey(nameof(AssetType))]
        public int AssetTypeId { get; set; }
        public virtual AssetType AssetType { get; set; }

        [ForeignKey(nameof(Employee))]
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
