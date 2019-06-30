using System.Collections.Generic;

namespace OMM.Domain
{
    public class AssetType
    {
        public AssetType()
        {
            this.Assets = new HashSet<Asset>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }

        // Software, Vehicle, Transport, Communication, Social, Office Equipment
    }
}
