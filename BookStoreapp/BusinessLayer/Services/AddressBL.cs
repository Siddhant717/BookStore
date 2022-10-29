using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AddressBL:IAddressBL
    {
        readonly IAddressRL addressRL;
        public AddressBL(IAddressRL addressRL)
        {
            
            this.addressRL = addressRL;
        }

        public bool AddAddress(AddressModel addressModel, int userid)
        {
            try
            {
                return this.addressRL.AddAddress(addressModel, userid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        public bool UpdateAddress(AddressModel addressModel, int AddressId)
        {
            try
            {
                return this.addressRL.UpdateAddress(addressModel, AddressId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteAddress(int AddressId)
        {
            try
            {
                return this.addressRL.DeleteAddress(AddressId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
