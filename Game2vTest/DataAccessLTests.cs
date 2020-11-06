using System;
using System.IO;
using Game2v;
using Game2v.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace Game2vTest
{
    public class DataAccessLTests
    {

        private readonly ITestOutputHelper output;

        public DataAccessLTests(ITestOutputHelper output) {
            this.output = output;
        }

        [Fact]
        public async Task AddFriendAsync_FriendIsAdded()
        {
            using (var db = new DataContext(GetTestDbContextOptions()))
            {
                // Instancia um amigo fake
                var newFriend = new Friend() { FriendName = "ZZZ" };

                // Salva
                await db.AddFriendAsync(newFriend);

                // Consulta
                var savedFriend = await db.FindAsync<Friend>(newFriend.FriendId);

                // Apaga teste
                await db.DeleteFriendAsync(savedFriend.FriendId);
                
                // Compara
                Assert.Equal(newFriend, savedFriend);
            }
        }

        [Fact]
        public async Task DeleteFriendAsync_FriendIsDeleted()
        {
            using (var db = new DataContext(GetTestDbContextOptions()))
            {
                // Instancia um amigo fake
                var newFriend = new Friend() { FriendName = "ZZZ" };

                // Salva
                await db.AddFriendAsync(newFriend);

                // Consulta
                var savedFriend = await db.FindAsync<Friend>(newFriend.FriendId);

                // Apaga teste
                await db.DeleteFriendAsync(savedFriend.FriendId);
                
                // Consulta novamente
                savedFriend = await db.FindAsync<Friend>(newFriend.FriendId);

                // Compara
                Assert.NotEqual(newFriend, savedFriend);
            }
        }

        protected DbContextOptions<DataContext> GetTestDbContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlite()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<DataContext>()
                .UseSqlite(GetDbPath())
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
                        
        }

        protected string GetDbPath() 
        {
            string relativePath = @"Game2v\Game2v.db";
            var parentdir = Path.GetDirectoryName(Environment.CurrentDirectory);
            string myString = parentdir.Substring(0, parentdir.Length - 20);
            string absolutePath = Path.Combine(myString, relativePath);
            string connectionString = string.Format("Data Source={0}", absolutePath);

            return connectionString;
        }        
    }
}