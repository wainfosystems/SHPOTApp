using System;
using System.Collections.Generic;
using System.Linq;

namespace SHPOT_DAL
{
    public class Business_DAL
    {
        #region [ Constructor and Private Members ]
        /// <summary>
        /// Default Constructor and Private Members
        /// </summary>
        private DBContext _dbContext = null;

        public Business_DAL()
        {
            _dbContext = new DBContext();
        }
        #endregion

        #region [ Get All Businesses ]
        /// <summary>
        /// Get Business Details By BusinessID
        /// </summary>
        /// <returns></returns>
        public List<Business> GetAllBusinesses()
        {
            return _dbContext.Businesses.AsNoTracking().ToList();
        }
        #endregion

        #region [ Get Business Details By BusinessId ]
        /// <summary>
        /// Get Business Details By BusinessId
        /// </summary>
        /// <param name="_businessID"></param>
        /// <returns></returns>
        public Business GetBusinessDetails(int _businessID)
        {
            return _dbContext.Businesses.AsNoTracking().Where(d => d.BusinessID == _businessID).FirstOrDefault();
        }
        #endregion

        #region [ Add New Business ]
        /// <summary>
        /// Add New Business
        /// </summary>
        /// <returns></returns>
        public Business AddNewBusiness(Business _business)
        {
            _dbContext.Businesses.Add(_business);
            _dbContext.SaveChanges();
            return _business;
        }
        #endregion

        #region [ Add Business Images ]
        /// <summary>
        /// Add Business Images
        /// </summary>
        /// <returns></returns>
        public BusinessImage AddBusinessImages(BusinessImage _businessImage)
        {
            _dbContext.BusinessImages.Add(_businessImage);
            _dbContext.SaveChanges();
            return _businessImage;
        }
        #endregion

        #region [ Add Business Ratings ]
        /// <summary>
        /// Add Business Ratings
        /// </summary>
        /// <returns></returns>
        public BusinessRating AddBusinessRating(BusinessRating _businessRating)
        {
            Business _business = GetBusinessDetails(_businessRating.BusinessID);
            if (_business != null)
            {
                BusinessRating _objBusinessRating = _dbContext.BusinessRatings.Where(b => (b.BusinessID == _businessRating.BusinessID && b.UserID == _businessRating.UserID)).FirstOrDefault();

                // Check If Already in Business Rating
                if (_objBusinessRating != null)
                {
                    _objBusinessRating.Rating = _businessRating.Rating;
                    _objBusinessRating.Review = _businessRating.Review;
                    _dbContext.Entry(_objBusinessRating).State = System.Data.Entity.EntityState.Modified;
                    _dbContext.SaveChanges();
                    return _objBusinessRating;
                }
                else
                {
                    _dbContext.BusinessRatings.Add(_businessRating);
                    _dbContext.SaveChanges();
                    return _businessRating;
                }
            }
            else
                return null;
        }
        #endregion

        #region [ Get Favourite Places ]
        /// <summary>
        /// Get Favourite Places
        /// </summary>
        /// <param name="_userID"></param>
        /// <returns></returns>
        public List<FavouritePlace> GetFavouritePlaces(Int32 _userID)
        {
            return _dbContext.FavouritePlaces.AsNoTracking().Where(f => f.UserID == _userID).ToList();
        }
        #endregion

        #region [ Add To Favourite ]
        /// <summary>
        /// Add To Favourite
        /// </summary>
        /// <returns></returns>
        public FavouritePlace AddToFavourite(FavouritePlace _favouritePlace, Int32 _businessID, Boolean IsFavourite)
        {
            Business _business = GetBusinessDetails(_businessID);
            if (_business != null)
            {
                FavouritePlace _objFavouritePlace = _dbContext.FavouritePlaces.Where(b => (b.BusinessID == _business.BusinessID && b.UserID == _favouritePlace.UserID)).FirstOrDefault();

                if (IsFavourite) // Add To Favourite
                {
                    // Check If Already in Favourite List
                    if (_objFavouritePlace != null)
                        return _objFavouritePlace;
                    else
                    {
                        // Add To Favourite Place
                        _favouritePlace.BusinessID = _business.BusinessID;
                        _dbContext.FavouritePlaces.Add(_favouritePlace);
                        _dbContext.SaveChanges();
                        return _favouritePlace;
                    }
                }
                else // Revemove From Favourite
                {
                    if (_objFavouritePlace != null)
                    {
                        _dbContext.Entry(_objFavouritePlace).State = System.Data.Entity.EntityState.Deleted;
                        _dbContext.SaveChanges();
                        return _favouritePlace;
                    }
                    else
                        return null;
                }
            }
            else
                return null;
        }
        #endregion

        #region [ Add Support Query ]
        /// <summary>
        /// Add Support Query
        /// </summary>
        /// <param name="_supportQuery"></param>
        /// <returns></returns>
        public SupportQuery AddSupportQuery(SupportQuery _supportQuery)
        {
            _dbContext.SupportQueries.Add(_supportQuery);
            _dbContext.SaveChanges();
            return _supportQuery;
        }
        #endregion
    }
}
