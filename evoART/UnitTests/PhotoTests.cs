using evoART.DAL.UnitsOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace evoART.UnitTests
{
    [TestClass]
    public class PhotoTests
    {
        [TestMethod]
        public void PhotosCRUDTest()
        {
            var user = DatabaseWorkUnit.Instance.UserAccountRepository.GetUser("coddo");
            var album = DatabaseWorkUnit.Instance.AlbumsRepository.GetAlbumsForUser(user.UserId)[0];

            var photoId = DatabaseWorkUnit.Instance.PhotosRepository.Insert("testph", "descr", album);
            DatabaseWorkUnit.Instance.PhotosRepository.Insert("testph2", "", album);

            DatabaseWorkUnit.Instance.PhotosRepository.Update(
                DatabaseWorkUnit.Instance.PhotosRepository.GetPhoto(album.AlbumId, "testph"));

            DatabaseWorkUnit.Instance.PhotosRepository.Delete(photoId);
        }

        [TestMethod]
        public void PhotosFetchTest()
        {
            var user = DatabaseWorkUnit.Instance.UserAccountRepository.GetUser("coddo");
            var album = DatabaseWorkUnit.Instance.AlbumsRepository.GetAlbumsForUser(user.UserId)[0];
            
            DatabaseWorkUnit.Instance.PhotosRepository.GetPhotosFromAlbum(album.AlbumId);

            DatabaseWorkUnit.Instance.PhotosRepository.GetPhotosForHashTag("h2", 0, 3);
        }

        [TestMethod]
        public void HashTagsCRUDTest()
        {
            var user = DatabaseWorkUnit.Instance.UserAccountRepository.GetUser("coddo");
            var album = DatabaseWorkUnit.Instance.AlbumsRepository.GetAlbumsForUser(user.UserId)[0];

            var photo = DatabaseWorkUnit.Instance.PhotosRepository.GetPhoto(album.AlbumId, "testph2");

            DatabaseWorkUnit.Instance.HashTagsRepository.Insert(new[] {"h1", "h2", "h3", "h4", "h5",},
                photo.PhotoId);
        }
    }
}
