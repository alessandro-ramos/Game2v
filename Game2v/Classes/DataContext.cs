using Game2v.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Game2v
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Game> Games { get; set; }

        #region Friends DAO services
        public async virtual Task<List<Friend>> GetFriendsAsync()
        {
            return await (from f in Friends
                                orderby f.FriendName
                                select f).ToListAsync();
        }
    

        
        public async virtual Task AddFriendAsync(Friend friend)
        {
            await Friends.AddAsync(friend);
            await SaveChangesAsync();
        }
        
        public void UpdateFriend(Friend friend)
        {
                Friends.Update(friend);
                SaveChanges();
        }
        
        public async virtual Task DeleteFriendAsync(int id)
        {
            var friend = await Friends.FindAsync(id);

            if (friend != null)
            {
                Friends.Remove(friend);
                await SaveChangesAsync();
            }
        }
        #endregion

    }
    
}