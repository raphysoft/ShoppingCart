using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IArchivable
    {
        void Archive();
        void UnArchive();
    }
}