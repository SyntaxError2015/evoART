using evoART.DAL.UnitsOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace evoART.UnitTests
{
    [TestClass]
    public class PhotoTests
    {
        [TestMethod]
        public void PhotosTest()
        {
            var user = DatabaseWorkUnit.Instance.UserAccountRepository.GetUser("coddo");
            var album = DatabaseWorkUnit.Instance.AlbumsRepository.GetAlbumsForUser(user.UserId)[0];

            var photoId = DatabaseWorkUnit.Instance.PhotosRepository.Insert("testph", "descr", album);

            DatabaseWorkUnit.Instance.PhotosRepository.Update(
                DatabaseWorkUnit.Instance.PhotosRepository.GetPhoto(album.AlbumId, "testph"));

            DatabaseWorkUnit.Instance.PhotosRepository.Delete(photoId);
        }
    }
}
