using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IWhishListBL
    {
        bool AddtoWhishlist(WhishListModel whishlistModel,int userid);
        bool DeleteFromWhishlist(int whishlistId);
        List<getAllWhishlist> getAllWhishlist();

    }
}
