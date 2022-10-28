using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WhishListBL:IWhishListBL
    {
        readonly IWhishListRL whishlistRL;
        public WhishListBL(IWhishListRL whishlistRL)
        {
            this.whishlistRL = whishlistRL;
        }

        public bool AddtoWhishlist(WhishListModel whishlistModel,int userid)
        {
            try
            {
                return this.whishlistRL.AddtoWhishlist(whishlistModel, userid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteFromWhishlist( int whishlistId)
        {
            try
            {
                return this.whishlistRL.DeleteFromWhishlist(whishlistId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<getAllWhishlist> getAllWhishlist()
        {
            try
            {
                return this.whishlistRL.getAllWhishlist();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
