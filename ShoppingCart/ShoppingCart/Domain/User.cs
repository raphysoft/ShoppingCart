using ShoppingCart.Domain.Interfaces;

namespace ShoppingCart.Domain
{
    public class User : Entity, IArchivable
    {
        public virtual string Email { get; set; }
        protected virtual bool Archived { get; set; }

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
