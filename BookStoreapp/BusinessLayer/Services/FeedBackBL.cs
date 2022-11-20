using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class FeedBackBL:IFeedBackBL
    {
        readonly IFeedBackRL feedbackRL;
        public FeedBackBL(IFeedBackRL feedbackRL)
        {
            this.feedbackRL = feedbackRL;
        }

        public bool AddFeedback(AddFeedbackModel addFeedbackModel)
        {
            try
            {
                return this.feedbackRL.AddFeedback(addFeedbackModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AllfeedbackModel> getFeedback(int bookId)
        {
            try
            {
                return this.feedbackRL.getFeedback(bookId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
