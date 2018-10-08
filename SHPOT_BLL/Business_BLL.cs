using System;
using System.Collections.Generic;
using System.Text;
using SHPOT_DAL;
using SHPOT_ViewModel;
using System.Linq;
using System.Web.Script.Serialization;
using System.Configuration;

namespace SHPOT_BLL
{
    public class Business_BLL
    {
        #region [ Default Constructor and Private Members ]
        /// <summary>
        /// Default Constructor and Private Members
        /// </summary>
        Business_DAL _objBusinessDAL;
        BusinessVM _objBusinessVM;
        FavouritePlaceVM _objFavouritePlaceVM;

        public Business_BLL()
        {
            _objBusinessDAL = new Business_DAL();
            _objBusinessVM = null;
            _objFavouritePlaceVM = null;
        }
        #endregion

        #region [ Get All Businesses ]
        /// <summary>
        /// Get All Businesses
        /// </summary>
        /// <returns></returns>
        public List<BusinessVM> GetAllBusinesses()
        {
            List<Business> _businesses = _objBusinessDAL.GetAllBusinesses();

            if (_businesses.Count > 0)
            {
                List<BusinessVM> _listBusinessVM = new List<BusinessVM>();
                foreach (Business _business in _businesses)
                {
                    BusinessVM _businessVM = MakeBusinessVM(_business);
                    _listBusinessVM.Add(_businessVM);
                }
                return _listBusinessVM;
            }
            else
                return null;
        }
        #endregion

        #region [ Get Business Details By ID ]
        /// <summary>
        /// Get Business Details BY ID
        /// </summary>
        /// <param name="_businessID"></param>
        /// <returns></returns>
        public BusinessVM GetBusinessDetails(int _businessID)
        {
            Business _business = _objBusinessDAL.GetBusinessDetails(_businessID);
            if (_business != null)
            {
                return MakeBusinessVM(_business);
            }
            else
                return null;
        }
        #endregion        

        #region [ Add New Business ]
        /// <summary>
        /// Add New Business
        /// </summary>
        /// <param name="_businessVM"></param>
        /// <returns></returns>
        public BusinessVM AddNewBusiness(BusinessVM _businessVM)
        {
            Business _business = new Business
            {
                BusinessName = _businessVM.BusinessName,
                Latitude = _businessVM.Latitude,
                Longitude = _businessVM.Longitude,
                Location = _businessVM.Location,
                ContactNo = _businessVM.ContactNo,
                Website = _businessVM.Website,
                StartTime = _businessVM.StartTime,
                EndTime = _businessVM.EndTime,
                Flavours = _businessVM.Flavours,
                UserID = _businessVM.UserID,
                CreatedDate = DateTime.Now
            };

            _business = _objBusinessDAL.AddNewBusiness(_business);
            if (_business != null)
            {
                _businessVM.BusinessID = _business.BusinessID;
                return _businessVM;
            }
            else
                return null;
        }
        #endregion

        #region [ Add Business Images ]
        /// <summary>
        /// Add Business Images
        /// </summary>
        /// <returns></returns>
        public BusinessImageVM AddBusinessImages(BusinessImageVM _businessImageVM)
        {
            BusinessImage _businessImage = new BusinessImage { BusinessID = _businessImageVM.BusinessID, ImageName = _businessImageVM.ImageName, ImageType = _businessImageVM.ImageType };
            _businessImage = _objBusinessDAL.AddBusinessImages(_businessImage);
            _businessImageVM.BusinessImageID = _businessImage.BusinessImageID;
            return _businessImageVM;
        }
        #endregion

        #region [ Add Business Rating ]
        /// <summary>
        /// Add Business Rating
        /// </summary>
        /// <returns></returns>
        public BusinessVM AddBusinessRating(BusinessRatingVM _businessRatingVM)
        {
            BusinessRating _businessRating = new BusinessRating { BusinessID = _businessRatingVM.BusinessID, UserID = _businessRatingVM.UserID, Rating = Convert.ToDouble(_businessRatingVM.Rating), Review = _businessRatingVM.Review };
            _objBusinessDAL.AddBusinessRating(_businessRating);

            return GetBusinessDetails(_businessRatingVM.BusinessID);
        }
        #endregion

