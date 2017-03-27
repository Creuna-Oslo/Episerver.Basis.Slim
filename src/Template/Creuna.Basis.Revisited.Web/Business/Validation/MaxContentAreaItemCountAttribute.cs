using EPiServer.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace Creuna.Basis.Revisited.Web.Business.Validation
{
    /// <summary>    
    /// Limit numbers of items in ContentArea
    /// Adding [MaxContentAreaItemCount(2)] to a prop-definition limits number of items;
    /// </summary>  
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MaxContentAreaItemCountAttribute : ValidationAttribute
    {
        int Limit { get; }

        public MaxContentAreaItemCountAttribute(int limit)
        {
            Limit = limit;
        }

        public override bool IsValid(object value)
        {
            var contentArea = value as ContentArea;

            if (contentArea == null)
                throw new ValidationException("ContentAreaItemsMaxAttribute is intended only for use with ContentArea properties");

            return contentArea.Count <= Limit;
        }

        public override string FormatErrorMessage(string name)
            => $"Content area restricted to {Limit} content items"; 
    }
}