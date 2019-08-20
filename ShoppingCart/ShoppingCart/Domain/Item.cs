using ShoppingCart.Domain.Interfaces;

namespace ShoppingCart.Domain
{
    public class Item : Entity, IArchivable
    {
        public virtual string ItemCode { get; set; }
        public virtual string Description { get; set; }
        public virtual decimal Price { get; set; }

        protected internal virtual bool Archived { get; set; }

        public void Archive()
        {
            Archived = true;
        }

        public void UnArchive()
        {
            Archived = false;
        }
    }
}