        #region [ Get Favourite Places ]
        /// <summary>
        /// Get Favourite Places
        /// </summary>
        /// <param name="_userID"></param>
        /// <returns></returns>
        public List<FavouritePlaceVM> GetFavouritePlaces(Int32 _userID)
        {
            List<FavouritePlace> _favouritePlaces = _objBusinessDAL.GetFavouritePlaces(_userID);

            if (_favouritePlaces.Count > 0)
            {
                List<FavouritePlaceVM> _listFavouritePlaceVM = new List<FavouritePlaceVM>();
                foreach (FavouritePlace _favouritePlace in _favouritePlaces)
                {
                    FavouritePlaceVM _favouritePlaceVM = MakeFavouritePlaceVM(_favouritePlace);
                    _listFavouritePlaceVM.Add(_favouritePlaceVM);
                }
                return _listFavouritePlaceVM;
            }
            else
                return null;
        }
        #endregion

        #region [ Add To Favourite ]
        /// <summary>
        /// AddToFavourite
        /// </summary>
        /// <param name="_businessVM"></param>
        /// <returns></returns>
        public FavouritePlaceVM AddToFavourite(FavouritePlaceVM _favouritePlaceVM)
        {
            FavouritePlace _favouritePlace = new FavouritePlace { UserID = _favouritePlaceVM.UserID };
            _favouritePlace = _objBusinessDAL.AddToFavourite(_favouritePlace, _favouritePlaceVM.BusinessID, _favouritePlaceVM.IsFavourite);
            if (_favouritePlace != null)
            {
                _favouritePlaceVM.FavouritePlaceID = _favouritePlace.FavouritePlaceID;
                _favouritePlaceVM.BusinessID = _favouritePlace.BusinessID; /// need to check... 
                return _favouritePlaceVM;
            }
            else
                return null;
        }
        #endregion

        #region [ Add Support Query ]
        /// <summary>
        /// Add Support Query
        /// </summary>
        /// <param name="_supportQueryVM"></param>
        /// <returns></returns>
        public SupportQueryVM AddSupportQuery(SupportQueryVM _supportQueryVM)
        {
            SupportQuery _supportQuery = new SupportQuery { FullName = _supportQueryVM.FullName, EmailAddress = _supportQueryVM.EmailAddress, Query = _supportQueryVM.Query, IPAddress = _supportQueryVM.IPAddress };
            _supportQuery = _objBusinessDAL.AddSupportQuery(_supportQuery);
            if (_supportQuery != null)
            {
                _supportQueryVM.SupportQueryID = _supportQuery.SupportQueryID;
                _supportQueryVM.QueryDateTime = _supportQuery.QueryDateTime;
                return _supportQueryVM;
            }
            else
                return null;
        }
        #endregion

        #region [ Convert Business to Business View Model Object ]
        /// <summary>
        /// Convert Business to Business View Model Object 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private BusinessVM MakeBusinessVM(Business item)
        {
            _objBusinessVM = new BusinessVM()
            {
                BusinessID = item.BusinessID,
                BusinessName = item.BusinessName,
                Latitude = item.Latitude,
                Longitude = item.Longitude,
                Location = item.Location,
                ContactNo = item.ContactNo,
                Website = item.Website,
                StartTime = item.StartTime,
                EndTime = item.EndTime,
                Flavours = item.Flavours,
                UserID = item.UserID,
                IsFavouritePlace = item.FavouritePlaces.Where(f => (f.BusinessID == item.BusinessID && f.UserID == item.UserID)).Count() > 0 ? 1 : 0
            };


            // Add Business Rating
            Double _averageRating = 0;
            if (item.BusinessRatings.Count > 0)
            {
                List<BusinessRatingVM> lstBusinessRating = new List<BusinessRatingVM>();
                foreach (var rating in item.BusinessRatings)
                {
                    BusinessRatingVM _businessRatingVM = new BusinessRatingVM
                    {
                        BusinessRatingID = rating.BusinessRatingID,
                        BusinessID = rating.BusinessID,
                        Rating = rating.Rating.ToString(),
                        Review = rating.Review,
                        UserID = rating.UserID,
                        UserName = rating.User.UserName,
                        ProfileImageUrl = rating.User.ProfileImageUrl
                    };
                    lstBusinessRating.Add(_businessRatingVM);
                    _averageRating = _averageRating + rating.Rating;
                }

                _objBusinessVM.Reviews = new JavaScriptSerializer().Serialize(lstBusinessRating);
                _averageRating = _averageRating / item.BusinessRatings.Count;
            }
            else
                _objBusinessVM.Reviews = "[]";

            _objBusinessVM.AverageRating = _averageRating;

            // Add Business Images
            if (item.BusinessImages.Count > 0)
            {
                List<BusinessImageVM> lstBusinessImage = new List<BusinessImageVM>();
                string tempDocUrl = ConfigurationManager.AppSettings["APIURL"];
                foreach (var image in item.BusinessImages)
                {
                    BusinessImageVM _businessImageVM = new BusinessImageVM
                    {
                        BusinessImageID = image.BusinessImageID,
                        BusinessID = image.BusinessID,
                        ImageName = image.ImageName,
                        ImageType = image.ImageType,
                        ImagePath = tempDocUrl + "/Uploads/Business/" + image.BusinessID + "/" + image.ImageName
                    };
                    lstBusinessImage.Add(_businessImageVM);
                }
                _objBusinessVM.Images = new JavaScriptSerializer().Serialize(lstBusinessImage);
            }
            else
                _objBusinessVM.Images = "[]";

            return _objBusinessVM;
        }
        #endregion

