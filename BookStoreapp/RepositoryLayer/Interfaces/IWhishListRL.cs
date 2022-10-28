using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IWhishListRL
    {
        bool AddtoWhishlist(WhishListModel whishlistModel,int userid);
        bool DeleteFromWhishlist(int whishlistId);
        List<getAllWhishlist> getAllWhishlist();
    }
}
