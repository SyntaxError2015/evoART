using System;
using evoART.DAL.UnitsOfWork;
using evoART.Models.DbModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace evoART.UnitTests
{
    //[TestClass]
    public class PhotoTests
    {
        [TestMethod]
        public void PhotosCRUDTest()
        {
            var user = DatabaseWorkUnit.Instance.UserAccountRepository.GetUser("coddo");
            var album = DatabaseWorkUnit.Instance.AlbumsRepository.GetAlbumsForUser(user.UserId, user.UserId)[0];
            var tag = DatabaseWorkUnit.Instance.ContentTagsRepository.GetContentTag("SFW");

            var photoId = DatabaseWorkUnit.Instance.PhotosRepository.Insert("testph", "descr", album, tag);
            DatabaseWorkUnit.Instance.PhotosRepository.Insert("testph2", "", album, tag);

            var photo = DatabaseWorkUnit.Instance.PhotosRepository.GetPhoto(album.AlbumId, "testph2");
            photo.PhotoName = "ICNERCARE ADSAFASFAS";

            DatabaseWorkUnit.Instance.PhotosRepository.Update(photo);

            //DatabaseWorkUnit.Instance.PhotosRepository.Delete(photoId);
            //*/
            //DatabaseWorkUnit.Instance.AlbumsRepository.Delete(album.AlbumId);
            //DatabaseWorkUnit.Instance.UserAccountRepository.Delete(user.UserName);
        }

        [TestMethod]
        public void PhotosFetchTest()
        {
            var user = DatabaseWorkUnit.Instance.UserAccountRepository.GetUser("coddo");
            var album = DatabaseWorkUnit.Instance.AlbumsRepository.GetAlbumsForUser(user.UserId, user.UserId)[0];
            
            DatabaseWorkUnit.Instance.PhotosRepository.GetPhotosFromAlbum(album.AlbumId);

            DatabaseWorkUnit.Instance.PhotosRepository.GetPhotosForHashTag("h2", 0, 3);
        }

        [TestMethod]
        public void HashTagsCRUDTest()
        {
            var user = DatabaseWorkUnit.Instance.UserAccountRepository.GetUser("coddo");
            var album = DatabaseWorkUnit.Instance.AlbumsRepository.GetAlbumsForUser(user.UserId, user.UserId)[0];

            var photo = DatabaseWorkUnit.Instance.PhotosRepository.GetPhoto(album.AlbumId, "testph2");

            DatabaseWorkUnit.Instance.HashTagsRepository.Insert(new[] {"h1", "h2", "h3", "h4", "h5",},
                photo.PhotoId);
        }

        [TestMethod]
        public void ContentTagsFetchTest()
        {
            var tags = DatabaseWorkUnit.Instance.ContentTagsRepository.GetAllContentTags();

            var tag = DatabaseWorkUnit.Instance.ContentTagsRepository.GetContentTag("SEXY");
        }
    }
}