        #region [ Convert FavouritePlace to FavouritePlace View Model Object ]
        /// <summary>
        /// Convert FavouritePlace to FavouritePlace View Model Object 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private FavouritePlaceVM MakeFavouritePlaceVM(FavouritePlace item)
        {
            _objFavouritePlaceVM = new FavouritePlaceVM()
            {
                FavouritePlaceID = item.FavouritePlaceID,
                UserID = item.Business.UserID,
                BusinessID = item.BusinessID,
                BusinessName = item.Business.BusinessName,
                Latitude = item.Business.Latitude,
                Longitude = item.Business.Longitude,
                Location = item.Business.Location,
                ContactNo = item.Business.ContactNo,
                Website = item.Business.Website,
                StartTime = item.Business.StartTime,
                EndTime = item.Business.EndTime,
                Flavours = item.Business.Flavours,
                IsFavouritePlace = 1
            };

            // Add Business Rating
            Double _averageRating = 0;
            if (item.Business.BusinessRatings.Count > 0)
            {
                List<BusinessRatingVM> lstBusinessRating = new List<BusinessRatingVM>();
                foreach (var rating in item.Business.BusinessRatings)
                {
                    BusinessRatingVM _businessRatingVM = new BusinessRatingVM
                    {
                        BusinessRatingID = rating.BusinessRatingID,
                        BusinessID = rating.BusinessID,
                        Rating = rating.Rating.ToString(),
                        Review = rating.Review,
                        UserID = rating.UserID,
                        UserName = rating.User.UserName,
                        ProfileImageUrl = rating.User.ProfileImageUrl
                    };
                    lstBusinessRating.Add(_businessRatingVM);
                    _averageRating = _averageRating + rating.Rating;
                }

                _objFavouritePlaceVM.Reviews = new JavaScriptSerializer().Serialize(lstBusinessRating);
                _averageRating = _averageRating / item.Business.BusinessRatings.Count;
            }
            else
                _objFavouritePlaceVM.Reviews = "[]";

            _objFavouritePlaceVM.AverageRating = _averageRating;

            // Add Business Images
            if (item.Business.BusinessImages.Count > 0)
            {
                List<BusinessImageVM> lstBusinessImage = new List<BusinessImageVM>();
                string tempDocUrl = ConfigurationManager.AppSettings["APIURL"];
                foreach (var image in item.Business.BusinessImages)
                {
                    BusinessImageVM _businessImageVM = new BusinessImageVM
                    {
                        BusinessImageID = image.BusinessImageID,
                        BusinessID = image.BusinessID,
                        ImageName = image.ImageName,
                        ImageType = image.ImageType,
                        ImagePath = tempDocUrl + "/Uploads/Business/" + image.BusinessID + "/" + image.ImageName
                    };
                    lstBusinessImage.Add(_businessImageVM);
                }
                _objFavouritePlaceVM.Images = new JavaScriptSerializer().Serialize(lstBusinessImage);
            }
            else
                _objFavouritePlaceVM.Images = "[]";

            return _objFavouritePlaceVM;
        }
        #endregion             
    }
}
