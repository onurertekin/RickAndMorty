using DatabaseModel;
using DatabaseModel.Entities;
using DomainService.Exceptions;

namespace DomainService.Operations
{
    public class UserOperation
    {
        private readonly MainDbContext mainDbContext;

        public UserOperation(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }
        public List<User> Search(string userName, string firstName, string lastName, string email)
        {
            var query = mainDbContext.Users.AsQueryable();

            if (!string.IsNullOrEmpty(userName))
                query = query.Where(x => x.UserName == userName);

            if (!string.IsNullOrEmpty(firstName))
                query = query.Where(x => x.FirstName == firstName);

            if (!string.IsNullOrEmpty(lastName))
                query = query.Where(x => x.LastName == lastName);

            if (!string.IsNullOrEmpty(email))
                query = query.Where(x => x.Email == email);

            return query.ToList();
        }
        public User GetSingle(int id)
        {
            var currentUser = mainDbContext.Users.Where(x => x.Id == id).SingleOrDefault();
            if (currentUser == null)
                throw new BusinessException(404, "Kullanıcı Bulunamadı.");

            return currentUser;
        }
        public void Create(string firstName, string lastName, string userName, string email, string password)
        {
            #region Validations

            //username'i bertekin olan kullanıcıyı çekiyoruz
            var currentUser = mainDbContext.Users.Where(x => x.UserName == userName).SingleOrDefault();
            if (currentUser != null)
                throw new BusinessException(400, "Bu username kullanılıyor.");

            #endregion

            User user = new User();
            user.FirstName = firstName;
            user.LastName = lastName;
            user.UserName = userName;
            user.Email = email;
            user.Password = password;

            mainDbContext.Users.Add(user);
            mainDbContext.SaveChanges();
        }
        public void Update(int id, string firstName, string lastName, string userName, string email, string password)
        {
            #region Validations

            //username'i bertekin olan kullanıcıyı çekiyoruz
            var currentUser = mainDbContext.Users.Where(x => x.Id != id && x.UserName == userName).SingleOrDefault();
            if (currentUser != null)
                throw new BusinessException(400, "Bu username kullanılıyor.");

            #endregion

            var user = mainDbContext.Users.Where(x => x.Id == id).SingleOrDefault();
            if (user == null)
                throw new BusinessException(404, "Kullanıcı Bulunamadı.");

            user.FirstName = firstName;
            user.LastName = lastName;
            user.UserName = userName;
            user.Email = email;
            user.Password = password;

            mainDbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            var user = mainDbContext.Users.Where(x => x.Id == id).SingleOrDefault();
            if (user == null)
                throw new BusinessException(404, "Kullanıcı Bulunamadı.");

            user.Roles.Clear();

            mainDbContext.Users.Remove(user);
            mainDbContext.SaveChanges();
        }
    }
}
